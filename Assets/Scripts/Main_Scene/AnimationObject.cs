using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DDP.Main_Scene
{
	public class AnimationObject : MonoBehaviour
	{
		public virtual void InitBeforeAni()
		{
			
		}

		public virtual IEnumerator PlayShowAnimation()
		{
			yield return null;
		}
	}
}