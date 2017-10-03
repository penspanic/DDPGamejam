using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DDP.Main_Scene
{
	public class Moon : AnimationObject
	{
		public override void InitBeforeAni()
		{
			transform.localPosition = new Vector2(0, 300);
		}
		public override IEnumerator PlayShowAnimation()
		{
			yield return transform.DOLocalMoveY(550, 0.5f).SetEase(Ease.OutBack).WaitForCompletion();
		}
	}
}