using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnfocusOnPCAnimationEnd : StateMachineBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayerController playerController;

    void Awake() 
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mainCamera = gameManager.mainCamera;
        playerController = gameManager.playerController;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mainCamera.GetComponent<Animator>().enabled = false; //lets you move camera around after leaving computer
        playerController.CanMove = true;
    }
}
