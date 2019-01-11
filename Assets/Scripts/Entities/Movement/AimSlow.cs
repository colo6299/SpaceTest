using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSlow : MonoBehaviour {

    public float slowRatio = 0.5f;
    public float slowSpeed = 0.5f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Time.timeScale - slowSpeed * Time.fixedUnscaledDeltaTime <= slowRatio)
            {
                Time.timeScale = slowRatio;
            }
            else
            {
                Time.timeScale -= slowSpeed * Time.fixedUnscaledDeltaTime;
            }
        }
        else if (Time.timeScale < 1f)
        {
            if (Time.timeScale + slowSpeed * Time.fixedUnscaledDeltaTime > 1)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale += slowSpeed * Time.fixedUnscaledDeltaTime;
            }
            
        } 
    }
}
