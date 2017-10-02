using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


namespace DDP.UI
{
	public class Star : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem ps;
		private Image image;

		void Awake()
		{
			image = GetComponent<Image>();
		}

		public void InitStar()
		{
			gameObject.SetActive(false);
		}

		public void FireStarEffect(float aniTime)
		{
			gameObject.SetActive(true);

			transform.rotation = new Quaternion(0, 0, 0.8f, 1f);

			Debug.Log(transform.rotation.eulerAngles.z);
			transform.localScale = new Vector3(8, 8, 8);
			image.color = new Color(1, 1, 1, 0);

			transform.DORotate(Vector3.zero, aniTime);
			image.DOFade(1f, aniTime);
			transform.DOScale(1f, aniTime).SetEase(Ease.InQuad).OnComplete(() =>
			{
				ps.Play();
			});
		}
	}

}