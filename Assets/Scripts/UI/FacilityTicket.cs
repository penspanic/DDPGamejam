using UnityEngine;
using System.Collections;

namespace DDP.UI
{
    public class FacilityTicket : MonoBehaviour
    {
        public Constants.FacilityType TargetFacility;
        public static FacilityTicket Create(Constants.FacilityType facility)
        {
            FacilityTicket newTicket = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/FacilityTicket")).GetComponent<FacilityTicket>();
            newTicket.TargetFacility = facility;

            newTicket.transform.SetParent(null);
            newTicket.transform.localScale = Vector3.one;

            return newTicket;
        }
    }
}