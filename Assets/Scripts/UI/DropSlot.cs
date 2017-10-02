using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace DDP.UI
{
    public class DropSlot : MonoBehaviour, IDropHandler
    {
        private Logic.Visitor targetVisitor;

        private void Awake()
        {
            targetVisitor = GetComponent<Logic.Visitor>();
        }

        private GameObject GetItem()
        {
            if (transform.childCount > 0)
                return transform.GetChild(0).gameObject;
            else
                return null;
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (DragTarget.selectedObject == null)
            {
                Debug.Log("SelectedObject is null.");
                return;
            }

            Debug.Log("OnDrop : Target");
            if(DragTarget.selectedObject.GetComponent<RoomKey>() != null)
            {
                targetVisitor.SetRoom(DragTarget.selectedObject.GetComponent<RoomKey>().Attribute);
            }
            else if(DragTarget.selectedObject.GetComponent<FacilityTicket>() != null)
            {
                targetVisitor.SetFacility(DragTarget.selectedObject.GetComponent<FacilityTicket>().TargetFacility);
            }
            else if (DragTarget.selectedObject.GetComponent<FoodTicket>() != null)
            {
                targetVisitor.SetFood(DragTarget.selectedObject.GetComponent<FoodTicket>().TargetFood);
            }


            //targetVisitor.AttachCommand(DragTarget.selectedObject.GetComponent<Command>(), true);
        }
    }
}