using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGameStartOpenBook : StateMachineBehaviour
{

    [SerializeField] private InstructionBook book;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        book = GameManager.Manager.bookInstructions;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        book.OnInteract();
        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = false;
        GameManager.Manager.isReading = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Manager.shipDoneMoving = true;
        GameManager.Manager.theManager.GetComponent<Animator>().enabled = false;
    }
}
