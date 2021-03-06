﻿using UnityEngine;
using System.Collections;

namespace DDP.Logic
{
    public class HotelManager : Singleton<HotelManager>
    {
        public int Score { get; private set; }
		public int Grade { get { return GetGrade(); } }
        public event System.Action<int/*Current*/, int/*Previous*/> OnGradeChanged;
        public event System.Action<float> OnExpRateChanged;

        protected override void Awake()
        {
            base.Awake();
			DontDestroyOnLoad(this.gameObject);
            if(PlayerPrefs.HasKey("Score") == true)
            {
                Score = PlayerPrefs.GetInt("Score");
            }
        }

        private void Start()
        {
			OnGradeChanged?.Invoke(GetGrade(), GetGrade());
            OnExpRateChanged?.Invoke(GetExpRate());
        }

        private int GetGrade()
        {
            var gradeInfo = SpecificSdb<Sdb.HotelGradeInfo>.Get();
            return gradeInfo.GetGrade(Score);
        }

		public float GetExpRate()
        {
            var gradeInfo = SpecificSdb<Sdb.HotelGradeInfo>.Get();
            return gradeInfo.GetExpRate(Score);
        }

        public void CheckOut(Visitor visitor, int visitorScore)
        {
            var visitorInfo = SdbInstance<Sdb.VisitorInfo>.Get(visitor.Info.RaceType.ToString());
            var gradeInfo = SpecificSdb<Sdb.HotelGradeInfo>.Get();
            int originalGrade = gradeInfo.GetGrade(Score);
			int originalScore = Score;
            Score += visitorScore * visitorInfo.VipGrade;
            int newGrade = gradeInfo.GetGrade(Score);

            if(originalGrade != newGrade)
            {
                if(originalGrade > newGrade)
                {
                    Score = gradeInfo.Grades[originalGrade - 1].X;
                }
                else
                {
				    OnGradeChanged?.Invoke(newGrade, originalGrade);
                }
            }
			if (newGrade > originalGrade)
			{
				Debug.Log("UpGrade!!, new: " + newGrade + ", ori: " + originalGrade + " new score : " + Score + " ori score : " + originalScore);
				Main_Scene.GradeChangeListener.Instance.OnHotelGradeIncreased();
			}

            OnExpRateChanged?.Invoke(gradeInfo.GetExpRate(Score));
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("Score", Score);
        }
    }
}