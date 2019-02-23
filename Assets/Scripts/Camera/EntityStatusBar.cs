using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityStatusBar : MonoBehaviour
{
    public GameObject Health;
    public GameObject Energy;

    public void UpdateBars(Entity Entity)
    {
        if (Health != null)
            Health.transform.localScale = new Vector3((Entity.Health.Value / Entity.Health.Max), 1, 1);

        if (Energy != null)
            Energy.transform.localScale = new Vector3((Entity.Energy.Value / Entity.Health.Max), 1, 1);
    }
}
