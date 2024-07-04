using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSwitch : Interactable
{
    [SerializeField] private Transform switchBody;
    [SerializeField] private bool isOn = true;
    [SerializeField] private Vector3 onRotation = new Vector3(13, 0, 0);
    [SerializeField] private Vector3 offRotation = new Vector3(100, 0, 0);
    
    void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
        switchBody = transform.Find("Switch");
    }
    public override void OnFocus()
    {
        Debug.Log("Looking at switch " + transform.name);
        //GetComponentInChildren<Outline>().enabled = true;
    }

    public override void OnInteract()
    {
        if(isOn)
        {
            switchBody.Rotate(onRotation, Space.Self);
        } else 
        {
            switchBody.Rotate(offRotation, Space.Self);
        }

        isOn = !isOn;
    }

    public override void OnLoseFocus()
    {
        GetComponentInChildren<Outline>().enabled = false;
    }

    public void Break()
    {
        switchBody.Rotate(onRotation, Space.Self);
        isOn = false;
    }

  
}
