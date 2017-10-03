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
        [SerializeField]
        private Animator doorAnimator;

		public Visitor currentVisitor { get; private set; }
        public int VisitorRating { get { return GetStarAmount(CalculateScore(currentVisitor)); } }
        public bool IsVisitorProcessed { get; private set; }

        public event System.Action<Logic.Visitor> OnVisitorCreated;

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
			var visitorInfos = SdbInstance<Sdb.VisitorInfo>.GetAll();
			var selectedInfo = visitorInfos[Random.Range(0, visitorInfos.Count)];
			Visitor newVisitor = VisitorFactory.Instance.Create(selectedInfo.RaceType);
            newVisitor.MoveToCounter(VisitorFactory.Instance.CounterPosition);
            IsVisitorProcessed = false;

			currentVisitor = newVisitor;

            rooms.SetActive(true);
            doorAnimator.Play("OpenAndClose");

            OnVisitorCreated?.Invoke(currentVisitor);
        }

        private int CalculateScore(Logic.Visitor visitor)
        {
            Constants.RaceType raceType = visitor.Info.RaceType;
            Sdb.VisitorInfo visitorInfo = SdbInstance<Sdb.VisitorInfo>.Get(raceType.ToString());
            int score = 0;
            const int scoreValue = 10;
            if(visitor.SelectedAttribute == visitorInfo.AttributeType)
            {
                score += scoreValue;
            }
            else if(visitor.SelectedAttribute == visitorInfo.HateAttributeType)
            {
                score -= scoreValue;
            }

            if(visitor.SelectedFacility == visitorInfo.LikeFacility)
            {
                score += scoreValue;
            }
            else if(visitor.SelectedFacility == visitorInfo.HateFacility)
            {
                score -= scoreValue;
            }

            if(visitor.SelectedFood == visitorInfo.LikeFood)
            {
                score += scoreValue;
            }
            else if(visitor.SelectedFood == visitorInfo.HateFood)
            {
                score -= scoreValue;
            }

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
			if (rating < 3)
			{
				messages = currentVisitor.Info.FailMessages;
			}
            else if(rating == 3)
            {
                messages = currentVisitor.Info.NormalMessages;
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

            HotelManager.Instance.CheckOut(currentVisitor, CalculateScore(currentVisitor) * currentVisitor.Info.VipGrade);
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