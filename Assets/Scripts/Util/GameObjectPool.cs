using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameObjectPool<T> where T : Component
{

	private Stack<T> objectPool;
	private int overAllocateCount;
	private T oriObject;
	private Transform parent;
	private string objName;
	private bool active;

	public GameObjectPool(T oriObject, string objName, Transform parent, int count, int overAllocateCount, bool active = false)
	{
		this.overAllocateCount = overAllocateCount;
		this.oriObject = oriObject;
		this.parent = parent;
		this.objName = objName;
		this.objectPool = new Stack<T>(count);
		this.active = active;
		Allocate(count, active);
	}

	public void Allocate(int alloCount, bool active = false)
	{
		for (int i = 0; i < alloCount; ++i)
		{
			T obj = GameObject.Instantiate<T>(oriObject, parent, false);
			obj.name = objName + i.ToString();
			obj.gameObject.SetActive(active);

			objectPool.Push(obj);
		}
	}


	public T Pop(bool setActive = true)
	{
		if (objectPool.Count <= 0)
		{
			// Debug.Log("ObjPool : Over Count Object Pop");
			Allocate(overAllocateCount, active);
		}
		T retObj = objectPool.Pop();
		retObj.gameObject.SetActive(setActive);
		return retObj;

		// return objectPool.Pop();
	}

	public void Push(T obj, bool active = false)
	{
		obj.gameObject.SetActive(active);
		if (objectPool.Contains(obj) == false)
		{

			objectPool.Push(obj);
		}
		else
		{
			Debug.Log("OVERLAP!!");
		}
	}

	public int GetCurPoolCount()
	{
		return objectPool.Count;
	}

}

