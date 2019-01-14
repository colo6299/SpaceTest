using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadInfo {

    public int[] trash;
    public int[] normal;
    public int[] elite;

    public SquadInfo(int[] _trash, int[] _normal, int[] _elite)
    {
        trash = _trash;
        normal = _normal;
        elite = _elite;
    }

}
