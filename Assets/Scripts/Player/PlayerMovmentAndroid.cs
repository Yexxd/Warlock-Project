using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovmentAndroid : MonoBehaviourPun
{
    Vector3 targetPoint;
    public Transform model;
    public bool isMine = true;
    bool isWalking;
    WarlockPlayer w;

    FixedJoystick joy;

    private void Awake()
    {
        transform.rotation = Quaternion.identity;
        model.transform.localRotation = transform.rotation;

        targetPoint = transform.position;
        w = GetComponent<WarlockPlayer>();
        joy = FindObjectOfType<FixedJoystick>();
    }

    void Update()
    {
        GetComponentInChildren<Animator>().SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        if(isMine && w.canMove)
        {
            transform.Translate(joy.Horizontal*Time.deltaTime*5,0, joy.Vertical*Time.deltaTime*5);
            model.LookAt(transform.position + new Vector3(joy.Horizontal, 0, joy.Vertical));
            isWalking = joy.Direction.magnitude>0;
        }
    }

    public void LookTo(Vector3 point)
    {
        model.LookAt(point);
    }

}
