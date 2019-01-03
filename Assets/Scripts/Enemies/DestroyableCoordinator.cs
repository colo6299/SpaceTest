using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableCoordinator : MonoBehaviour {


    public float health = 100;
    public GameObject dmgPrefab;

    
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(Instantiate(dmgPrefab, transform.position, transform.rotation, null), 5f);
        }
    }







}
