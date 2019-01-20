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

    private bool spawnHold = false;

    void Start()
    {
        inWave = true;
    }

    void Update()
    {

        if (inWave && !spawning)
        {
            InvokeRepeating("SpawnSquad", 0, spawnRate);
            spawning = true;
            endTime = Time.time + waveLength;
            //InvokeRepeating("WaveEnder", , 5);
        }

        if (Time.time > endTime)
        {
            if (spawning)
            {
                InvokeRepeating("WaveEnder", 1, 5);
                CancelInvoke("SpawnSquad");
            }

            else
            {
                endTime = Time.time + waveLength;
                waveNumber++;
                inWave = true;
                coolDown = false;
            }
        }

        if (!spawning)
        {
            CancelInvoke("SpawnSquad");
        }        
    }

    private void WaveEnder()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            inWave = false;
            CancelInvoke("WaveEnder");
            endTime = Time.time + coolTime;
            coolDown = true;
            spawning = false;
        }
    }

    private void SpawnSquad()
    {
        SquadInfo sd = EnemyHolder.RandomSquad(difficulty, waveNumber);
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
