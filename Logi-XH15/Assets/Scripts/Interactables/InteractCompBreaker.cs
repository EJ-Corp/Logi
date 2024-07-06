using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCompBreaker : Interactable
{
    [SerializeField] private bool isOn = true;
    [SerializeField] private AudioClip[] switchSFX;
    [SerializeField] private Animator animator;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        
        SFXManager.Instance.PlayRandomSFXClip(switchSFX, transform, 1f);
        if (isOn) //Turning Computer Off
        {
            Debug.Log("Turned off");
            GameManager.Manager.ToggleComputer(true);
            isOn = false;

            //XAVIER add animation here from ON to OFF
            animator.SetTrigger("Off");


        } else //Turning COmputer On
        {
            Debug.Log("Turned on");
            GameManager.Manager.ToggleComputer(false);
            isOn = true;

            //XAVIER add animation here from OFF to ON
            animator.SetTrigger("On");
        }

        
    }

    public override void OnLoseFocus()
    {
        
    }

    // Start is called before the first frame update
    public void FlareBreak()
    {
        isOn = false;
        animator.SetTrigger("Off");
        //XAVIER add animation here from ON to OFF

    }
}
