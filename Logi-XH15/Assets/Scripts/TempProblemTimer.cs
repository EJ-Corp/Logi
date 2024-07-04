using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempProblemTimer : MonoBehaviour
{
    [SerializeField] private int numberOfProblems;
    [SerializeField] private float nextProblemCountDown;
    [SerializeField] private int activeProblems = 0; 
    [SerializeField] private ButtonProblem buttonProblem;
    [SerializeField] private SwitchProblem switchProblem;
    [SerializeField] private AudioClip alarmSFX;
    [SerializeField] private ProblemHandler warningPanel;

    [SerializeField] private List<int> problemIDPool;   //IDs: Buttons - 11, Switches = 12

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeProblems < numberOfProblems)
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
        if(activeProblems == 0)
        {
            warningPanel.StartProblem();
        }
        int randomProblem = Random.Range(0, problemIDPool.Count);

        int chosenProblemID = problemIDPool[randomProblem];

        SFXManager.Instance.PlaySFXClip(alarmSFX, transform, 0.75f);

        if(chosenProblemID == 11) //Chose Buttons (ID = 11)
        {
            buttonProblem.ActivateProblem();
            activeProblems++;
            problemIDPool.RemoveAt(randomProblem);
            
        } else if(chosenProblemID == 12) //Chose Switched (ID = 12)
        {
            switchProblem.ActivateProblem();
            activeProblems++;
            problemIDPool.RemoveAt(randomProblem);
        }

        ResetCountdown();
    }

    public void ResetCountdown()
    {
        nextProblemCountDown = Random.Range(10.0f, 15.0f);
    }

    public void FixedProblem(int IDFixed)
    {
        activeProblems--;
        problemIDPool.Add(IDFixed);

        if(activeProblems <= 0)
        {
            warningPanel.NoProblems();
        }
    }
}
