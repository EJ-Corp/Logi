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
    [SerializeField] private GameObject randomFactBubble;
    [SerializeField] private GameObject flirtBubble;
    [SerializeField] private List<GameObject> activeBubbles = new List<GameObject>();

    [Header("SOVA Animation Stuff")]
    [SerializeField] private Animator sovaAnimator;
    [SerializeField] private int problemBubbleCount = 0;
    [SerializeField] private float idleTriggerTimer;
    [SerializeField] private float idleTriggerRandInterval;

    private int buttonBubbleIdx;
    private int switchBubbleIdx;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mouseBounds = gameManager.monitorBoundary;
        mouse = Mouse.current;
        desktopScreen.SetActive(false);
        correctID = "helios";

        idleTriggerRandInterval = Random.Range(4, 10);
    }

    // Update is called once per frame
    void Update()
    {
        correctPSWD = gameManager.password;
        mousePos = Input.mousePosition;
        mouseBounds.GetComponent<RectTransform>().GetWorldCorners(worldCorners);

        if(isUsable)
        {
            idInputValue = gameManager.idInput.GetInputResult();
            pswdInputValue = gameManager.pswdInput.GetInputResult();
            CheckIDAndPassword();
        }

        if (problemBubbleCount < 0)
        {
            problemBubbleCount = 0;
        }

        if (activeBubbles.Count == 0)
        {
            sovaAnimator.SetBool("isBubbleActive", false);
        } else {
            sovaAnimator.SetBool("isBubbleActive", true);
            sovaAnimator.SetInteger("isProblemActive", problemBubbleCount);

            if (idleTriggerTimer > 0)
            {
                idleTriggerTimer -= Time.deltaTime;
            }

            if (idleTriggerTimer <= 0)
            {
                float randomCoinFlip = Random.Range(1,4);

                if (randomCoinFlip <= 2)
                {
                    sovaAnimator.SetTrigger("goIdle1");   
                } else if (randomCoinFlip >= 3)
                {
                    sovaAnimator.SetTrigger("goIdle2");
                }

                idleTriggerRandInterval = Random.Range(8, 16);
                idleTriggerTimer = idleTriggerRandInterval;
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
        int randomAnnoy = Random.Range(0, annoyIDPool.Count);
        int chosenAnnoyID = annoyIDPool[randomAnnoy];

        //Debug.Log("Spawned Annoy ID: " + randomAnnoy);

        switch(chosenAnnoyID)
        {
            case 0: //Spawn a flirt
                GameObject flirtSpeechBubble = Instantiate(flirtBubble, bubbleHolder.transform);
                flirtSpeechBubble.name = "Flirt Bubble";
                activeAnnoy++;
                sovaAnimator.SetTrigger("flirtTime");
                annoyIDPool.RemoveAt(randomAnnoy);
                break;
            case 1: //Spawn a fact
                GameObject factSpeeechBubble = Instantiate(randomFactBubble, bubbleHolder.transform);
                factSpeeechBubble.name = "Fact Bubble";
                activeAnnoy++;
                sovaAnimator.SetTrigger("talkingTime");
                annoyIDPool.RemoveAt(randomAnnoy);
                break;
        }

        annoyCountdown = Random.Range(5.0f, 30.0f);
    }

    public void ClosedAnnoy(int annoyIDClosed)
    {
        activeAnnoy--;
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
                TMP_Text bubbleText = buttonBubble.transform.Find("fact (2)").GetComponent<TMP_Text>();
                bubbleText.text = 
                "Don't worry, the " + buttonID + " button should be able to stabilise it. You got this! :)";
                //buttonBubble.transform.parent = bubbleHolder.transform;

                problemBubbleCount += 1;
                sovaAnimator.SetTrigger("talkingTime");

                break;

            case 12: //Its the switch Problem (ID = 12)
                GameObject switchBubble = Instantiate(switchProbBubble, bubbleHolder.transform);
                switchBubble.name = "12Bubble";
                activeBubbles.Add(switchBubble);
                switchBubbleIdx = activeBubbles.Count - 1;
                //switchBubble.transform.parent = bubbleHolder.transform;

                problemBubbleCount += 1;
                sovaAnimator.SetTrigger("talkingTime");

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

}
