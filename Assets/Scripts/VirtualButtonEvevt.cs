/*
 * 实现VirtualButton
 *
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VirtualButtonEvevt : MonoBehaviour, IVirtualButtonEventHandler {

    VirtualButtonBehaviour [] vbs;
    public GameObject cube;
    private bool IsRotion;
    void Start () {
        vbs = this.GetComponentsInChildren<VirtualButtonBehaviour>();
        for(int i = 0; i< vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
	}
	
	
	void Update () {
        if (IsRotion)
        {
            cube.transform.Rotate(Vector3.up, 60f, Space.World);
        }
	}

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "Rotate":
                IsRotion = true;
                break;
            case "Large":
                cube.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
                break;
               
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "Rotion":
                IsRotion = false ;
                break;
            case "Large":
                break;

        }

    }
}
