using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnPCAnimationEnd : StateMachineBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] ComputerFocusScript compFocusScript;

    void Awake() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        compFocusScript = gameManager.computerScreen.GetComponent<ComputerFocusScript>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        compFocusScript.SwapCameras();
    }
}
