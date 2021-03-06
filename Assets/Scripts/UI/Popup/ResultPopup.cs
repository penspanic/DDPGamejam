﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DDP.UI
{
	public class ResultPopup : PopupBase
	{
		[SerializeField]
		private Text evaluationText;
		[SerializeField]
		private Image outlineImg;
		[SerializeField]
		private Star[] stars;
		[SerializeField]
		private Sprite[] gradeOutlineSprs;

		private Transform portraitRoot;
		private Dictionary<string, Image> portraitDic;
		private bool isShowed;


		public override void PreInit()
		{
			base.PreInit();
			portraitRoot = transform.Find("Portrait");
			portraitDic = new Dictionary<string, Image>();

			AddPortrait("Body");
			AddPortrait("Head");
			AddPortrait("Hair");
			AddPortrait("Eyes");
		}

		public override IEnumerator ShowPopup()
		{
			isShowed = false;
			var curVisitor = Logic.VisitorManager.Instance.currentVisitor;
			var sprs = curVisitor.sprs;

            int visitorRating = Logic.VisitorManager.Instance.VisitorRating;

            if(visitorRating <= 3)
            {
                SfxManager.Instance.Play(SfxType.UI_Result_Normal);
            }
            else
            {
                SfxManager.Instance.Play(SfxType.UI_result_Success);
            }

            ApplyCharImg(sprs);
			ApplyOutline(visitorRating);

			// Stars
			for (int i = 0; i < stars.Length; ++i)
			{
				stars[i].InitStar();
			}

			// evalText
			evaluationText.text = Logic.VisitorManager.Instance.GetResultMessage();

			yield return base.ShowPopup();


			float aniTime = 0.4f;
			for (int i = 0; i < visitorRating; ++i)
			{
                SfxManager.Instance.Play(SfxType.Star);
				stars[i].FireStarEffect(aniTime);
				yield return new WaitForSeconds(aniTime);
			}

			isShowed = true;
		}

		private void AddPortrait(string identifier)
		{
			portraitDic.Add(identifier, portraitRoot.Find(identifier).GetComponent<Image>());
		}

		private void AddEyeEmotion()
		{
			var img = portraitRoot.Find("Eyes").GetComponent<Image>();
			portraitDic.Add("Default", img);
			portraitDic.Add("Happy", img);
			portraitDic.Add("Sad", img);
			portraitDic.Add("Angry", img);
		}

		private void ApplyCharImg(Dictionary<string, Sprite> sprDic)
		{
			foreach (KeyValuePair<string, Image> div in portraitDic)
			{
				div.Value.enabled = false;
			}

			// Body And Head
			foreach (KeyValuePair<string, Sprite> div in sprDic)
			{
				
				if (portraitDic.ContainsKey(div.Key) == false)
					continue;
				
				portraitDic[div.Key].enabled = true;
				portraitDic[div.Key].sprite = div.Value;
				portraitDic[div.Key].SetNativeSize();
			}

			// Emotion
			int rate = Logic.VisitorManager.Instance.VisitorRating;
			ApplyEmotion(rate, sprDic);

		}

		private void ApplyEmotion(int rate, Dictionary<string, Sprite> sprDic)
		{
			var eyeImg = portraitDic["Eyes"];
			eyeImg.enabled = true;

			if (rate <= 2)
				eyeImg.sprite = Random.Range(0, 2) == 0 ? sprDic["Angry"] : sprDic["Sad"];
			else if( rate == 3)
				eyeImg.sprite = sprDic["Default"];
			else
				eyeImg.sprite = sprDic["Happy"];

			eyeImg.SetNativeSize();
			
		}

		private void ApplyOutline(int rate)
		{
			if (rate <= 2)
				outlineImg.sprite = gradeOutlineSprs[0];
			else if(rate <= 3)
				outlineImg.sprite = gradeOutlineSprs[1];
			else
				outlineImg.sprite = gradeOutlineSprs[2];
		}

        public void OnPressedCloseButton()
        {
			if (isShowed == false)
				return;

            SfxManager.Instance.Play(SfxType.UI_Click3);

            Logic.VisitorManager.Instance.OnResultPopupClosed();
            PopupManager.Instance.PopHidePopup();
        }
	}
}