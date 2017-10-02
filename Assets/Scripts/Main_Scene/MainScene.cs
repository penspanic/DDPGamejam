using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DDP.Main_Scene
{
	public class MainScene : MonoBehaviour
	{
		[SerializeField]
		private Hotel hotel;
		[SerializeField]
		private Moon moon;


		void Awake()
		{
			string prefix = "Sprites/Main/";
			int level = 5;

			moon.InitStepImage(prefix, level);
			hotel.InitStepImage(prefix, level);

			StartCoroutine(ShowMainSceneRoutine());
		}


		private IEnumerator ShowMainSceneRoutine()
		{
			yield return null;
		}

	}

}