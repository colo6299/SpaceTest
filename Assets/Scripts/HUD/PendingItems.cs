using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendingItems : MonoBehaviour
{
    private HudCoordinator coordinator;

    public void Start()
    {
        coordinator = gameObject.GetComponentInParent<HudCoordinator>();
    }

    public void Update()
    {
        if (coordinator.IsPendingItemsBarExtended)
        {
            transform.position = new Vector3(42, transform.position.y, transform.position.z);

            foreach (CanvasGroup cg in gameObject.GetComponentsInChildren<CanvasGroup>())
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }

        }
        else
        {
            transform.position = new Vector3(-25, transform.position.y, transform.position.z);

            foreach (CanvasGroup cg in gameObject.GetComponentsInChildren<CanvasGroup>())
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }
    }
}
