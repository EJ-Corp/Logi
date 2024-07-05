using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemHandler : MonoBehaviour
{
    [SerializeField] private Image warningPanel;

    [SerializeField] private float flashInterval;
    [SerializeField] private float flashCooldown;

    enum WarningState
    {
        diabled, active
    };

    [SerializeField] private WarningState currentState;
    private bool active = false;

    //Get rid of this and make it better
    [SerializeField] private TempProblemTimer problemTimer;

    // Start is called before the first frame update
    void Start()
    {
        warningPanel = GameManager.Manager.problemPanel;
        warningPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case WarningState.diabled:
                break;

            case WarningState.active:

                
                if(flashCooldown > 0)
                {
                    flashCooldown -= Time.deltaTime;
                }

                if(flashCooldown <= 0)
                {
                    Trigger();
                }
                break;
        }
    }

    public void Trigger()
    {
        if(active)
        {
            warningPanel.enabled = false;
            active = false;
            flashCooldown = flashInterval;
        } else 
        {
            warningPanel.enabled = true;
            active = true;
            flashCooldown = flashInterval;
            
        }
    }

    public void FixProblem(int problemID)
    {       
        problemTimer.FixedProblem(problemID);
    }

    public void StartProblem()
    {
        currentState = WarningState.active;
    }

    public void NoProblems()
    {
        warningPanel.enabled = false;
        currentState = WarningState.diabled;
        active = false;
    }
}
