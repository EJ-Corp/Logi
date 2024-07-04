using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchProblem : MonoBehaviour
{
    [SerializeField] private List<InteractSwitch> allSwitches;
    [SerializeField] private List<InteractSwitch> breakbleSwitches;
    [SerializeField] private List<InteractSwitch> switchesToBreak;

    private bool problemActive = false;

    void Start()
    {
        
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
                Debug.Log("Button problem is already active");
            }
            
        }
    }

    public void ActivateProblem()
    {
        problemActive = true;

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
    }
}
