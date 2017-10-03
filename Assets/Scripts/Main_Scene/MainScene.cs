using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DDP.Main_Scene
{
	public class MainScene : MonoBehaviour
	{

		[SerializeField]
		private StepImage[] stepImages;


		// private bool is

		void Awake()
		{
			string prefix = "Sprites/Main/";
			int level = Logic.HotelManager.Instance.Grade;
			bool isUpgrading = GradeChangeListener.Instance.isUpgrading;

			for (int i = 0; i < stepImages.Length; ++i)
			{
				stepImages[i].InitStepImage(prefix, level);
			}

			StartCoroutine(ShowMainSceneRoutine());
		}


		private IEnumerator ShowMainSceneRoutine()
		{
			yield return null;
		}

	}

}