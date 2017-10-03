using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace DDP.Main_Scene
{
	public class MainScene : MonoBehaviour
	{

		[SerializeField]
		private StepImage[] stepImages;

		[SerializeField]
		private AnimationObject moon;
		[SerializeField]
		private AnimationObject title;
		[SerializeField]
		private AnimationObject buttons;
		[SerializeField]
		private Star5_Effect star5Effect;
		[SerializeField]
		private UpgradingObjs upgradeObjs;

		// public 

		private string prefix = "Sprites/Main/";

		// private bool is

		void Awake()
		{

			int level = Mathf.Clamp(Logic.HotelManager.Instance.Grade, 1, 5);
			bool isUpgrading = GradeChangeListener.Instance.isUpgrading;

			//int level = 5;
			//bool isUpgrading = true;


			moon.InitBeforeAni();
			title.InitBeforeAni();
			buttons.InitBeforeAni();


			if (isUpgrading)
				upgradeObjs.InitBeforeAni(level);
			else
				upgradeObjs.gameObject.SetActive(false);
			//if (isUpgrading == true)
			//{
			//	upgradeObjs.OnChangeStepImage += OnChangeStepImage;

			//	OnChangeStepImage(level - 1);
			//	upgradeObjs.InitBeforeAni(level);
			//	StartCoroutine(upgradeObjs.PlayShowAnimation());
			//}
			//else
			//{
			//	OnChangeStepImage(level);

			StartCoroutine(ShowMainSceneRoutine(level, isUpgrading));
			GradeChangeListener.Instance.isUpgrading = false;
			// }
		}


		private IEnumerator ShowMainSceneRoutine(int grade, bool isUpgrade)
		{
			int stepGrade = isUpgrade == true ? grade - 1 : grade;
			OnChangeStepImage(stepGrade);
			// init
			yield return new WaitForSeconds(1f);
			yield return moon.PlayShowAnimation();


			if (isUpgrade == true)
			{
				upgradeObjs.OnChangeStepImage += OnChangeStepImage;

				StartCoroutine(upgradeObjs.PlayShowAnimation());
			}
			else 
			{
				yield return new WaitForSeconds(0.2f);
				yield return title.PlayShowAnimation();
				yield return buttons.PlayShowAnimation();
			}

		}



		public void OnPressedStartButton()
		{
			SceneManager.Instance.ChangeScene("Lobby");
		}

		public void OnChangeStepImage(int grade)
		{
			for (int i = 0; i < stepImages.Length; ++i)
				stepImages[i].InitStepImage(prefix, grade);

			if (grade == 5)
				star5Effect.ShowEffect();
		}



	}

}