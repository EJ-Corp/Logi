using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractButton : Interactable
{
    [SerializeField] private Animator buttonClick;
    [SerializeField] private bool canFix = false;
    [SerializeField] private string id;
    [SerializeField] public string ID
    {
        get { return id; }
    }
    [SerializeField] private AudioClip[] buttonSFX;
    [SerializeField] private AudioClip[] correctSFX;
    [SerializeField] private AudioClip[] wrongSFX;
    private ProblemHandler warningSign; 
    [SerializeField] private Renderer buttonLight;
    [SerializeField] private Material[] buttonLightMaterial;

    void Start()
    {
        if(buttonLight != null)
        {
            buttonLight.sharedMaterial = buttonLightMaterial[0];
        }
    }

    public override void OnInteract()
    {
        buttonClick.SetTrigger("IsClicked");
        SFXManager.Instance.PlayRandomSFXClip(buttonSFX, transform, 1f);
        if(canFix)
        {
            FixProblemOnInteractButton();
            SFXManager.Instance.PlayRandomSFXClip(correctSFX, transform, 1f);
        } else
        {
            //Show it was the wrong button -> maybe drop data collection rate and have a wrong sound + camera shake
            //Debug.Log("You got the wrong button");
            SFXManager.Instance.PlayRandomSFXClip(wrongSFX, transform, 1f);
        }
        
    }

    public override void OnStopInteract()
    {

    }
    public override void OnFocus()
    {
    }
    public override void OnLoseFocus()
    {
        
    }

    public void FixProblemOnInteractButton()
    {
        //Debug.Log("Fixed the problem");
        canFix = false;
        warningSign.FixProblemOnHandler(11);
        
        if(buttonLight != null)
        {
            buttonLight.sharedMaterial = buttonLightMaterial[0];
        }
    }

    public void MakeProblem(ProblemHandler warning)
    {
        canFix = true;
        warningSign = warning;
        if(buttonLight != null)
        {
            buttonLight.sharedMaterial = buttonLightMaterial[1];
        }
    }

    public void Update()
    {
        if(canFix)
        {
            buttonLight.sharedMaterial = buttonLightMaterial[1];
        } else 
        {
            buttonLight.sharedMaterial = buttonLightMaterial[0];
        }
    }
    
}
