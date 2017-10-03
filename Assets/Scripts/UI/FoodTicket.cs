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
            newTicket.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/UI/Foods/" + food.ToString());

            newTicket.transform.SetParent(null);

            return newTicket;
        }
    }
}