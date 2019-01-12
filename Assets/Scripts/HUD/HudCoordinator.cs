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

    private Transform selectedItem;

    private void Awake()
    {
        enabled = false;

        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        thisCamera = obj.GetComponentInChildren<Camera>();
        rayCaster = GetComponentInParent<GraphicRaycaster>();
        eventData = new PointerEventData(null);

        SystemControls.HudStateChange += OnHudChange;
        SystemControls.ClickDown += BeginDrag;
        SystemControls.ClickUp += EndDrag;

        loadout = GameObject.FindGameObjectWithTag("Loadout").GetComponent<LoadoutManager>();
    }

    private void Update()
    {
        if (selectedItem == null) return;

        selectedItem.position = Input.mousePosition;
    }

    private void OnHudChange(SystemControls.HudStates state)
    {
        if (state == SystemControls.HudStates.Menu)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            EndDrag();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void BeginDrag()
    {
        DragContainer entity = RaycastDragEntity();

        if (entity != null)
        {
            selectedItem = entity.transform;
        }

        enabled = true;
    }

    private void EndDrag()
    {
        if (selectedItem == null) return;

        DropContainer container = RaycastDropContainer();

        if (container == null)
        {
            selectedItem.localPosition = Vector3.zero;
        }
        else if (selectedItem.tag == container.tag || container.tag == "Untagged")
        {
            Transform p = selectedItem.parent;
            selectedItem.SetParent(container.transform);
            if (selectedItem.tag == container.tag)
            {
                //2 lazy to pass Entity :)
                loadout.SlotItem(selectedItem.GetComponent<DragContainer>().item);
            }

            if (p.name == "Slot")
            {
                p.gameObject.SetActive(false);
            }
        }
        else
        {
            selectedItem.localPosition = Vector3.zero; //I do realize this is here twice, I'm too lazy to swap 1 & 2
        }

        selectedItem = null;
        enabled = false;
    }

    public DragContainer RaycastDragEntity()
    {
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        rayCaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            DragContainer item = result.gameObject.GetComponent<DragContainer>();
            if (item != null)
            {
                return item;
            }
        }

        return null;
    }

    public DropContainer RaycastDropContainer()
    {
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        rayCaster.Raycast(eventData, results);

        foreach (RaycastResult result in results)
        {
            DropContainer container = result.gameObject.GetComponent<DropContainer>();
            if (container != null)
            {
                return container;
            }
        }

        return null;
    }
}

