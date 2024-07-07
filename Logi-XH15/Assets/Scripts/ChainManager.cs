using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainManager : Interactable
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject anchor;
    [SerializeField] private Rigidbody rb;
    public override void OnInteract()
    {
        rb.isKinematic = true;
        this.transform.parent = player.transform;
    }
    public override void OnStopInteract()
    {
        rb.isKinematic = false;
        this.transform.parent = anchor.transform;
    }
    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {

    }
}
