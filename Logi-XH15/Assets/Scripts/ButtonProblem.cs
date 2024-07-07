using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonProblem : MonoBehaviour
{
    [SerializeField] private List<InteractButton> buttons;
    [SerializeField] private InteractButton fixButton;
    [SerializeField] private string buttonID;

    //Maybe get rid of this and make it more efficient -> only used to get the flashing warning sign.
    [SerializeField] private ProblemHandler warningSign;
    [SerializeField] private bool problemActive = false;

    public void ActivateProblem()
    {
        problemActive = true;
        int randomButton = UnityEngine.Random.Range(0, buttons.Count);
        fixButton = buttons[randomButton];
        fixButton.MakeProblem(warningSign);
        buttonID = fixButton.ID;
        Debug.Log("Problem is the " + buttonID + " button");

    }

    public string getCurButtonID()
    {
        return buttonID;
    }
}
