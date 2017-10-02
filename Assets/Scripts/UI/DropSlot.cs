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
                return;
            }

            Debug.Log("OnDrop");
            Destroy(targetVisitor.gameObject);
            //targetVisitor.AttachCommand(DragTarget.selectedObject.GetComponent<Command>(), true);
        }
    }
}