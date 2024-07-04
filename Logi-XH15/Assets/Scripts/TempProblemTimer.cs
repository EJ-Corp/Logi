using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProblemTimer : MonoBehaviour
{
    
    [SerializeField] private float nextProblemCountDown;
    [SerializeField] private int activeProblems = 0; 
    [SerializeField] private ButtonProblem buttonProblem;
    [SerializeField] private SwitchProblem switchProblem;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeProblems < 2)
        {
            if(nextProblemCountDown > 0)
            {
                nextProblemCountDown -= Time.deltaTime;
            }

            if(nextProblemCountDown <= 0)
            {
                StartProblem();
            }
        }
    }

    public void StartProblem()
    {
        int randomProblem = Random.Range(1, 3);

        if(randomProblem == 1)
        {
            //Start Button
            buttonProblem.ActivateProblem();
            activeProblems++;
            
        } else if(randomProblem == 2)
        {
            switchProblem.ActivateProblem();
            activeProblems++;
        }

        ResetCountdown();
    }

    public void ResetCountdown()
    {
        nextProblemCountDown = Random.Range(10.0f, 15.0f);
    }

    public void FixedProblem()
    {
        activeProblems--;
    }
}
