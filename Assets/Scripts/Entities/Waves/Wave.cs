using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float difficulty;
    public float endTime;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        
    }

    private void SpawnWave()
    {
        SquadInfo sd = EnemyHolder.RandomSquad(1, 1);
        foreach (int id in sd.trash)
        {
            Instantiate(EnemyHolder.staticTrashArray[id],
                new Vector3(Random.value, Random.value, Random.value) * 1000,
                transform.rotation, null
                );
        }
        foreach (int id in sd.normal)
        {
            Instantiate(EnemyHolder.staticNormalArray[id],
                new Vector3(Random.value, Random.value, Random.value) * 1000,
                transform.rotation, null
                );
        }
        foreach (int id in sd.elite)
        {
            Instantiate(EnemyHolder.staticEliteArray[id],
                new Vector3(Random.value, Random.value, Random.value) * 1000,
                transform.rotation, null
                );
        }
    }
}
