using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeder : MonoBehaviour
{
    public Animator anim;

    void Update()
    {
        anim.speed = 0.4f;
    }
}
