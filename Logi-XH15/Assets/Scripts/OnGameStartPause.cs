using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGameStartPause : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = false;
        GameManager.Manager.isReading = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
