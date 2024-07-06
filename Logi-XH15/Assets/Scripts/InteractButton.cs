using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : Interactable
{
    [SerializeField] private Animator buttonClick;
    [SerializeField] private ButtonProblem problem;
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

    void Start()
    {
        
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
            Debug.Log("You got the wrong button");
            SFXManager.Instance.PlayRandomSFXClip(wrongSFX, transform, 1f);
        }
        
    }
    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {
        
    }

    public void FixProblemOnInteractButton()
    {
        Debug.Log("Fixed the problem");
        warningSign.FixProblemOnHandler(11);
        canFix = false;
    }

    public void MakeProblem(ProblemHandler warning)
    {
        canFix = true;
        warningSign = warning;
    }

    
}
