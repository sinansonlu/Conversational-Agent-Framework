using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetAutoMove : MonoBehaviour {

    private int currentIndex;
    private float factor;

    private Vector3[] targets;

    private float timer;

    public bool active;

    void Start () {
        factor = 0;
        timer = 0;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("IcoLoc");
        targets = new Vector3[objs.Length];
        for(int i = 0; i < objs.Length; i++)
        {
            targets[i] = objs[i].transform.position;
        }

        if (targets != null && targets.Length > 0)
        {
            currentIndex = Random.Range(0, targets.Length);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targets[currentIndex], Time.deltaTime);
            timer += Time.deltaTime;
            if(timer > 1.5f)
            {
                currentIndex = Random.Range(0, targets.Length);
                timer = 0;
            }
        }
    }
}
