using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DDP.Main_Scene
{
	public class StepImage : MonoBehaviour
	{
		[SerializeField]
		private string identifier;
		[SerializeField]
		protected Image image;

		public virtual void InitStepImage(string prefix, int idx)
		{
			var path = prefix + identifier + "/" + idx;
			var source = Resources.Load<Sprite>(path);

			if (source == null)
				throw new UnityException("Don't have Sprite!!, Path: " + path);


			image.sprite = source;
			image.SetNativeSize();
		}
	}
}