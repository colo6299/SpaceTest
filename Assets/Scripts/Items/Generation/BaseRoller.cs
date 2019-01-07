using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoller {

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
}
