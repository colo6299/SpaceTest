using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HudCoordinator : MonoBehaviour
{

    public bool IsPendingItemsBarExtended;

    public void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            IsPendingItemsBarExtended = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            IsPendingItemsBarExtended = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}

