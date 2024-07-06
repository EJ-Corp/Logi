using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemHandler : MonoBehaviour
{
    // [SerializeField] private Image warningPanel;

    // [SerializeField] private float flashInterval;
    // [SerializeField] private float flashCooldown;

    // [SerializeField] private Light[] problemLights = new Light[4];

    // enum WarningState
    // {
    //     disabled, active
    // };

    // [SerializeField] private WarningState currentState;
    // private bool panelActive = false;

    [SerializeField] private ProblemTimer problemTimer;

    // Start is called before the first frame update
    void Start()
    {
        // warningPanel = GameManager.Manager.problemPanel;
        // warningPanel.enabled = false;
        
        // for (int i = 0; i < problemLights.Length; i++)
        // {
        //     problemLights[i] = GameManager.Manager.warningLights[i];  
        //     problemLights[i].enabled = false;
        // }

    }

    // Update is called once per frame
    void Update()
    {
       // LoopThroughLights();
       // switch(currentState)
      //  {
      //      case WarningState.disabled:

                // foreach(WarningLights light in warningLights)
                // {
                //     light.SetFlashing(false);
                // }

        //        break;

       //     case WarningState.active:

                // foreach(WarningLights light in warningLights)
                // {
                //     light.SetFlashing(true);
                // }

                // if(flashCooldown > 0)
                // {
                //     flashCooldown -= Time.deltaTime;
                // }

                // if(flashCooldown <= 0)
                // {
                //     Trigger();
                // }
       //     break;
       // }
    }

    public void Trigger()
    {
        // if(panelActive)
        // {
        //     warningPanel.enabled = false;
        //     panelActive = false;
        //     flashCooldown = flashInterval;
        // } else 
        // {
        //     warningPanel.enabled = true;
        //     panelActive = true;
        //     flashCooldown = flashInterval;
            
        // }
    }

    public void FixProblemOnHandler(int problemID)
    {       
        problemTimer.FixedProblemOnTimer(problemID);
    }

    // public void StartWarning()
    // {
    //     currentState = WarningState.active;
    // }

    // public void NoProblems()
    // {
    //     warningPanel.enabled = false;
    //     currentState = WarningState.disabled;
    //     panelActive = false;
    // }

    // void LoopThroughLights() //lights turn on when a problem is happening commenting out to move them to when sun launches a flare
    // {
    //     for (int i = 0; i < problemLights.Length; i++)
    //     {
    //         if (currentState == WarningState.disabled) {
    //             problemLights[i].enabled = false;
    //         } else {
    //             problemLights[i].enabled = true;
    //         }
    //     }
    // }
}
