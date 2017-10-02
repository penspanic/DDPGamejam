using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

namespace DDP.UI
{
    public class Room : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IDragHandler
    {
        [SerializeField]
        private Image attributeImage;
        [SerializeField]
        private Text upgradeText;

        private Vector2 pointerDownPosition = Vector2.zero;
        private RoomKey createdKey = null;

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
            if (distance > 50f && createdKey == null)
            {
                createdKey = RoomKey.Create(this.GetComponent<Logic.Room>());

                createdKey.GetComponent<DragTarget>().IsDraggable = true;
                ExecuteEvents.Execute<IBeginDragHandler>(createdKey.gameObject, eventData, ExecuteEvents.beginDragHandler);

                ExecuteEvents.Execute<IEndDragHandler>(this.gameObject, eventData, ExecuteEvents.endDragHandler);
                eventData.pointerEnter = null;
                eventData.pointerDrag = createdKey.gameObject;
            }
        }
    }
}