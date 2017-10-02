using UnityEngine;
using System.Collections;

namespace DDP.UI
{
    public class FoodTicket : MonoBehaviour
    {
        public Constants.FoodType TargetFood;

        public static FoodTicket Create(Constants.FoodType food)
        {
            FoodTicket newTicket = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/UI/FoodTicket")).GetComponent<FoodTicket>();
            newTicket.TargetFood = food;

            newTicket.transform.SetParent(null);
            newTicket.transform.localScale = Vector3.one;

            return newTicket;
        }
    }
}