using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DDP.Main_Scene
{
	public class MainButtons : AnimationObject
	{
		[SerializeField]
		private Transform startButton;
		[SerializeField]
		private Transform creditButton;


		public override void InitBeforeAni()
		{
			gameObject.SetActive(true);
			startButton.localPosition = new Vector2(startButton.localPosition.x, -400);
			creditButton.localPosition = new Vector2(creditButton.localPosition.x, -400);
		}

		public override IEnumerator PlayShowAnimation()
		{
			startButton.DOLocalMoveY(50, 0.4f).SetEase(Ease.OutBack);
			yield return creditButton.DOLocalMoveY(-50, 0.4f).SetEase(Ease.OutBack).SetDelay(0.1f).WaitForCompletion();
		}
	}

}