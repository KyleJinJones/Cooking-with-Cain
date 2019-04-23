using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        Debug.Log("OnPointerExit");
    }

    public void OnDrop(PointerEventData eventData) {
        Draggable2 draggable = eventData.pointerDrag.GetComponent<Draggable2>();
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        if(draggable != null) {
            draggable.parentToReturnTo = this.transform;
        }
    }
}
