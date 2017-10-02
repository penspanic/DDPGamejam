using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace DDP.UI
{
    public class DragTarget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static GameObject selectedObject;
        public bool IsDraggable { get; set; }
        public bool IsDragging { get; private set; }

        private Vector3 startPos;
        private Transform startParent;
        private RectTransform rectTransform;

        public event System.Action OnDragBegin;
        public event System.Action OnDragEnd;
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDraggable == false)
            {
                return;
            }

            if (OnDragBegin != null)
            {
                OnDragBegin();
            }

            selectedObject = gameObject;
            transform.SetParent(GameObject.Find("Front Canvas").transform);
            startParent = transform.parent;
            gameObject.GetComponent<Image>().raycastTarget = false;
            startPos = transform.position;
            IsDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsDraggable == false)
            {
                return;
            }

            Vector2 point;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out point);
            rectTransform.anchoredPosition = point;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (OnDragEnd != null)
            {
                OnDragEnd();
            }

            selectedObject = null;

            if (transform.parent == startParent && startParent.GetComponent<DropSlot>() == null)
            {
                Destroy(this.gameObject);
                return;
            }

            IsDragging = false;
            gameObject.GetComponent<Image>().raycastTarget = true;
        }
    }
}
