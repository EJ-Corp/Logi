using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform target;
    public float rotateYSpeed;
    public float rotateXSpeed;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, rotateYSpeed * Time.deltaTime);
        transform.RotateAround(target.position, Vector3.right, rotateXSpeed * Time.deltaTime);
    }
}
