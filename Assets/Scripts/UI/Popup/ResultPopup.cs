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
			var curVisitor = Logic.VisitorManager.Instance.curVisitor;
			var sprs = curVisitor.sprs;

			foreach (KeyValuePair<string, Sprite> div in sprs)
			{
				portraitDic[div.Key].sprite = div.Value;
			}

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
	}
}