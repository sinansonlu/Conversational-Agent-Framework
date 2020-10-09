using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCam : MonoBehaviour {

    float camx;
    float camy;

    bool gyroCamEnable;

	void Start () {
        camx = 0f;
        camy = 0f;
        Input.gyro.enabled = true;
	}
	
	void Update () {
        if(gyroCamEnable)
        {
            camx += -Input.gyro.rotationRateUnbiased.x;
            camy += -Input.gyro.rotationRateUnbiased.y;
            transform.localRotation = Quaternion.Euler(camx, camy, 0);
        }
    }

    public void ToggleGyro()
    {
        gyroCamEnable = !gyroCamEnable;
    }
}
