using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasRootObject : MonoBehaviour
{
	protected CanvasGroup group;

	protected virtual void Awake()
	{
		group = GetComponent<CanvasGroup>();
	}



	public void SetActiveCanvasGroup(bool enable, float fadeTime = 0.0f)
	{
		group.interactable = enable;

		if (fadeTime == 0f)
			group.alpha = enable ? 1f : 0f;
		else
			group.DOFade(enable ? 1f : 0f, fadeTime);

		group.blocksRaycasts = enable;
	}
}
