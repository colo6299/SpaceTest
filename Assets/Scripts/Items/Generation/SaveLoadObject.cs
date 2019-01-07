using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadObject : MonoBehaviour {

    public RollInfo roll;
    public string slot;
    public PrefabHolder dm;

    void Start()
    {
        dm = GameObject.FindGameObjectWithTag("DungeonMaster").GetComponent<PrefabHolder>();
        DebugGen();
    }

    void DebugGen()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //Instantiate(dm.prefabArray[Random.Range(0, dm.prefabArray.Length)]);
        GameObject go = Instantiate(dm.prefabArray[0], transform);

        //yeah there's a better way to do this
        go.GetComponent<Minigun>().RollStats(new RollInfo(BaseRoller.RollInitiative(100)));
    }

    public void SaveSlot()
    {
        
    }

    public void LoadSlot()
    {

    }

}
