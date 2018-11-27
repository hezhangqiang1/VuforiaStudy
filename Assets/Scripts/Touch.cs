using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

	
	void Start () {
		
	}
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                //双击
                if(Input.touchCount==1&&Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if(Input.GetTouch(0).tapCount==2)
                    Destroy(hit.collider.gameObject);
                }
            }
        }
	}
}
