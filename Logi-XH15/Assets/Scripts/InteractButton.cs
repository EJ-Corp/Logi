using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : Interactable
{
    [SerializeField] private Animator buttonClick;
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        buttonClick.SetTrigger("IsClicked");
        
    }
    public override void OnFocus()
    {
        Debug.Log("Looking at " + gameObject.name);
        print("Looking at " + gameObject.name);
    }
    public override void OnLoseFocus()
    {
        Debug.Log("Stopped looking at " + gameObject.name);
    }

    
}
