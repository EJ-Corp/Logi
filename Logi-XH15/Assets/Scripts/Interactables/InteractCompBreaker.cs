using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCompBreaker : Interactable
{
    [SerializeField] private bool isOn = true;
    [SerializeField] private AudioClip[] switchSFX;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        isOn = !isOn;
        SFXManager.Instance.PlayRandomSFXClip(switchSFX, transform, 1f);
    }

    public override void OnLoseFocus()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
