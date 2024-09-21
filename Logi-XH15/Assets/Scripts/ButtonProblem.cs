using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonProblem : MonoBehaviour
{
    [SerializeField] private List<InteractButton> buttons;
    [SerializeField] private InteractButton fixButton;
    [SerializeField] private string buttonID;

    //Maybe get rid of this and make it more efficient -> only used to get the flashing warning sign.
    [SerializeField] private ProblemTimer warningSign;
    [SerializeField] private bool problemActive = false;

    public void ActivateProblem(bool tutorial)
    {
        problemActive = true;
        int randomButton = UnityEngine.Random.Range(0, buttons.Count);
        fixButton = buttons[randomButton];
        fixButton.MakeProblem(warningSign);
        buttonID = fixButton.ID;
        //Debug.Log("Problem is the " + name + " button");

        if(tutorial)
        {
            TutorialInteracts.TutManager.SpawnTutorial(3);
        }
    }

    public void FixProblem()
    {
        problemActive = false;
        fixButton = null;
        GameManager.Manager.problemTimer.NextTutorial();
    }

    public string getCurButtonID()
    {
        return buttonID;
    }
}
