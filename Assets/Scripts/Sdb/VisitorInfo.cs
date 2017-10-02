using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DDP.Sdb
{
    [CreateAssetMenu(fileName = "VisitorInfo", menuName = "VisitorInfo")]
    public class VisitorInfo : SdbIdentifiableBase
    {
        public Constants.RaceType RaceType;
        public string[] Names;
        public int StayTime;
        public int VipScore;
        public List<Constants.FacilityType> LikeFacilities;
    }
}