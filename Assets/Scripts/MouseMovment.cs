using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovment : MonoBehaviour
{
    GameObject movIndicator;
    Vector3 targetPoint;
    bool moving;
    public float movSpeed;
    public bool canMove;
    public Animator anim;
    public Transform model;

    private void Awake()
    {
        movIndicator = Resources.Load("MovIndicator") as GameObject;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canMove)
            if (MouseRoutines.Screen2Plane(out Vector3 point))
            {
                Instantiate(movIndicator, point, movIndicator.transform.rotation);
                targetPoint = point;
                model.LookAt(point);
                moving = true;
            }
        anim.SetBool("isWalking", moving);
    }

    public void Stop()
    {
        moving = false;
        targetPoint = transform.position;
    }

    private void FixedUpdate()
    {
        if (moving && canMove)
            if ((targetPoint - transform.position).magnitude > 0.1f)
                transform.Translate((targetPoint - transform.position).normalized * Time.fixedDeltaTime * movSpeed);
            else
                moving = false;
    }
}
    