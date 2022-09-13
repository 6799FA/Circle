using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemScript : Planet
{
    [SerializeField] Planet[] sons;
    
    protected override void Start()
    {
        for (int i = 0; i < sons.Length; i++)
            sons[i].planetNum = planetNum;

        originPos = transform.position;
    }
}
