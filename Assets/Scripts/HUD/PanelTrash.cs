using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTrash : MonoBehaviour
{

    private HudCoordinator hud;

	// Use this for initialization
	void Start () {
        hud = GetComponentInParent<HudCoordinator>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
