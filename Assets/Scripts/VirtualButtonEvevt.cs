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

public class VirtualButtonEvevt : MonoBehaviour ,IVirtualButtonEventHandler{
   

   
    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        throw new NotImplementedException();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        throw new NotImplementedException();
    }
}
