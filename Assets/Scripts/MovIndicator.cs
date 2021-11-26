using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > 0)
            transform.localScale -= Vector3.one * 2 * Time.deltaTime;
        if(Input.GetMouseButtonDown(1))
            Destroy(gameObject);
    }
}
