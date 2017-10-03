using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Sdb
{
    [CreateAssetMenu(fileName = "VisitorInfo", menuName = "VisitorInfo")]
    public class VisitorInfo : SdbIdentifiableBase
    {
        public Constants.RaceType RaceType;
		public Constants.AttributeType AttributeType;
		public Constants.FacilityType LikeFacility;
        public Constants.FoodType LikeFood;
        public string Name;
        public int VipGrade;
        public string[] EnterMessages;
        public string[] SuccessMessages;
        public string[] FailMessages;
    }
}