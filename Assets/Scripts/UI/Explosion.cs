using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DDP.UI
{
	public class Explosion : MonoBehaviour
	{
		public Animator animator;

		public void Init()
		{
			gameObject.SetActive(false);
		}

		public void PlayExplosion(Vector2 spawnPos)
		{
			gameObject.SetActive(true);
			transform.position = spawnPos;
			animator.Play("explosion", 0, 0.0f);
		}

		public void OnEndExplosionAni()
		{
			//gameObject.SetActive(false);
		}
	}

}