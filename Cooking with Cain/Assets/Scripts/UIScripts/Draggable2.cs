using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public GameObject parent;

    public enum Slot {  };

    GameObject placeholder = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        placeholder = new GameObject();
        placeholder.transform.SetParent(parent.transform.parent);
        LayoutElement layoutElement = placeholder.AddComponent<LayoutElement>();
        layoutElement.preferredWidth = parent.GetComponent<LayoutElement>().preferredWidth;
        layoutElement.preferredHeight = parent.GetComponent<LayoutElement>().preferredHeight;
        layoutElement.flexibleWidth = 0;
        layoutElement.flexibleHeight = 0;

        placeholder.transform.SetSiblingIndex(parent.transform.GetSiblingIndex());

        parentToReturnTo = parent.transform.parent;
        parent.transform.SetParent(parent.transform.parent.parent);
        parent.GetComponent<CanvasGroup>().blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        parent.transform.position = eventData.position;

        int newSiblingIndex = parentToReturnTo.childCount-4;

        parent.transform.position = eventData.position;
        for (int i = 0; i < parentToReturnTo.childCount; i++) {

            if (parent.transform.position.x < parentToReturnTo.GetChild(i).position.x) {
                newSiblingIndex = i;
                if (parent.transform.position.y < parentToReturnTo.GetChild(i).position.y)
                {
                    newSiblingIndex += 4;
                }
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;

            }/*

            if (parent.transform.position.y > parentToReturnTo.GetChild(i).position.y)
            {
                newSiblingIndex = i - 3;
                break;
            }*/
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        parent.transform.SetParent(parentToReturnTo);
        parent.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        parent.GetComponent<CanvasGroup>().blocksRaycasts = true;
        parent.transform.position = placeholder.transform.position;
        Destroy(placeholder);
    }
}
