using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DDP.UI
{
	public class ResultPopup : PopupBase
	{
		[SerializeField]
		private Image portrait;
		[SerializeField]
		private Text evaluationText;



		public override IEnumerator ShowPopup()
		{
			var curVisitor = Logic.VisitorManager.Instance.curVisitor;
			var renderers = curVisitor.sprs;

			for (int i = 0; i < renderers.Length; ++i)
			{
				
			}
			yield return base.ShowPopup();
		}
	}
}