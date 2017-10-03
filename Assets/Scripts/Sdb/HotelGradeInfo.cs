using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Sdb
{
    [CreateAssetMenu(fileName = "HotelGradeInfo", menuName = "HotelGradeInfo")]
    public class HotelGradeInfo : ScriptableObject
    {
        public List<IntVector2> Grades;
        public int GetGrade(int score)
        {
            for(int i = 0; i < Grades.Count; ++i)
            {
                if(Grades[i].X <= score && score <= Grades[i].Y)
                {
                    return i + 1;
                }
            }

            return 0;
        }

        public float GetExpRate(int score)
        {
            for (int i = 0; i < Grades.Count; ++i)
            {
                if (Grades[i].X <= score && score <= Grades[i].Y)
                {
                    int rangeValue = Grades[i].Y - Grades[i].X;
                    int myValue = score - Grades[i].X;

                    return (float)myValue / (float)rangeValue;
                }
            }

            return 0f;
        }
    }
}