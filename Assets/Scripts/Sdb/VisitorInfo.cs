using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Sdb
{
    [CreateAssetMenu(fileName = "VisitorInfo", menuName = "VisitorInfo")]
    public class VisitorInfo : SdbIdentifiableBase
    {
        public Constants.RaceType RaceType;
		public List<Constants.AttributeType> Attributes;
		public List<Constants.FacilityType> Facilities;
        public List<Constants.FoodType> Foods;
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