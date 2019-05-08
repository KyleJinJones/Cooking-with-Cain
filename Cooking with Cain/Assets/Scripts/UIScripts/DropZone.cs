using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerDrag == null)
            return;

        Draggable2 draggable = eventData.pointerDrag.GetComponent<Draggable2>();

        if (draggable != null) {
            transform.GetChild(0).SetParent(draggable.parentToReturnTo);
            draggable.placeholderParent = this.transform;
            draggable.transform.SetSiblingIndex(5);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (eventData.pointerDrag == null)
            return;
        Draggable2 draggable = eventData.pointerDrag.GetComponent<Draggable2>();
        if (draggable != null && draggable.placeholderParent == this.transform)
        {
            draggable.placeholderParent = draggable.parentToReturnTo;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Draggable2 draggable = eventData.pointerDrag.GetComponent<Draggable2>();
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        if(draggable != null) {
            draggable.parentToReturnTo = this.transform;
        }
    }
}
