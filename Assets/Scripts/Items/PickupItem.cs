using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PickupItem : MonoBehaviour {

    public RollInfo roll;
    public GameObject slot;
    public Transform pending;

    private Item go;
    private Collider gg;

    void Start()
    {
        pending = GameObject.FindGameObjectWithTag("UI_Pending").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gg = other;

            go = BaseRoller.InstantiateItem(roll, other.transform.position, other.transform.rotation, other.transform.GetChild(0)).GetComponent<Item>();
            go.gameObject.SetActive(false);

            GameObject dg = Instantiate(slot, pending.position, pending.rotation, pending);
            dg.name = "Slot";

            GameObject item = Instantiate(PrefabHolder.staticIconArray[roll.prefabID].icon, dg.transform.position, dg.transform.rotation, dg.transform);
            item.tag = Constants.Tag_UI_Item;
            Instantiate(PrefabHolder.staticTierArray[(int)go.Rarity], item.transform.position, item.transform.rotation).transform.SetParent(item.transform);

            DragContainer dragContainer = dg.GetComponentInChildren<DragContainer>();
            dragContainer.Item = go;

            Destroy(gameObject);
        }
    }



}
