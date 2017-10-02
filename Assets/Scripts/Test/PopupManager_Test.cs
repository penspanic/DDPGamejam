using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager_Test : MonoBehaviour 
{
	void Start()
	{
		PopupManager.Instance.InitPopupManager();
		PopupManager.Instance.ShowPopup(PopupType.ResultPopup);
	}
}
