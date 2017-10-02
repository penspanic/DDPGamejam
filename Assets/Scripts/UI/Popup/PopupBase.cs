using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DDP.UI
{
	public class PopupBase : CanvasRootObject
	{
		public bool isShown { get; protected set; }
		// public AnimationCurve curve;

		public virtual void PreInit()
		{
			isShown = false;


			// gameObject.SetActive(false);
			gameObject.SetActive(true);
			SetActiveCanvasGroup(false);

		}

		public virtual IEnumerator ShowPopup()
		{
			transform.SetAsLastSibling();

			SetActiveCanvasGroup(true);

			// Init();

			transform.localScale = Vector3.one;
			yield return transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f, 8, 0.5f).WaitForCompletion();

			transform.localScale = Vector3.one;

			isShown = true;
		}

		public virtual IEnumerator HidePopup()
		{
			isShown = false;
			yield return null;
			SetActiveCanvasGroup(false);
			// Release();
		}

		public void OnPressedCloseButton()
		{
			PopupManager.Instance.PopHidePopup();
		}
	}
}