using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DDP.Main_Scene
{
	public class Title : AnimationObject 
	{
		public override void InitBeforeAni()
		{
			transform.localPosition = new Vector2(0, 750);	
		}
		public override IEnumerator PlayShowAnimation()
		{
			

			yield return transform.DOLocalMoveY(520, 0.3f).SetEase(Ease.OutBounce).WaitForCompletion();
		}
	}
}