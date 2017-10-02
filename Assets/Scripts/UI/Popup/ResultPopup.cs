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
			yield return base.ShowPopup();
		}

		private void AddPortrait(string identifier)
		{
			portraitDic.Add(identifier, portraitRoot.Find(identifier).GetComponent<Image>());
		}

	}
}