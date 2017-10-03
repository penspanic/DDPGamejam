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
		private Star5_Effect star5Effect;


		// private bool is

		void Awake()
		{
			string prefix = "Sprites/Main/";
			int level = Mathf.Clamp(Logic.HotelManager.Instance.Grade, 1, 5);
			bool isUpgrading = GradeChangeListener.Instance.isUpgrading;

			for (int i = 0; i < stepImages.Length; ++i)
			{
				stepImages[i].InitStepImage(prefix, level);
			}

			moon.InitBeforeAni();
			title.InitBeforeAni();

			if(level == 5)
				star5Effect.ShowEffect();

			StartCoroutine(ShowMainSceneRoutine());
		}


		private IEnumerator ShowMainSceneRoutine()
		{
			// init
			yield return new WaitForSeconds(1f);
			yield return moon.PlayShowAnimation();
			yield return new WaitForSeconds(0.2f);
			yield return title.PlayShowAnimation();
		}

		public void OnPressedStartButton()
		{
			SceneManager.Instance.ChangeScene("Lobby");
		}

	}

}