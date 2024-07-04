using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, rotationSpeed*Time.deltaTime);
    }
}
