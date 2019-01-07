using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    /// <summary>
    /// The items power level
    /// I wouldn't recommend setting this below zero
    /// </summary>
    public int PowerLevel = 0;

    public RollInfo RollInfo;

    private int rollIterator;
    private float[] rollArray;

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

}
