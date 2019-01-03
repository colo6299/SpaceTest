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

    public Basic Primary;
    public Basic Secondary;

	// Update is called once per frame
	void Update ()
    {
        IsPrimaryFiring = false;
        IsSecondaryFiring = false;

        if (Input.GetKey(KeyCode.Space))
        {
            IsPrimaryFiring = true;
        }

        if (Input.GetKey(KeyCode.R))
        {
            IsSecondaryFiring = true;
        }

        if (Primary != null)
        {
            Primary.IsFireing = IsPrimaryFiring;
        }

        if (Secondary != null)
        {
            Secondary.IsFireing = IsSecondaryFiring;
        }
    }
}
