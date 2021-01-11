﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    public Camera camera;
    public float maximunLength;


    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 dir;
    private Quaternion rotation;



    // Update is called once per frame
    void Update()
    {
        if (camera != null)
        {
            RaycastHit hit;
            var mousePos = Input.mousePosition;
            rayMouse = camera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximunLength))
            {
                RotateToMouseDorection(gameObject, hit.point);
            }
            else
            {
                var pos = rayMouse.GetPoint(maximunLength);
                RotateToMouseDorection(gameObject, pos);
            }
        }
        else
        {
            Debug.Log("No camera");
        }

    }

    void RotateToMouseDorection(GameObject obj, Vector3 destination)
    {
        dir = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(dir);
        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }

    public Quaternion GetRotation(){
        return rotation;
    }
}
