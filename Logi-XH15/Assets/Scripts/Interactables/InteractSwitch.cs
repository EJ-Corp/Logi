using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSwitch : Interactable
{
    [SerializeField] private Transform switchBody;
    [SerializeField] private bool isOn = true;
    [SerializeField] private Vector3 onRotation = new Vector3(13, 0, 0);
    [SerializeField] private Vector3 offRotation = new Vector3(100, 0, 0);
    [SerializeField] private AudioClip[] switchSFX;
    
    void Start()
    {
        GetComponentInChildren<Outline>().enabled = false;
        switchBody = transform.Find("Switch");
    }
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        GameManager.Manager.soundManager.PlayRandomSFXClip(switchSFX, transform, 1f);
        if(isOn)
        {
            switchBody.Rotate(onRotation, Space.Self);
            GameManager.Manager.switches.SwitchTrigger(false);
        } else 
        {
            switchBody.Rotate(offRotation, Space.Self);
            GameManager.Manager.switches.SwitchTrigger(true);
        }

        isOn = !isOn;
    }

    public override void OnLoseFocus()
    {
        GetComponentInChildren<Outline>().enabled = false;
    }

    public void Break()
    {
        if(isOn)
        {
            switchBody.Rotate(onRotation, Space.Self);
            isOn = false;
            GameManager.Manager.switches.SwitchTrigger(false);
        }
        
    }

  
}
