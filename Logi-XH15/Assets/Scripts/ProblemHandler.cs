using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemHandler : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private WarningLights[] warningLights;

    //[SerializeField] private WarningLights[] warningLights;

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
        warningPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case WarningState.diabled:

                // foreach(WarningLights light in warningLights)
                // {
                //     light.SetFlashing(false);
                // }

                break;

            case WarningState.active:

                // foreach(WarningLights light in warningLights)
                // {
                //     light.SetFlashing(true);
                // }

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
            warningPanel.SetActive(false);
            active = false;
            flashCooldown = flashInterval;
        } else 
        {
            warningPanel.SetActive(true);
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
        warningPanel.SetActive(false);
        currentState = WarningState.diabled;
        active = false;
    }
}
