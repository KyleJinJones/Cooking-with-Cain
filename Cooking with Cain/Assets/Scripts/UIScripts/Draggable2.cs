using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;
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
        placeholderParent = parentToReturnTo;
        parent.transform.SetParent(parent.transform.parent.parent);
        parent.GetComponent<CanvasGroup>().blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        parent.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
        {
            //  print();
            //int index =
            placeholder.transform.SetParent(placeholderParent);
            
        }

        int newSiblingIndex = placeholderParent.childCount-4;

        parent.transform.position = eventData.position;
        for (int i = 0; i < placeholderParent.childCount; i++) {
            
            if (parent.transform.position.x < placeholderParent.GetChild(i).position.x) {
                newSiblingIndex = i;

                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
            if (parent.transform.position.y < placeholderParent.GetChild(i).position.y) {
                newSiblingIndex = i + 4;
                print(placeholderParent.GetChild(i));
                break;
            }
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
