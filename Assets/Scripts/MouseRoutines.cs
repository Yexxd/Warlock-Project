using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRoutines : MonoBehaviour
{
    static Plane plane = new Plane(Vector3.up, 0);
    static GameObject movIndicator;

    private void OnEnable()
    {
        movIndicator = Resources.Load("MovIndicator") as GameObject;
    }

    static public void MovIndicator(Vector3 point)
    {
        Instantiate(movIndicator, point, movIndicator.transform.rotation);
    }

    static public bool Screen2Plane(out Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        point = Vector3.zero;
        if (plane.Raycast(ray, out float distance))
        {
            point = ray.GetPoint(distance);
            return true;
        }
        return false;
    }
}
