using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class VisitorManager : Singleton<VisitorManager>
    {
        private List<Visitor> visitors = new List<Visitor>();
        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(VisitorCreateProcess());
        }

        private IEnumerator VisitorCreateProcess()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
				Visitor newVisitor = VisitorFactory.Instance.Create(Constants.RaceType.Human_W);
                newVisitor.MoveToCounter(VisitorFactory.Instance.CounterPosition);
            }
        }

        public void Add(Visitor newVisitor)
        {
            visitors.Add(newVisitor);
        }

        public Visitor Get(int serial)
        {
            return visitors.Find((visitor) => { return visitor.Data.Serial == serial; });
        }
    }
}