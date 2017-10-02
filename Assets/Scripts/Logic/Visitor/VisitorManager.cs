using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class VisitorManager : Singleton<VisitorManager>
    {
		public Visitor curVisitor { get; private set; }
        public bool IsVisitorProcessed { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            IsVisitorProcessed = true;
            StartCoroutine(VisitorCreateProcess());
        }

        private IEnumerator VisitorCreateProcess()
        {
            while (true)
            {
                if(IsVisitorProcessed == false)
                {
                    yield return null;
                    continue;
                }

                CreateVisitor();
            }
        }

        private void CreateVisitor()
        {
            Visitor newVisitor = VisitorFactory.Instance.Create(Constants.RaceType.Human_W);
            newVisitor.MoveToCounter(VisitorFactory.Instance.CounterPosition);
            IsVisitorProcessed = false;

			curVisitor = newVisitor;
        }

        //public void Add(Visitor newVisitor)
        //{
        //    visitors.Add(newVisitor);
        //}

        //public Visitor Get(int serial)
        //{
        //    return visitors.Find((visitor) => { return visitor.Data.Serial == serial; });
        //}

        public void OnRoomSelected()
        {

        }

        public void OnFacilitySelected()
        {

        }

        public void OnFoodSelected()
        {

        }
    }
}