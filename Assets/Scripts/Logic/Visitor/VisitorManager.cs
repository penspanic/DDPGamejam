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

		public Visitor currentVisitor { get; private set; }
        public int VisitorRating { get { return GetStarAmount(CalculateScore(currentVisitor)); } }
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

			currentVisitor = newVisitor;

            rooms.SetActive(true);
        }

        private int CalculateScore(Logic.Visitor visitor)
        {
            Constants.RaceType raceType = visitor.Info.RaceType;
            Sdb.VisitorInfo visitorInfo = SdbInstance<Sdb.VisitorInfo>.Get(raceType.ToString());
            int score = 0;
            score += visitor.SelectedAttribute == visitorInfo.AttributeType ? 1 : -1;
            score += visitor.SelectedFacility == visitorInfo.LikeFacility ? 1 : -1;
            score += visitor.SelectedFood == visitorInfo.LikeFood ? 1 : -1;
            score *= 10;
            return score;
        }

        public int GetStarAmount(int score)
        {
            if(score >= 30)
            {
                return 5;
            }
            else if(score >= 20)
            {
                return 4;
            }
            else if(score >= 0)
            {
                return 3;
            }
            else if(score >= -10)
            {
                return 2;
            }

            return 1;
        }

		public string GetResultMessage()
		{
			int rating = VisitorRating;
			string[] messages = null;
			if (rating <= 3)
			{
				messages = currentVisitor.Info.FailMessages;
			}
			else
			{
				messages = currentVisitor.Info.SuccessMessages;
			}

			if (messages.Length == 0)
			{
				return string.Empty;
			}

			return messages[Random.Range(0, messages.Length)];
		}

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

            HotelManager.Instance.CheckOut(currentVisitor, CalculateScore(currentVisitor));
            UI.PopupManager.Instance.ShowPopup(UI.PopupType.ResultPopup);
        }

        public void OnResultPopupClosed()
        {
            Destroy(currentVisitor.gameObject);
            currentVisitor = null;
            IsVisitorProcessed = true;
        }
    }
}