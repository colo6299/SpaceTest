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

    public bool IsPrimaryFiring = false;
    public bool IsSecondaryFiring = false;

	// Update is called once per frame
	void Update ()
    {
        IsPrimaryFiring = false;
        IsSecondaryFiring = false;

        if (Input.GetMouseButton(0))
        {
            IsPrimaryFiring = true;
        }

        if (Input.GetMouseButton(1))
        {
            IsSecondaryFiring = true;
        }
    }
}
