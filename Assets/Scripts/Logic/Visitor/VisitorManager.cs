using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Logic
{
    public class VisitorManager : Singleton<VisitorManager>
    {
        [SerializeField]
        private GameObject rooms;
        [SerializeField]
        private GameObject facilities;
        [SerializeField]
        private GameObject foods;

		public Visitor curVisitor { get; private set; }
        public int VisitorRating { get { return 1; } }
        public bool IsVisitorProcessed { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            UI.PopupManager.Instance.InitPopupManager();

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

            rooms.SetActive(true);
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
            Debug.Log("OnRoomSelected");
            rooms.SetActive(false);
            facilities.SetActive(true);
        }

        public void OnFacilitySelected()
        {
            Debug.Log("OnFacilitySelected");
            facilities.SetActive(false);
            foods.SetActive(true);
        }

        public void OnFoodSelected()
        {
            Debug.Log("OnFoodSelected");
            foods.SetActive(false);

            UI.PopupManager.Instance.ShowPopup(UI.PopupType.ResultPopup);
        }
    }
}