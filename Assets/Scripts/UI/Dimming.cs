using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dimming : CanvasRootObject
{
	[SerializeField]
	private UnityEngine.UI.Image dimmingImg;

	public void Init()
	{
		// base.Init();
		transform.SetAsLastSibling();
		SetActiveCanvasGroup(false);
		// dimmingImg.color = new Color(0, 0, 0, 0);
		// dimmingImg.DOFade(0f, 0f);
	}

	public void ShowDimming()
	{
		// dimmingImg.DOFade(0.66f, 0.1f);
		SetActiveCanvasGroup(true, 0.1f);
	}

	public void HideDimming()
	{
		transform.SetAsFirstSibling();
		// return dimmingImg.DOFade(0f, 0f);
		SetActiveCanvasGroup(false);
	}

	public void OnPressedDimming()
	{
		PopupManager.Instance.PopHidePopup();
	}

	public void SetActiveDimming(bool enable)
	{
		group.interactable = enable;
		group.alpha = enable ? 1f : 0f;
		group.blocksRaycasts = enable;
	}
}