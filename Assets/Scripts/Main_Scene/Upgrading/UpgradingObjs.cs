using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DDP.Main_Scene
{
	public class UpgradingObjs : AnimationObject
	{
		[SerializeField]
		private UI.Star[] stars;
		[SerializeField]
		private Image whiteFade;
		[SerializeField]
		private Image lvUpImage;

		private int grade;

		public Action<int> OnChangeStepImage = delegate { };


		public void InitBeforeAni(int nextGrade)
		{
			base.InitBeforeAni();
			gameObject.SetActive(true);
			grade = nextGrade;
			lvUpImage.color = new Color(1, 1, 1, 0);
			lvUpImage.transform.localScale = new Vector3(2, 2, 2);

			for (int i = 0; i < stars.Length; ++i)
				stars[i].InitStar();

			whiteFade.enabled = false;
		}

		public override IEnumerator PlayShowAnimation()
		{
			
			for (int i = 0; i < grade; ++i)
			{
				stars[i].FireStarEffect(0.4f);
				yield return new WaitForSeconds(0.25f);
			}

			yield return new WaitForSeconds(0.1f);
			whiteFade.enabled = true;
			OnChangeStepImage(grade);


			yield return whiteFade.DOFade(0f, 1f).WaitForCompletion();
			lvUpImage.transform.DOScale(1f, 0.4f);
			yield return lvUpImage.DOFade(1f, 0.4f).WaitForCompletion();


			// transform.DO
//			yield return lvUpTrs.DOLocalMoveY(450, 0.4f).SetEase(Ease.OutBack, 2.5f).WaitForCompletion();
		}



	}

}