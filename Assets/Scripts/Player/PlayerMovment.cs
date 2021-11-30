using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovment : MonoBehaviourPun
{
    Vector3 targetPoint;
    public Transform model;
    public bool isMine = true;
    bool isWalking;
    WarlockPlayer w;

    private void Awake()
    {
        model.transform.localRotation = transform.rotation;
        transform.rotation = Quaternion.identity;

        targetPoint = transform.position;
        w = GetComponent<WarlockPlayer>();
    }

    void Update()
    {
        if (isMine && Input.GetMouseButtonDown(1) && w.canMove)
            if (MouseRoutines.Screen2Plane(out Vector3 point))
            {
                MouseRoutines.MovIndicator(point);
                model.LookAt(point);
                photonView.RPC(nameof(MoveTo), RpcTarget.AllViaServer,point);
            }

        GetComponentInChildren<Animator>().SetBool("isWalking", isWalking);
    }

    private void FixedUpdate()
    {
        if(isWalking)
            if ((targetPoint - transform.position).magnitude > 0.1f)
                transform.Translate((targetPoint - transform.position).normalized*Time.deltaTime*5, Space.World);
            else
                isWalking = false;
    }

    public void LookTo(Vector3 point)
    {
        model.LookAt(point);
        photonView.RPC(nameof(Stop), RpcTarget.AllViaServer);
    }

    [PunRPC]
    void Stop()
    {
        targetPoint = transform.position;
        isWalking = false;
    }

    [PunRPC]
    void MoveTo(Vector3 point)
    {
        model.LookAt(point);
        targetPoint = point;
        transform.Translate((targetPoint - transform.position).normalized * Time.deltaTime * 6, Space.World);
        isWalking = true;
    }
}
