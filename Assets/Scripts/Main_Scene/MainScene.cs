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
			//PlayerPrefs.DeleteAll();
			#if UNITY_IOS
			Application.targetFrameRate = 60;
			#endif

			int level = Mathf.Clamp(Logic.HotelManager.Instance.Grade, 1, 5);
			bool isUpgrading = GradeChangeListener.Instance.isUpgrading;

			moon.InitBeforeAni();
			title.InitBeforeAni();
			buttons.InitBeforeAni();

			if (isUpgrading)
				upgradeObjs.InitBeforeAni(level);
			else
				upgradeObjs.gameObject.SetActive(false);

			StartCoroutine(ShowMainSceneRoutine(level, isUpgrading));
			GradeChangeListener.Instance.isUpgrading = false;
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

				yield return upgradeObjs.PlayShowAnimation();
				yield return new WaitForSeconds(1f);
				SceneManager.Instance.ChangeScene("Lobby");
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
            SfxManager.Instance.Play(SfxType.UI_Click2);
			SceneManager.Instance.ChangeScene("Lobby");
		}
		public void OnPressedCredit()
		{
            SfxManager.Instance.Play(SfxType.UI_Click3);
            Credit.Instance.Show();
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