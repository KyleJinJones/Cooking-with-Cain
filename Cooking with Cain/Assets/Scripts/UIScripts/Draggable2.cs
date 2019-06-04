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

        print(parent.transform.GetSiblingIndex());
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
            print("CHANGE");
            placeholder.transform.SetParent(placeholderParent);
        }

        int newSiblingIndex = placeholderParent.childCount - 4;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (parent.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                if (placeholder.transform.GetSiblingIndex() % 3 == 0)
                {
                    if (i == placeholder.transform.GetSiblingIndex() + 1)
                    {
                        newSiblingIndex = i;
                    }
                }
                else if (placeholder.transform.GetSiblingIndex() % 3 == 2)
                {
                    if (i == placeholder.transform.GetSiblingIndex() - 1)
                    {
                        newSiblingIndex = i;
                    }
                }
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;
                break;
            }
            /*if (parent.transform.position.y < placeholderParent.GetChild(i).position.y && placeholderParent.transform.GetSiblingIndex() == i % 3)
            {
                newSiblingIndex += 3;
                print(newSiblingIndex);
                placeholderParent.GetChild(newSiblingIndex).SetSiblingIndex(newSiblingIndex - 3);

                break;
            }*/
        }
        //print(newSiblingIndex);
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

    public void checkRows(PointerEventData eventData, int newSiblingIndex, int index) {
        parent.transform.position = eventData.position;

        newSiblingIndex = index;

        if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
            newSiblingIndex--;

        placeholder.transform.SetSiblingIndex(newSiblingIndex);
    }

    public void checkColumns(PointerEventData eventData, int newSiblingIndex) {
        parent.transform.position = eventData.position;

        newSiblingIndex += 3;

        placeholderParent.GetChild(newSiblingIndex).SetSiblingIndex(newSiblingIndex - 3);


    }
}
