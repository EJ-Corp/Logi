using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialInteracts : MonoBehaviour
{
    private static TutorialInteracts tutManager;
    public static TutorialInteracts TutManager
    {
        get
        {
            return tutManager;
        }
    }

    [SerializeField] private GameObject stellaTutorial;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private CountdownTimer timer;

    // Start is called before the first frame update
    void Start()
    {
        if(tutManager == null)
        {
            tutManager = this;
        }

        stellaTutorial = gameObject;
      //  WhichText(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        Debug.Log("Button Pressed");

        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = true;
        GameManager.Manager.player.GetComponent<PlayerController>().PlayerReticle.enabled = true;
        GameManager.Manager.isReading = false;
        Cursor.visible = false;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void SpawnTutorial(int whichProblem)
    {
        gameObject.SetActive(true);
        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = false;
        GameManager.Manager.player.GetComponent<PlayerController>().PlayerReticle.enabled = false;
        GameManager.Manager.isReading = true;
        Cursor.visible = true;
        timer.timerOn = false;

        WhichText(whichProblem);
    }

    public void FixTimer()
    {
        timer.timerOn = true;
    }

    void WhichText(int whichProblem)
    {
        /*starting text
            Welcome<br><size=105%><b>[NEW EMPLOYEE NAME]!</b></size><br>\(*v*)/<br><br>Your job tasks include<br><size=110%><b>[FIXING]!</b></size> the various <size=110%><b>[PROBLEMS]!</b></size><br>your ship might encounter orbiting <size=105%><b>[LOGI-XH15]!</b></size><br><br>Check out the <size=105%><b>[Manual]!</b></size> for more details!
        */
        switch(whichProblem)
        {
            case 1: //pressure problem
                tutorialText.text
                ="<size=105%><b>[OOPS]!</b></size><br>That alarm and symbol means a problem is occuring!<br><br>This time it's the pressure, so go <size=105%><b>[PULL DOWN]!</b></size> on the <size=105%><b>[CORD]!</b></size> till the gauge is in the<br> <size=105%><b>[GREEN]!</b></size> to fix it!";
            break;

            case 2: //switch problem
                tutorialText.text
                ="<size=105%><b>[HELLO AGAIN]!</b></size><br>If you ever forget how to solve a problem you should read the <size=105%><b>[MANUAL]!</b></size><br><br>This problem is the <size=105%><b>[SWITCHES]!</b></size> this time, to <size=105%><b>[FIX]!</b></size> it, just <size=105%><b>[FLIP]!</b></size> every one so they are <size=105%><b>[POINTING UPWARDS]!</b></size>";
            break;

            case 3: //buttons problem
                tutorialText.text
                ="You're almost ready to go on your own!<br> For <size=105%><b>[FUTURE PROBLEMS]!</b></size> you can find <size=105%><b>[STELLA]!</b></size> on your <size=105%><b>[TERMINAL]!</b></size> at the front!<br><br>The <size=105%><b>[CURRENT PROBLEM]!</b></size> is the <size=105%><b>[BUTTONS]!</b></size> <size=105%><br><b>[FIND]!</b></size> and <size=105%><b>[PUSH]!</b></size> the correct button to <size=105%><b>[FIX]!</b></size> it!";
            break;
        }
    }
}
