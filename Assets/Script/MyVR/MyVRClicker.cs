using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVRClicker : MonoBehaviour {

    RaycastHit rh;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics.Raycast(new Ray (gameObject.transform.position, gameObject.transform.forward),out rh,200f))
        {
            rh.collider.gameObject.transform.Rotate(1f, 0.8f, 0.2f);
        }
	}
}
