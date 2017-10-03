using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DDP.Test
{
	public class CharacterFactory_Test : MonoBehaviour
	{

		void Start()
		{

			var visitorInfos = SdbInstance<Sdb.VisitorInfo>.GetAll();
			var selectedInfo = Constants.RaceType.Asmodian_W;//visitorInfos[Random.Range(0, visitorInfos.Count)];
			Logic.Visitor newVisitor = Logic.VisitorFactory.Instance.Create(selectedInfo);
			newVisitor.MoveToCounter(Logic.VisitorFactory.Instance.CounterPosition);
		}
	}

}