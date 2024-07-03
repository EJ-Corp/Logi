using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfocusOnPCAnimationEnd : StateMachineBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Camera roomCamera;

    void Awake() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        roomCamera = gameManager.roomCamera;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        roomCamera.GetComponent<Animator>().enabled = false; //lets you move camera around after leaving computer
    }
}
