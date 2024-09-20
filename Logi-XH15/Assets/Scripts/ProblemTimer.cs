using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProblemTimer : MonoBehaviour
{
    [SerializeField] private int numberOfProblems;
    [SerializeField] private float nextProblemCountDown;
    [SerializeField] private int activeProblems = 0; 

    [SerializeField] private ButtonProblem buttonProblem;
    [SerializeField] private string buttonID;
    [SerializeField] private SwitchProblem switchProblem;
    [SerializeField] private PressurePuzzle pressureProblem;

    // [SerializeField] private AudioClip alarmSFX;

    [SerializeField] private HUDController warningPanel;

    [SerializeField] public List<int> problemIDPool;   //IDs: Buttons - 11, Switches = 12, Pressure = 13

    private MonitorScript computerScreen;

    [Header("Tutorial Checks")]
    [SerializeField] private bool tutorialActive = true;
    [SerializeField] private int tutorialTasksLeft = 3;
    [SerializeField] private bool awaitingTask = false;
    [SerializeField] private ShipIntegrity integrity;

    void Start()
    {
        computerScreen = GameObject.FindGameObjectWithTag("PCScreen").transform.GetComponent<MonitorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activeProblems < numberOfProblems)
        {
            if(nextProblemCountDown > 0 && !awaitingTask)
            {
                nextProblemCountDown -= Time.deltaTime;
            }

            if(nextProblemCountDown <= 0)
            {
                StartProblem();
            }
        }

        if (activeProblems < 0)
        {
            activeProblems = 0;
        }
    }

    public void StartProblem()
    {
        if(activeProblems == 0)
        {
           warningPanel.StartWarning();
        }
        int randomProblem = UnityEngine.Random.Range(0, problemIDPool.Count);

        //If tutorial still true then choose tasks left - 1 (for index purpose)
        if(tutorialActive)
        {
            randomProblem = tutorialTasksLeft - 1;
            awaitingTask = true;
            integrity.TutorialDamage();
        }

        int chosenProblemID = problemIDPool[randomProblem];

        //SFXManager.Instance.PlaySFXClip(alarmSFX, transform, 0.75f);

        if(chosenProblemID == 11) //Chose Buttons (ID = 11)
        {
            buttonProblem.ActivateProblem(tutorialActive);
            buttonID = buttonProblem.getCurButtonID();
            //activeProblems += 1;
            problemIDPool.RemoveAt(randomProblem);

            //Spawn Speech bubble
            computerScreen.SpawnProblemFact(chosenProblemID, buttonID);
            
        } else if(chosenProblemID == 12) //Chose Switched (ID = 12)
        {
            switchProblem.ActivateProblem(tutorialActive);
            //activeProblems += 1;
            problemIDPool.RemoveAt(randomProblem);

            //Spawn Speech bubble
            computerScreen.SpawnProblemFact(chosenProblemID, buttonID);
        } else if(chosenProblemID == 13) //Chose Pressure (ID = 13)
        {
            pressureProblem.ActivateProblem(tutorialActive);
            //activeProblems += 1;
            problemIDPool.RemoveAt(randomProblem);
            
            //Spawn Speech bubble
            computerScreen.SpawnProblemFact(chosenProblemID, buttonID);
        }

        activeProblems++;
        ResetCountdown();
    }

    public void ResetCountdown()
    {
        nextProblemCountDown = UnityEngine.Random.Range(10.0f, 15.0f);
    }

    public void FixedProblemOnTimer(int IDFixed)
    {
        activeProblems -= 1; 

        computerScreen.FixedProblemOnMonitor(IDFixed);
        
        if(IDFixed == 11)
        {
            buttonProblem.FixProblem();
        }

        if(!problemIDPool.Contains(IDFixed))
        {
            //Debug.Log("added problem back to list " + IDFixed);
            problemIDPool.Add(IDFixed);
        }     
        
        
        

        if(activeProblems <= 0)
        {
            warningPanel.NoProblems();
        }

        
    }

    public void NextTutorial()
    {
        tutorialTasksLeft--;
        awaitingTask = false;
        TutorialInteracts.TutManager.FixTimer();

        if(tutorialTasksLeft <= 0)
        {
            tutorialActive = false;
            SunManager.Sun.ActivateFire();
            GameManager.Manager.tutorialCompleted = true;
        }
    }
}
