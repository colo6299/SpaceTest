using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfo : MonoBehaviour {


    public float Health = 100;
    public float Armor = 5;

    public float Acceleration = 10f;
    public float MaxSpeed = 20f;
    public float SkidRatio = 0.8f;

    public GameObject dmgPrefab;

    
    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (Health <= 0)
        {
            Destroy(gameObject);
            Destroy(Instantiate(dmgPrefab, transform.position, transform.rotation, null), 5f);
        }
    }







}
