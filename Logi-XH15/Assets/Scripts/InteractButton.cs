using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : Interactable
{
    [SerializeField] private Animator buttonClick;
    [SerializeField] private ButtonProblem problem;
    [SerializeField] private bool canFix = false;
    [SerializeField] private AudioClip[] buttonSFX;
    private ProblemHandler warningSign; 

    void Start()
    {
        
    }

    public override void OnInteract()
    {
        buttonClick.SetTrigger("IsClicked");
        Debug.Log(SFXManager.Instance.name);
        SFXManager.Instance.PlayRandomSFXClip(buttonSFX, transform, 1f);
        if(canFix)
        {
            FixProblem();
        } else
        {
            //Show it was the wrong button -> maybe drop data collection rate and have a wrong sound + camera shake
            Debug.Log("You got the wrong button");
        }
        
    }
    public override void OnFocus()
    {

    }
    public override void OnLoseFocus()
    {
        
    }

    public void FixProblem()
    {
        Debug.Log("Fixed the problem");
        warningSign.FixProblem();
        canFix = false;
    }

    public void MakeProblem(ProblemHandler warning)
    {
        canFix = true;
        warningSign = warning;
    }

    
}
