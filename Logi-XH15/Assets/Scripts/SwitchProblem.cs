using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchProblem : MonoBehaviour
{
    [SerializeField] private List<InteractSwitch> allSwitches;
    [SerializeField] private List<InteractSwitch> breakbleSwitches;
    [SerializeField] private List<InteractSwitch> switchesToBreak;

    [SerializeField] private ProblemHandler warningSign;

    [SerializeField] private bool problemActive = false;

    [SerializeField] private int onSwitches;

    void Start()
    {
        onSwitches = allSwitches.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!problemActive)
            {
                ActivateProblem();
            } else 
            {
                Debug.Log("Switch problem is already active");
            }
            
        }

        if(problemActive)
        {
            if(onSwitches == allSwitches.Count)
            {
                Debug.Log("Problem Fixed");

                breakbleSwitches.Clear();
                switchesToBreak.Clear();

                warningSign.FixProblem();
                problemActive = false;
            }
        }
    }

    public void ActivateProblem()
    {
        

        Debug.Log("Problem Activated");
        int amountToBreak = Random.Range(1, allSwitches.Count);
        Debug.Log("Must break: " + amountToBreak);
        breakbleSwitches = new List<InteractSwitch>(allSwitches);

        for(int i = amountToBreak; i > 0; i--)
        {
            int randomSwitch = Random.Range(0, breakbleSwitches.Count);
            switchesToBreak.Add(breakbleSwitches[randomSwitch]);
            breakbleSwitches.RemoveAt(randomSwitch);
        }

        for(int i = 0; i < switchesToBreak.Count; i++)
        {
            switchesToBreak[i].Break();
            
        }

        warningSign.StartProblem();
        problemActive = true;
    }

    public void SwitchTrigger(bool on)
    {
        if(on)
        {
            onSwitches++;
        } else
        {
            onSwitches--;
        }
    }
}
