using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PickupItem : MonoBehaviour {

    public RollInfo roll;
    public GameObject slot;
    public Transform pending;

    private GameObject go;
    private Collider gg;
    private bool s = false;

    void Start()
    {
        pending = GameObject.FindGameObjectWithTag("UI_Pending").transform;
    }

    void Update()
    {
        if (s)
        {
            gg.GetComponentInChildren<LoadoutManager>().SlotItem(go);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gg = other;
            //somewhat better now
            //use this one for really -> //BaseRoller.InstantiateItem(roll, other.transform.position, other.transform.rotation, other.transform.GetChild(0)).SetActive(false);
            go = BaseRoller.InstantiateItem(roll, other.transform.position, other.transform.rotation, other.transform.GetChild(0));
            go.SetActive(false);

            GameObject dg = Instantiate(slot, pending.position, pending.rotation, pending);           
            Instantiate(PrefabHolder.staticIconArray[roll.prefabID].icon, dg.transform.position, dg.transform.rotation, dg.transform).tag = PrefabHolder.staticIconArray[roll.prefabID].type;
            dg.GetComponentInChildren<DragContainer>().item = go;
            //dg.transform.GetChild(0).tag = PrefabHolder.staticIconArray[roll.prefabID].type;
            //dg.transform.GetChild(0).GetComponent<Renderer>() = PrefabHolder.staticIconArray[roll.prefabID].icon;
            dg.name = "Slot";

            //s = true;
            Destroy(gameObject);
        }
    }



}
