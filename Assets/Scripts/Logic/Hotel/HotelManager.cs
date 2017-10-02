using UnityEngine;
using System.Collections;

namespace DDP.Logic
{
    public class HotelManager : Singleton<HotelManager>
    {
        private Hotel hotel;
        public event System.Action<int> OnGradeChanged;

        protected override void Awake()
        {
            base.Awake();
        }

        public void CheckOut(Visitor visitor)
        {
            var gradeInfo = SpecificSdb<Sdb.HotelGradeInfo>.Get();
            int originalGrade = gradeInfo.GetGrade(hotel.Data.Score);
            hotel.SetScore(hotel.Data.Score + visitor.Data.Satisfaction);
            int newGrade = gradeInfo.GetGrade(hotel.Data.Score);

            if(originalGrade != newGrade)
            {
                OnGradeChanged?.Invoke(newGrade);
            }
        }
    }
}