using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoller {

    

    /// <summary>
    /// Chance to roll a common rarity or higher.
    /// </summary>
    private static float commonChance = 1f;

    /// <summary>
    /// Chance to roll a uncommon rarity or higher.
    /// </summary>
    private static float uncommonChance = 0.3f;

    /// <summary>
    /// Chance to roll a rare rarity or higher.
    /// </summary>
    private static float rareChance = 0.1f;

    /// <summary>
    /// Chance to roll a legendary rarity or higher.
    /// </summary>
    private static float legendaryChance = 0.04f;

    /// <summary>
    /// Chance to roll an ultimate rarity or higher.
    /// </summary>
    private static float ultimateChance = 0.01f;

    /// <summary>
    /// Chance to roll a curated item.
    /// Overrides other rolls.
    /// </summary>
    private static float uniqueChance = 0.01f;



    /// <summary>
    /// Returns a randomized float[] rollArray
    /// </summary>
    /// <param name="gamePower"></param>
    /// <returns></returns>
    public static float[] RollInitiative(int gamePower)
    {
        float[] rollArray = new float[10];

        //this is the power roll, should probably be weighted by rarity, but idgaf about that right now
        rollArray[0] = Mathf.Clamp(gamePower + (50 * Random.Range(-1f, 1f)), 10, Mathf.Infinity);

        //look kevin, it's about being *verbose* alright?
        //     ...and yes I cut it to [9] so it'd be aligned nicely
        rollArray[1] = Random.Range(-1f, 1f);
        rollArray[2] = Random.Range(-1f, 1f);
        rollArray[3] = Random.Range(-1f, 1f);
        rollArray[4] = Random.Range(-1f, 1f);
        rollArray[5] = Random.Range(-1f, 1f);
        rollArray[6] = Random.Range(-1f, 1f);
        rollArray[7] = Random.Range(-1f, 1f);
        rollArray[8] = Random.Range(-1f, 1f);
        rollArray[9] = Random.Range(-1f, 1f);

        return rollArray; 
    }

    /// <summary>
    /// Returns a RollItem representing a random item.
    /// </summary>
    /// <returns></returns>
    public static RollInfo RollItem(int gamePower)
    {
        RollInfo roll = new RollInfo(RollInitiative(gamePower));
        roll.rarity = ChooseRarity();
        roll.prefabID = Random.Range(0, PrefabHolder.staticItemArray.Length);
        return roll; 
    }

    private static Rarity ChooseRarity()
    {
        Rarity rarity = Rarity.Common;
        float rareRoll = Random.value;

        //there's a good way to make this expandable, but it's not like it matters
        if (rareRoll <= ultimateChance) { rarity = Rarity.Ultimate; }
        else if (rareRoll <= legendaryChance) { rarity = Rarity.Legendary; }
        else if (rareRoll <= rareChance) { rarity = Rarity.Rare; }
        else if (rareRoll <= uncommonChance) { rarity = Rarity.Uncommon; }
        else if (rareRoll <= commonChance) { rarity = Rarity.Common; }

        return rarity;
    }

    /// <summary>
    /// Instantiates and returns item associated with RollInfo "info"
    /// </summary>
    /// <param name="info"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstantiateItem(RollInfo info, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject prefab = PrefabHolder.staticItemArray[info.prefabID];
        GameObject gen = GameObject.Instantiate(prefab, position, rotation, parent);
        gen.GetComponent<IItem>().RollStats(info);

        return gen;
    }

    /// <summary>
    /// Instantiates and returns random item.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject InstantiateRandomItem(int power, Vector3 position, Quaternion rotation, Transform parent)
    {
        return InstantiateItem(RollItem(power), position, rotation, parent);
    }
}
