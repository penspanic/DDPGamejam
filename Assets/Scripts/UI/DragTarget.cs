using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace DDP.UI
{
    public class DragTarget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static DragTarget selectedObject;
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

            SfxManager.Instance.Play(SfxType.UI_DragStart);

            OnDragBegin?.Invoke();

            selectedObject = this;
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
            this.transform.position = CameraUtil.GetWorldPositionOnPlane(mousePos, 2f);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SfxManager.Instance.Play(SfxType.UI_DragEnd);

            OnDragEnd?.Invoke();

            selectedObject = null;
            Destroy(this.gameObject);
        }
    }
}
