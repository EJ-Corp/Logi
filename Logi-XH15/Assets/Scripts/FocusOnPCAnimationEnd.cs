using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnPCAnimationEnd : StateMachineBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] ComputerFocusScript compFocusScript;
   // [SerializeField] ComputerInteractableScript compInteractableScript;

    void Awake() 
    {
       // Debug.Log("We start 1");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        compFocusScript = gameManager.computerScreenFocus;
       // compInteractableScript = gameManager.computerScreen.GetComponent<ComputerInteractableScript>();
       // compFocusScript = GameManager.manager.ShareComputerFocusScript();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        compFocusScript.SwapCameras();
       // compInteractableScript.SwapCameras();
    }
}
