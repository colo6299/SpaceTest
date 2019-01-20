using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float difficulty = 1;
    public float waveLength = 60;
    public float spawnRate = 20;
    public float coolTime = 15;
    public int waveNumber;
    
    private float endTime;
    private bool inWave = false;
    private bool spawning = false;
    private bool coolDown = false;

    public static List<GameObject> spawned = new List<GameObject>();

    void Start()
    {
        inWave = true;
    }

    void Update()
    {

        spawned.TrimExcess();
        Debug.Log(spawned.Count);

        if (inWave && !spawning)
        {
            InvokeRepeating("SpawnSquad", 0, spawnRate);
            spawning = true;
            endTime = Time.time + waveLength;
        }

        if (Time.time > endTime)
        {
            if (spawning)
            {
                endTime = Time.time + coolTime;
                coolDown = true;
                inWave = false;
                spawning = false;
            }

            else
            {
                endTime = Time.time + waveLength;
                waveNumber++;
                spawning = true;
                inWave = true;
                coolDown = false;
            }


        }

        if (!spawning)
        {
            CancelInvoke("SpawnSquad");
        }        
    }

    private void SpawnSquad()
    {
        SquadInfo sd = EnemyHolder.RandomSquad(difficulty, waveNumber);
        foreach (int id in sd.trash)
        {
            spawned.Add(Instantiate(EnemyHolder.staticTrashArray[id],
                new Vector3(Random.value, Random.value, Random.value) * 1000,
                transform.rotation, null
                ));
        }
        foreach (int id in sd.normal)
        {
            spawned.Add(Instantiate(EnemyHolder.staticNormalArray[id],
                new Vector3(Random.value, Random.value, Random.value) * 1000,
                transform.rotation, null
                ));
        }
        foreach (int id in sd.elite)
        {
            spawned.Add(Instantiate(EnemyHolder.staticEliteArray[id],
                new Vector3(Random.value, Random.value, Random.value) * 1000,
                transform.rotation, null
                ));
        }
    }
}
