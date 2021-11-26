using System;
using System.Collections.Generic;
using UnityEngine;

public class Teset : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            gameObject.AddComponent(Type.GetType("Inmolate"));
        }
    }
}
