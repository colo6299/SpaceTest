using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles weapons control from users
/// 
/// Switching primaries and such
/// </summary>
public class FireCoordinator : MonoBehaviour
{
    public bool IsFiring { get; private set; }

    // The current selected weapon (assuming more than one type of weapon is)
    public MonoBehaviour Selected;

	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            IsFiring = true;
        }
        else
        {

        }


    }
}
