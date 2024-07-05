using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestDummy : MonoBehaviour
{
    public bool isGrabbing;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isGrabbing = !isGrabbing;
        }
    }

    public void ReleaseGrab()
    {
        isGrabbing = false;
    }
}
