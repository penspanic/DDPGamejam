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
        public Constants.AttributeType HateAttributeType;
		public Constants.FacilityType LikeFacility;
        public Constants.FacilityType HateFacility;
        public Constants.FoodType LikeFood;
        public Constants.FoodType HateFood;
        public string Name;
        public Constants.SexType Sex;
        public string RaceName;
        public string JobName;
        public string AttributeDescription;
        public int VipGrade;
        public string[] EnterMessages;
        public string[] SuccessMessages;
        public string[] NormalMessages;
        public string[] FailMessages;
    }
}