using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour {

    public GameObject[] trashArray;
    public static GameObject[] staticTrashArray;

    public GameObject[] normalArray;
    public static GameObject[] staticNormalArray;

    public GameObject[] eliteArray;
    public static GameObject[] staticEliteArray;

    public static float trashFrac = 0.6f;
    public static float normalFrac = 0.3f;
    public static float eliteFrac = 0.1f;
    public static float difficultyScalar = 2;

    void Awake()
    {
        staticTrashArray = trashArray;
        staticNormalArray = normalArray;
        staticEliteArray = eliteArray;
    }

    public static SquadInfo RandomSquad(float difficulty, int waveNumber)
    {
        int total = (int)(Mathf.Sqrt(waveNumber) * 10);

        int t = 0;
        int n = 0;
        int e = 0;

        int ttl = total;

        while (t + n + e < total)
        {
            if (Random.value < eliteFrac)
            {
                e++;
                ttl--;
            }
            else if (Random.value < normalFrac)
            {
                n++;
                ttl--;
            }
            else if (Random.value < trashFrac)
            {
                t++;
                ttl--;
            }

        }

        return new SquadInfo(
            TrashGroup(difficulty, t),
            NormalGroup(difficulty, n),
            EliteGroup(difficulty, e)
            );
    }

    private static int[] TrashGroup(float difficulty, int number)
    {
        int[] group = new int[number + (int)(difficulty * difficultyScalar)];
        for (int i = 0; i < group.Length; i++)
        {
            group[i] = Random.Range(0, staticTrashArray.Length);
        }
        return group;
    }

    private static int[] NormalGroup(float difficulty, int number)
    {
        int[] group = new int[number + (int)(difficulty * difficultyScalar)];
        for (int i = 0; i < group.Length; i++)
        {
            group[i] = Random.Range(0, staticNormalArray.Length);
        }
        return group;
    }

    private static int[] EliteGroup(float difficulty, int number)
    {
        int[] group = new int[number + (int)(difficulty * difficultyScalar)];
        for (int i = 0; i < group.Length; i++)
        {
            group[i] = Random.Range(0, staticEliteArray.Length);
        }
        return group;
    }



}

