using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public static GameObject itemBeingDragged;
    public static GameObject HoverObject;

    public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            itemBeingDragged = null;
        transform.localPosition = Vector3.zero;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (HoverObject != null)
        {
            //ItemDragHandler.itemBeingDragged.transform.SetParent(transform);

            if (HoverObject.transform.childCount > 0)
            {
                Transform tran = HoverObject.transform.GetChild(0).parent;
                HoverObject.transform.GetChild(0).SetParent(itemBeingDragged.transform.parent);
                itemBeingDragged.transform.SetParent(tran);
            }
        }
    }
}
