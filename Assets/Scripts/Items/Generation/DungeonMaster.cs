using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour {

    /// <summary>
    /// Instantiates and returns item associated with RollInfo item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstantiateItem(RollInfo item, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject prefab = PrefabHolder.staticItemArray[item.prefabID];
        GameObject gen = Instantiate(prefab, position, rotation, parent);
        gen.GetComponent<IItem>().RollStats(item);

        return gen;
    }
}
