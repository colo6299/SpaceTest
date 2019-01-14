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
        float total = Mathf.Sqrt(waveNumber) * 10;
        return new SquadInfo(
            TrashGroup(difficulty, (int)(total * trashFrac)),
            NormalGroup(difficulty, (int)(total * normalFrac)),
            EliteGroup(difficulty, (int)(total * eliteFrac))
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

