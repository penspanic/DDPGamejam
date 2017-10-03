using System.Collections;
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
		private Star[] stars;

		private Transform portraitRoot;
		private Dictionary<string, Image> portraitDic;


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
			var curVisitor = Logic.VisitorManager.Instance.currentVisitor;
			var sprs = curVisitor.sprs;

			ApplyCharImg(sprs);

			// Stars
			for (int i = 0; i < stars.Length; ++i)
			{
				stars[i].InitStar();
			}


			yield return base.ShowPopup();


			float aniTime = 0.4f;
			for (int i = 0; i < Logic.VisitorManager.Instance.VisitorRating; ++i)
			{
				stars[i].FireStarEffect(aniTime);
				yield return new WaitForSeconds(aniTime);
			}
		}

		private void AddPortrait(string identifier)
		{
			portraitDic.Add(identifier, portraitRoot.Find(identifier).GetComponent<Image>());
		}

		private void AddEyeEmotion()
		{
			var img = portraitRoot.Find("Eyes").GetComponent<Image>();
			portraitDic.Add("Happy", img);
			portraitDic.Add("Sad", img);
			portraitDic.Add("Angry", img);
		}

		private void ApplyCharImg(Dictionary<string, Sprite> sprDic)
		{
			// Body And Head
			foreach (KeyValuePair<string, Sprite> div in sprDic)
			{
				if (portraitDic.ContainsKey(div.Key) == false)
					continue;

				portraitDic[div.Key].sprite = div.Value;
				portraitDic[div.Key].SetNativeSize();
			}

			// Emotion
			int rate = 3;
			ApplyEmotion(rate, sprDic);
		}

		private void ApplyEmotion(int rate, Dictionary<string, Sprite> sprDic)
		{
			var eyeImg = portraitDic["Eyes"];

			if (rate <= 3)
				eyeImg.sprite = Random.Range(0, 2) == 0 ? sprDic["Angry"] : sprDic["Sad"];
			else
				eyeImg.sprite = sprDic["Happy"];

			eyeImg.SetNativeSize();
			
		}

        public void OnPressedCloseButton()
        {
            Logic.VisitorManager.Instance.OnResultPopupClosed();
            PopupManager.Instance.PopHidePopup();
        }
	}
}