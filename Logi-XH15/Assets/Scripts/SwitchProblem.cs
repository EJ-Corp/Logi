using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchProblem : MonoBehaviour
{
    [SerializeField] private List<InteractSwitch> allSwitches;
    [SerializeField] private List<InteractSwitch> breakbleSwitches;
    [SerializeField] private List<InteractSwitch> switchesToBreak;

    [SerializeField] private ProblemTimer warningSign;

    [SerializeField] private bool problemActive = false;

    [SerializeField] private int onSwitches;
    private bool firstFix = false;

    

    void Start()
    {
        onSwitches = allSwitches.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(problemActive)
        {
            if(onSwitches == allSwitches.Count)
            {
                //Debug.Log("Problem Fixed");

                breakbleSwitches.Clear();
                switchesToBreak.Clear();

                warningSign.FixedProblemOnTimer(12);
                problemActive = false;

                if(!firstFix)
                {
                    firstFix = true;
                    GameManager.Manager.problemTimer.NextTutorial();
                }
            }
        }
    }

    public void ActivateProblem(bool tutorial)
    {
        //Debug.Log("Problem Activated");
        int amountToBreak = UnityEngine.Random.Range(1, allSwitches.Count);
        //Debug.Log("Must break: " + amountToBreak);
        breakbleSwitches = new List<InteractSwitch>(allSwitches);

        for(int i = amountToBreak; i > 0; i--)
        {
            int randomSwitch = UnityEngine.Random.Range(0, breakbleSwitches.Count);
            switchesToBreak.Add(breakbleSwitches[randomSwitch]);
            breakbleSwitches.RemoveAt(randomSwitch);
        }

        for(int i = 0; i < switchesToBreak.Count; i++)
        {
            switchesToBreak[i].Break();
            
        }

        problemActive = true;

        if(tutorial)
        {
            TutorialInteracts.TutManager.SpawnTutorial();
        }
    }

    public void SwitchTrigger(bool on)
    {
        if(on)
        {
            onSwitches += 1;
        } else
        {
            onSwitches -= 1;
        }
    }
}
