using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.UI;
using TMPro;

public class MonitorScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private Mouse mouse;
    [SerializeField] private Canvas mouseBounds;
    [SerializeField] private Vector3 mousePos;
    private Vector3[] worldCorners = new Vector3[4];
    [SerializeField] private Vector3[] screenCorners = new Vector3[4];

    [SerializeField] private string correctID;
    [SerializeField] private string correctPSWD;
    [SerializeField] string idInputValue;
    [SerializeField] string pswdInputValue;
    private bool isUsable = true;

    [Header("Screens & Switch")]
    [SerializeField] GameObject desktopScreen;
    [SerializeField] GameObject loginScreen;
    [SerializeField] private InteractCompBreaker powerSupply;
    [SerializeField] private bool startingGame;

    [Header("Annoying Bubbles")]
    [SerializeField] private int maxAnnoy;
    [SerializeField] private int activeAnnoy;
    [SerializeField] private float annoyCountdown;
    [SerializeField] private List<int> annoyIDPool; 

    [Header("Speech Bubbles")]
    [SerializeField] private GameObject bubbleHolder;
    [SerializeField] private GameObject buttonProbBubble;
    [SerializeField] private GameObject switchProbBubble;
    [SerializeField] private GameObject pressureProbBubble;
    [SerializeField] private GameObject flirtBubble;
    [SerializeField] private GameObject historyBubble;
    [SerializeField] private GameObject galaxyBubble;
    [SerializeField] private List<GameObject> activeBubbles = new List<GameObject>();

    [Header("stella Animation Stuff")]
    [SerializeField] private Animator stellaAnimator;
    [SerializeField] private int problemBubbleCount = 0;
    [SerializeField] private float idleTriggerTimer;
    [SerializeField] private float idleTriggerRandInterval;

    private int buttonBubbleIdx;
    private int switchBubbleIdx;
    private int pressureBubbleIdx;

    private int flirtSpeechBubbleIdx;
    private int historySpeechBubbleIdx;
    private int galaxySpeechBubbleIdx;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mouseBounds = gameManager.monitorBoundary;
        mouse = Mouse.current;
        desktopScreen.SetActive(false);
        correctID = "helios";

        ResetIdleTimer();    
    }

    // Update is called once per frame
    void Update()
    {
        correctPSWD = gameManager.password;
        mousePos = Input.mousePosition;
        mouseBounds.GetComponent<RectTransform>().GetWorldCorners(worldCorners);
        GameManager.Manager.monitorAnimator.SetBool("pcIsWorking", isUsable);

        if(isUsable)
        {
            idInputValue = gameManager.idInput.GetInputResult();
            pswdInputValue = gameManager.pswdInput.GetInputResult();
            CheckIDAndPassword();
        }

        if (activeBubbles.Count <= 0)
        {
            stellaAnimator.SetBool("isBubbleActive", false);
        } else {
            stellaAnimator.SetBool("isBubbleActive", true);

            if (idleTriggerTimer > 0)
            {
                idleTriggerTimer -= Time.deltaTime;
            }

            if (idleTriggerTimer <= 0)
            {
                float randomCoinFlip = UnityEngine.Random.Range(1,4);

                if (randomCoinFlip <= 2)
                {
                    stellaAnimator.SetTrigger("goIdle1");   
                } else if (randomCoinFlip >= 3)
                {
                    stellaAnimator.SetTrigger("goIdle2");
                }

                ResetIdleTimer();
            }
        }
        
        //We are getting the same 4 corners repeadetly every fram --- IS THIS NEEDED??? NOT JUST ONCE?? BAD CODE.
        for (int i = 0 ; i < 4; i++)
        {
            screenCorners[i] = gameManager.pcCamera.WorldToScreenPoint(worldCorners[i]); //botleft, topleft, topright, botright
        }

        if (startingGame == true)
        {
            gameManager.playerCanvas.enabled = false;
        } else {
            if (gameManager.inPCView)
            {
                gameManager.playerCanvas.enabled = false;
                RestrictMouseToMonitorBounds();
            } else {
                gameManager.playerCanvas.enabled = true;
            }
            }


        if(annoyCountdown > 0 && activeAnnoy < maxAnnoy)
        {
            annoyCountdown -= Time.deltaTime;
        }

        if(annoyCountdown <= 0 && activeAnnoy < maxAnnoy)
        {
            SpawnAnnoyBubble();
        }
    }

    void CheckIDAndPassword()
    {
        if (idInputValue == correctID && pswdInputValue == correctPSWD)
        {
            loginScreen.SetActive(false);
            desktopScreen.SetActive(true);
        }
    }

    void RestrictMouseToMonitorBounds()
    {   //botleft0 (less than x and y)
        //topleft1 (less than x / greater than y)
        //topright2 (greater than x and y)
        //botright3 (greater than x less than y)

        //check y boundary
        if (mousePos.y <= screenCorners[0].y + 25)
        {
            Mouse.current.WarpCursorPosition(new Vector2(mousePos.x , screenCorners[0].y + 30));
        } else
        if(mousePos.y >= screenCorners[1].y)
        {
            Mouse.current.WarpCursorPosition(new Vector2(mousePos.x, screenCorners[1].y - 5));
        }

        //check x boundary
        if (mousePos.y >= ((screenCorners[0].y + screenCorners[1].y) / 2))
        {
            if(mousePos.x <= screenCorners[1].x)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[1].x + 5, mousePos.y));
            } else       
            if (mousePos.x >= screenCorners[2].x - 10)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[2].x - 15, mousePos.y));
            }  

        } else
        if (mousePos.y <= ((screenCorners[0].y + screenCorners[1].y) / 2))
        {
            if (mousePos.x <= screenCorners[0].x)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[0].x + 5, mousePos.y));
            } else        
            if(mousePos.x >= screenCorners[3].x - 10)
            {
                Mouse.current.WarpCursorPosition(new Vector2(screenCorners[3].x - 15, mousePos.y));
            }
        }
    }

    public void SpawnAnnoyBubble()
    {
        int randomAnnoy = UnityEngine.Random.Range(0, annoyIDPool.Count);
        int chosenAnnoyID = annoyIDPool[randomAnnoy];

        //Debug.Log("Spawned Annoy ID: " + randomAnnoy);

        switch(chosenAnnoyID)
        {
            case 0: //Spawn a flirt
                GameObject flirtSpeechBubble = Instantiate(flirtBubble, bubbleHolder.transform);
                flirtSpeechBubble.name = "Flirt Bubble";
                activeBubbles.Add(flirtSpeechBubble);
                flirtSpeechBubbleIdx = activeBubbles.Count - 1;

                activeAnnoy++;
                stellaAnimator.SetTrigger("flirtTime");
                annoyIDPool.RemoveAt(randomAnnoy);
            break;

            case 1: //Spawn a history fact
                GameObject historySpeechBubble = Instantiate(historyBubble, bubbleHolder.transform);
                historySpeechBubble.name = "History Bubble";
                activeBubbles.Add(historySpeechBubble);
                historySpeechBubbleIdx = activeBubbles.Count - 1;

                activeAnnoy++;
                stellaAnimator.SetTrigger("talkingTime");
                annoyIDPool.RemoveAt(randomAnnoy);
            break;

            case 2: //Spawn a galaxy fact
                GameObject galaxySpeechBubble = Instantiate(galaxyBubble, bubbleHolder.transform);
                galaxySpeechBubble.name = "Galaxy Bubble";
                activeBubbles.Add(galaxySpeechBubble);
                galaxySpeechBubbleIdx = activeBubbles.Count - 1;

                activeAnnoy++;
                stellaAnimator.SetTrigger("talkingTime");
                annoyIDPool.RemoveAt(randomAnnoy);
            break;
        }

        annoyCountdown = UnityEngine.Random.Range(5.0f, 30.0f);
    }

    public void ClosedAnnoy(int annoyIDClosed)
    {
        activeAnnoy--;
        if (annoyIDClosed == 0) //flirt bubble
        {
            activeBubbles.RemoveAt(flirtSpeechBubbleIdx);
        } else
        if (annoyIDClosed == 1) //history fact bubble
        {
            activeBubbles.RemoveAt(historySpeechBubbleIdx);
        } else 
        if (annoyIDClosed == 2) //galaxy fact bubble
        {
            activeBubbles.RemoveAt(galaxySpeechBubbleIdx);
        }

        annoyIDPool.Add(annoyIDClosed);
    }

    public void SpawnProblemFact(int problemID, string buttonID)
    {
        switch(problemID)
        {
            case 11: //Its the button problem (ID = 11)
                GameObject buttonBubble = Instantiate(buttonProbBubble, bubbleHolder.transform);
                buttonBubble.name = "11Bubble";
                activeBubbles.Add(buttonBubble);
                buttonBubbleIdx = activeBubbles.Count - 1;
                
                //change button name in text
                TMP_Text bubbleText = buttonBubble.transform.Find("fact (1)").GetComponent<TMP_Text>();
                bubbleText.text = 
                "Seems like the " + buttonID + " is destabilising!!!";
                //buttonBubble.transform.parent = bubbleHolder.transform;

                problemBubbleCount += 1;
                stellaAnimator.SetTrigger("talkingTime");
                ResetIdleTimer();

            break;

            case 12: //Its the switch Problem (ID = 12)
                GameObject switchBubble = Instantiate(switchProbBubble, bubbleHolder.transform);
                switchBubble.name = "12Bubble";
                activeBubbles.Add(switchBubble);
                switchBubbleIdx = activeBubbles.Count - 1;
                //switchBubble.transform.parent = bubbleHolder.transform;

                problemBubbleCount += 1;
                stellaAnimator.SetTrigger("talkingTime");
                ResetIdleTimer();

            break;

            case 13: //Its the pressure Problem (ID = 13)
                GameObject pressureBubble = Instantiate(pressureProbBubble, bubbleHolder.transform);
                pressureBubble.name = "13Bubble";
                activeBubbles.Add(pressureBubble);
                pressureBubbleIdx = activeBubbles.Count - 1;
                //switchBubble.transform.parent = bubbleHolder.transform;

                problemBubbleCount += 1;
                stellaAnimator.SetTrigger("talkingTime");
                ResetIdleTimer();

            break;
        }
    }

    public void FixedProblemOnMonitor(int problemID)
    {
        if(problemID == 11) //Fixed Buttons
        {
            Destroy(activeBubbles[buttonBubbleIdx].gameObject);
            activeBubbles.RemoveAt(buttonBubbleIdx);

            problemBubbleCount -= 1;

        } else if(problemID == 12) //Fixed Switches
        {
            Destroy(activeBubbles[switchBubbleIdx].gameObject);
            activeBubbles.RemoveAt(switchBubbleIdx);

            problemBubbleCount -= 1;
        } else if (problemID == 13) //Fixed pressure
        {
            Destroy(activeBubbles[pressureBubbleIdx].gameObject);
            activeBubbles.RemoveAt(pressureBubbleIdx);

            problemBubbleCount -= 1;            
        }
    }

    public void BreakComputer()
    {
        isUsable = false;
        loginScreen.SetActive(false);
        desktopScreen.SetActive(false);
        powerSupply.FlareBreak();
    }

    public void FixComputer()
    {
        isUsable = true;
        loginScreen.SetActive(true);
        desktopScreen.SetActive(false);
    }

    void ResetIdleTimer()
    {
        idleTriggerRandInterval = UnityEngine.Random.Range(8, 16);
        idleTriggerTimer = idleTriggerRandInterval;       
    }
}
