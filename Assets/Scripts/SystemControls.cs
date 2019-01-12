using System;
using UnityEngine;

public class SystemControls : MonoBehaviour
{
    public enum HudStates { Playing, Menu, Pause, WaveEnd }

    public static event Action<HudStates> HudStateChange;

    public static event Action ClickDown;
    public static event Action ClickUp;

    // hud controls

    // Ship controls

    // Weapon controls

    public HudStates HudState = HudStates.Playing;

    void Update()
    {
        if (HudState == HudStates.Playing || HudState ==  HudStates.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                HudState = HudStates.Menu;

                if (HudStateChange != null)
                {
                    HudStateChange.Invoke(HudState);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                HudState = HudStates.Playing;

                if (HudStateChange != null)
                {
                    HudStateChange.Invoke(HudState);
                }
            }
        }

        if (HudState == HudStates.Menu)
        {

            if (Input.GetMouseButtonDown(0))
            {
                if (ClickDown != null)
                {
                    ClickDown.Invoke();
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (ClickUp != null)
                {
                    ClickUp.Invoke();
                }
            }

        }
    }

}
