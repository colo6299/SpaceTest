using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShipInfo : MonoBehaviour {

    public int MaxHitPoints = 200;
    public float HitPoints { get; private set; }

    private void Start()
    {
        HitPoints = MaxHitPoints;
    }

    public void DoDamage(float damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }


}
