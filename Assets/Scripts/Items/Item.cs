using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum SlotType { Primary, Secondary, Armor, Engine, None}
public enum Rarity { Common, Uncommon, Rare, Legendary, Ultimate, Unique, None };

public class Item : MonoBehaviour
{
    public SlotType Type;
    public Rarity Rarity;

    /// <summary>
    /// The items power level
    /// I wouldn't recommend setting this below zero
    /// </summary>
    public int PowerLevel = 0;

    public RollInfo RollInfo;

    private int rollIterator;
    private float[] rollArray;


    public SlotType Slot()
    {
        return Type;
    }

    public virtual void RollStats(RollInfo info)
    {
        Rarity = info.rarity;
        StartRolling(info);
        PowerLevel = Mathf.RoundToInt(Roll() * RarityMultiplyer());
    }

    protected float Roll()
    {
        rollIterator++;
        return rollArray[rollIterator];
    }

    protected void StartRolling(RollInfo _Info)
    {
        RollInfo = _Info;
        rollArray = _Info.rollArray;
        rollIterator = -1;
    }

    public float RarityMultiplyer()
    {
        switch (Rarity)
        {
            case Rarity.Uncommon:
                return 1.10f;
            case Rarity.Rare:
                return 1.20f;
            case Rarity.Legendary:
                return 1.30f;
            case Rarity.Ultimate:
                return 1.50f;
            default:
                return 1;
        }
    }
}
