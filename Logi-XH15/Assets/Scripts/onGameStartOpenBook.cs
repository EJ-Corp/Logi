using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGameStartOpenBook : StateMachineBehaviour
{

    [SerializeField] private OnBookInteract book;
    [SerializeField] private GameObject stellaText;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        book = GameManager.Manager.bookInstructions;
        stellaText = GameManager.Manager.tutorialText;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //open the book on start
        //book.OnInteract();
        //Debug.Log(name);

        //Open a tutorial UI
        stellaText.SetActive(true);

        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = false;
        GameManager.Manager.isReading = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.Manager.shipDoneMoving = true;
        GameManager.Manager.theManager.GetComponent<Animator>().enabled = false;
    }
}
