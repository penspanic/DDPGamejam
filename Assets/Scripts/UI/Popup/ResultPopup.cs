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

			yield return base.ShowPopup();
		}
	}
}