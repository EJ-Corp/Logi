using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : MonoBehaviour
{
    [SerializeField] private TestDummy test;
    [SerializeField] private Transform anchor;
    // Update is called once per frame
    void Update()
    {
        if(test.isGrabbing)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = test.transform;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            transform.parent = anchor;
        }
    }
}
