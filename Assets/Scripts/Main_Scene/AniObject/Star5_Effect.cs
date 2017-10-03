using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DDP.Main_Scene
{
	public class Star5_Effect : MonoBehaviour
	{
		[SerializeField]
		private Transform light_L;
		[SerializeField]
		private Transform light_R;


		public void ShowEffect()
		{
			gameObject.SetActive(true);
			light_L.DORotate(new Vector3(0, 0, -40), 1.5f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
			light_R.DORotate(new Vector3(0, 0, 40), 1.6f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo).SetDelay(0.3f);
		}

		public void HideEffect()
		{
			gameObject.SetActive(false);
		}
	}
}