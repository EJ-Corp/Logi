using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LineConnector : MonoBehaviour
{
    //Draws the lines between joints
    [SerializeField] private GameObject[] joints;
    [SerializeField] private LineRenderer lineRenderer;
    
    void Start()
    {
        lineRenderer.SetPosition(0, this.transform.position);
    }
    void Update()
    {
        for(int i=0; i < joints.Length; i++)
        {
            lineRenderer.SetPosition(i + 1, joints[i].transform.position);
        }
    }
}
