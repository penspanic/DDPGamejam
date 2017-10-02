using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace DDP.UI
{
    public class Facility : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IDragHandler
    {
        [SerializeField]
        private Constants.FacilityType FailityType;

        private Vector2 pointerDownPosition = Vector2.zero;
        private FacilityTicket createdTicket = null;

        public void OnPointerDown(PointerEventData eventData)
        {
            pointerDownPosition = eventData.position;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            float distance = eventData.position.DistanceWith(pointerDownPosition);
            if (distance > 50f && createdTicket == null)
            {
                createdTicket = FacilityTicket.Create(FailityType);

                createdTicket.GetComponent<DragTarget>().IsDraggable = true;
                ExecuteEvents.Execute<IBeginDragHandler>(createdTicket.gameObject, eventData, ExecuteEvents.beginDragHandler);

                ExecuteEvents.Execute<IEndDragHandler>(this.gameObject, eventData, ExecuteEvents.endDragHandler);
                eventData.pointerEnter = null;
                eventData.pointerDrag = createdTicket.gameObject;
            }
        }
    }
}