using UnityEngine;

public class PanelPendingItems : MonoBehaviour
{
    public void Awake()
    {
        transform.position = new Vector3(-25, transform.position.y, transform.position.z);
        SystemControls.HudStateChange += OnHudChange;
    }

    private void OnHudChange(SystemControls.HudStates state)
    {
        if (state == SystemControls.HudStates.Menu)
        {
            transform.position = new Vector3(42, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(-25, transform.position.y, transform.position.z);
        }
    }
}