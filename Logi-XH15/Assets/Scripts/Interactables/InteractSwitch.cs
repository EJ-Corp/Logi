using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSwitch : Interactable
{
    public override void OnFocus()
    {
        Debug.Log("Looking at switch " + transform.name);
    }

    public override void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public override void OnLoseFocus()
    {
        throw new System.NotImplementedException();
    }

  
}
