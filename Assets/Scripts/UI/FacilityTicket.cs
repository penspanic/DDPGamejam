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
            newTicket.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/UI/Facilities/" + facility.ToString());

            newTicket.transform.SetParent(null);

            return newTicket;
        }
    }
}