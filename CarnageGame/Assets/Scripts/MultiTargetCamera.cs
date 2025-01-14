﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCamera : MonoBehaviour
{
    public float smoothTime;
    public float minFOV;
    public float maxFOV;
    public float sizeNormaliser;
    public Vector3 offset;
    public List<Transform> targets;

    private Camera cam;

    private Vector3 movVelocity;
    private float zoomVelocity;
    private float fovScalar = 1f;
    
    private void Start()
    {
        cam = GetComponent<Camera>();

        targets.Add(GameObject.FindGameObjectWithTag("PlayerOne").transform);
        targets.Add(GameObject.FindGameObjectWithTag("PlayerTwo").transform);
        targets.Add(GameObject.FindGameObjectWithTag("PlayerThree").transform);
        targets.Add(GameObject.FindGameObjectWithTag("PlayerFour").transform);
    }

    private void FixedUpdate()
    { 
        if (targets.Count == 0) { return; }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);

        Move(ref bounds);
        Zoom(bounds);
    }

    private void Move(ref Bounds bounds)
    {
        Vector3 centrePoint = GetCentrePoint(ref bounds);
        Vector3 newPos = centrePoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref movVelocity, smoothTime);
    }

    private Vector3 GetCentrePoint(ref Bounds bounds)
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        foreach (Transform t in targets)
        {
            bounds.Encapsulate(t.position);
        }

        if (targets[0] == null)
        {
            bounds.Encapsulate(targets[1].position);
        }

        return bounds.center;
    }

    private void Zoom(Bounds bounds)
    {
        if (targets.Count != 1)
        {
            fovScalar = Mathf.SmoothDamp(fovScalar, bounds.size.magnitude / sizeNormaliser, ref zoomVelocity, smoothTime);
        }
        else
        {
            fovScalar = Mathf.SmoothDamp(fovScalar, .5f, ref zoomVelocity, smoothTime);
        }
        cam.fieldOfView = Mathf.Lerp(minFOV, maxFOV, fovScalar);
    }
}
