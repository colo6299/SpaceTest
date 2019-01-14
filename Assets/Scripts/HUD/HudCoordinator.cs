using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HudCoordinator : MonoBehaviour
{
    private Camera thisCamera;
    private GraphicRaycaster rayCaster;
    private PointerEventData eventData;
    private LoadoutManager loadout;
    private PanelInfo InfoPanel;


    private DragContainer selectedItem;
    private DropContainer selectedItemSlot;

    private void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        thisCamera = obj.GetComponentInChildren<Camera>();
        rayCaster = GetComponentInParent<GraphicRaycaster>();
        eventData = new PointerEventData(null);

        InfoPanel = transform.GetComponentInChildren<PanelInfo>();

        // set trash to false
        transform.GetChild(2).gameObject.SetActive(false);

        SystemControls.HudStateChange += OnHudChange;
        SystemControls.ClickDown += BeginDrag;
        SystemControls.ClickUp += EndDrag;

        loadout = GameObject.FindGameObjectWithTag("Loadout").GetComponent<LoadoutManager>();
    }

    private void Update()
    {
        GameObject item = RaycastUI("UI_Item");

        if (item != null)
        {
            Item i = item.GetComponent<DragContainer>().Item;
            InfoPanel.UpdateStats(i, loadout.GetItemInSlot(i.Type));
        }

        if (selectedItem == null) return;

        selectedItem.transform.position = Input.mousePosition;
    }

    private void OnHudChange(SystemControls.HudStates state)
    {
        if (state == SystemControls.HudStates.Menu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            enabled = true;
        }
        else
        {
            EndDrag();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            enabled = false;
        }
    }

    private void BeginDrag()
    {
        GameObject item = RaycastUI("UI_Item");
        GameObject slot = RaycastUI("UI_Slot");

        if (item != null && slot != null)
        {
            selectedItem = item.GetComponent<DragContainer>();
            selectedItemSlot = slot.GetComponent<DropContainer>();
        }

        transform.GetChild(2).gameObject.SetActive(true);
    }

    private void EndDrag()
    {
        if (selectedItem == null) return;

        GameObject container = RaycastUI("UI_Slot");

        DropContainer dropContainer = (container == null) ? null : container.GetComponent<DropContainer>();

        // could not find valid drop location
        if (container == null || container == selectedItemSlot.gameObject)
        {
            selectedItem.transform.localPosition = Vector3.zero;
        }
        // delete item
        else if (container.name == "Trash")
        {
            selectedItem.transform.SetParent(container.transform);
            loadout.TrashItem(selectedItem.Item);

            if (selectedItemSlot.transform.parent.name == "PendingItems")
            {
                Destroy(selectedItemSlot.gameObject);
            }

            Destroy(selectedItem.gameObject);
        }
        // only slot items of the same type
        else if (dropContainer.Type == selectedItem.Item.Type || dropContainer.Type == SlotType.None)
        {
            // swap items
            if (container.transform.childCount > 0) 
            {
                loadout.SlotItem(selectedItem.Item);

                container.transform.GetChild(0).SetParent(selectedItemSlot.transform);
                selectedItem.transform.SetParent(container.transform);
            }
            // fill empty slot
            else
            {
                loadout.SlotItem(selectedItem.Item);
                selectedItem.transform.SetParent(container.transform);

                if (selectedItemSlot.name == "Slot")
                {
                    Destroy(selectedItemSlot.gameObject);
                }
            }
        }

        selectedItem.transform.localPosition = Vector3.zero;
        transform.GetChild(2).gameObject.SetActive(false);
        selectedItem = null;
        selectedItemSlot = null;
    }

    public GameObject RaycastUI(string tag)
    {
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        rayCaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.tag == tag)
            {
                return result.gameObject;
            }
        }

        return null;
    }
}

