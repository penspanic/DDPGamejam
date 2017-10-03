using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



namespace DDP.UI
{
	public enum PopupType
	{
		ResultPopup,
	}


	[Serializable]
	public class PopupDictionary : SerializableDictionary<PopupType, PopupBase> { }

	// 팝업이 뜨는 종류
	// 1. 위에 얹쳐지는 팝업
	// 2. 큐되는 팝업
	// -------------

	public class PopupManager : Singleton<PopupManager>
	{
		[SerializeField]
		private PopupDictionary popupPrefabDic;

		[SerializeField]
		private Dimming dimmingPrefab;

		private PopupDictionary popupDic;
		private bool isTransition;

		public Stack<PopupType> popupStack { get; private set; }
		public Queue<PopupType> popupQueue { get; private set; }

		private GameObjectPool<Dimming> dimmingPool;
		private Stack<Dimming> dimmingStack;


		public void InitPopupManager()
		{
			popupStack = new Stack<PopupType>();
			popupQueue = new Queue<PopupType>();
			popupDic = new PopupDictionary();

			// Dimming
			dimmingPool = new GameObjectPool<Dimming>(dimmingPrefab, "dimming", transform, popupDic.Count, 5);
			dimmingStack = new Stack<Dimming>();

			foreach (KeyValuePair<PopupType, PopupBase> div in popupPrefabDic)
			{
				if (div.Value == null)
					continue;


				var popup = Instantiate(div.Value, transform, false);
				popup.PreInit();

				popupDic.Add(div.Key, popup);
				Debug.Log("AAAA");
			}

			isTransition = false;
		}

		public void ShowPopup(PopupType showingPopup, bool isQueued = false)
		{
			if (IsInPopupDic(showingPopup) == false)
			{
				Debug.LogError("This PopupType is Not Impl!!");
				return;
			}

			if (isQueued == true)
			{
				if (popupStack.Count > 0)
				{
					popupQueue.Enqueue(showingPopup);
					return;
				}
			}
			else
			{
				if (IsOverlapPopup(showingPopup))
					return;

				if (isTransition == true)
					return;
			}

			StartCoroutine(ShowPopupRoutine(showingPopup));
		}

		private IEnumerator ShowPopupRoutine(PopupType showingPopup)
		{
			isTransition = true;


			popupStack.Push(showingPopup);
			PushDimming();
			yield return popupDic[showingPopup].ShowPopup();

			isTransition = false;
		}

		public void PopHidePopup()
		{
			if (popupStack.Count == 0)
				return;

			if (isTransition == true)
				return;

			var popup = popupStack.Peek();
			StartCoroutine(HidePopupRoutine(popup));

		}

		private IEnumerator HidePopupRoutine(PopupType hidingPopup)
		{
			isTransition = true;

			yield return popupDic[hidingPopup].HidePopup();



			popupStack.Pop();
			isTransition = false;

			if (popupStack.Count == 0 && popupQueue.Count > 0)
			{
				var queuePopup = popupQueue.Dequeue();
				ShowPopup(queuePopup);
				yield break;
			}

			PopDimming();
		}



		// --------------------
		// Dimming 
		// 
		private void PushDimming()
		{
			if (dimmingStack.Count == popupStack.Count)
				return;

			var dimming = dimmingPool.Pop();
			dimming.Init();
			dimming.ShowDimming();
			dimmingStack.Push(dimming);
		}

		private void PopDimming()
		{
			var dimming = dimmingStack.Pop();
			//dimming.HideDimming().OnComplete(() =>
			//{
			//	dimmingPool.Push(dimming);
			//});
			dimming.HideDimming();
			dimmingPool.Push(dimming);
		}

		// --------------------
		// Checking Func
		// 
		private bool IsInPopupDic(PopupType checkType)
		{
			foreach (KeyValuePair<PopupType, PopupBase> popup in popupDic)
			{
				if (checkType == popup.Key)
				{
					return true;
				}
			}
			return false;
		}

		private bool IsOverlapPopup(PopupType checkType)
		{
			foreach (PopupType popup in popupStack)
			{
				if (popup == checkType)
				{
					return true;
				}
			}
			return false;
		}

		// -------------------
		// Get
		// 

		public PopupBase GetPopup(PopupType popupType)
		{
			if (IsInPopupDic(popupType) == false)
				return null;

			return popupDic[popupType];
		}
	}
}