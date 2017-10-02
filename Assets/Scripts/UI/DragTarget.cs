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

        public event System.Action OnDragBegin;
        public event System.Action OnDragEnd;
        private void Awake()
        {
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDraggable == false)
            {
                return;
            }

            OnDragBegin?.Invoke();

            selectedObject = gameObject;
            startPos = transform.position;
            IsDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsDraggable == false)
            {
                return;
            }

            Vector2 mousePos = Input.mousePosition;
            this.transform.position = CameraUtil.GetWorldPositionOnPlane(mousePos, 1f);
            //Vector2 point;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out point);
            //rectTransform.anchoredPosition = point;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("DragTarget : OnEndDrag");
            OnDragEnd?.Invoke();

            selectedObject = null;
            Destroy(this.gameObject);
        }
    }
}
