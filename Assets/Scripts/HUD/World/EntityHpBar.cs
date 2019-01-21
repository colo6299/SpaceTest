using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHpBar : MonoBehaviour {

    public Image HitPoints;

    public void UpdateBar(float current, float max)
    {
        if (max != 0)
        {
            UpdateBar(current / max);
        }
    }

    public void UpdateBar(float scaler)
    {
        HitPoints.transform.localScale = new Vector3(scaler, 1, 1);
    }
}
