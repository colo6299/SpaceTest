using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotHandler : MonoBehaviour, IPointerEnterHandler
{

    //public GameObject Item
    //{
    //    get
    //    {
    //        if (transform.childCount > 0)
    //        {
    //            return transform.GetChild(0).gameObject;
    //        }

    //        return null;
    //    }
    //}

    //public void OnDrop(PointerEventData eventData)
    //{   
    //    GameObject item = Item;

    //    if (item == null)
    //    {
    //        ItemDragHandler.itemBeingDragged.transform.SetParent(transform);
    //    }
    //    else
    //    {
    //        item.transform.SetParent(ItemDragHandler.itemBeingDragged.transform.parent);
    //        ItemDragHandler.itemBeingDragged.transform.SetParent(transform);
    //    }
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemDragHandler.HoverObject = gameObject;   
        //Debug.Log("Doing stuff");
    }
}
