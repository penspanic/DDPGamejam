using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DDP.UI
{
	public class PopupManager_Test : MonoBehaviour
	{
		void Start()
		{
			PopupManager.Instance.InitPopupManager();
			PopupManager.Instance.ShowPopup(PopupType.ResultPopup);
		}
	}
}