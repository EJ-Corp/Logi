using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private static GameManager manager;
    public static GameManager Manager
    {
        get
        {
            return manager;
        }
    }

    //camera objects
    [SerializeField] public Camera mainCamera;
    [SerializeField] public AudioListener mainListener;
    [SerializeField] public Camera pcCamera; 
    [SerializeField] public AudioListener pcListener;

    //player objects
    [SerializeField] public GameObject player;
    [SerializeField] public Canvas playerCanvas;
    [SerializeField] public bool isReading = false;

    //computer objects
    [SerializeField] public MonitorScript computerMonitor;
    [SerializeField] public ComputerFocusScript computerScreenFocus;
    [SerializeField] public Canvas monitorBoundary;
    [SerializeField] public IDandPasswordInputScript idInput;
    [SerializeField] public IDandPasswordInputScript pswdInput;
    [SerializeField] public Animator monitorAnimator;

    //problem objects
    [SerializeField] public Image problemPanel;
    [SerializeField] public ProblemTimer problemTimer;
    [SerializeField] public Light[] warningLights;

    //book stuff

    [SerializeField] public OnBookInteract bookInstructions;

    //manager objects
    [SerializeField] public GameObject theManager;
    [SerializeField] public bool shipDoneMoving = false;
    [SerializeField] public SunManager sumManager;

    enum GameState
    {
        mainView,
        pcView
    };

    [SerializeField] private GameState gameState;
    public bool inMainView = true;
    public bool inPCView = false;

    public GameObject tutorialText;

    [Header("Problems")]
    public SwitchProblem switches;
    private bool hasPlayedTrigger = false;
    private bool hasBufferedSun = false;

    //Password Generator
    [Header("Password Generation")]
    [SerializeField] private int length;
    [SerializeField] private int max_length;
    [SerializeField] private int min_length;
    [SerializeField] private bool randomLength;
    [SerializeField] private TMP_Text passwordNote;

    public string password;

    public bool computerState = false;

    void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }

        // computerScreenFocus = GameObject.FindGameObjectWithTag("PCScreen").GetComponent<ComputerFocusScript>();
        // idInput = GameObject.FindGameObjectWithTag("idInput").GetComponent<IDandPasswordInputScript>();
        // pswdInput = GameObject.FindGameObjectWithTag("pswdInput").GetComponent<IDandPasswordInputScript>();
        // problemPanel = GameObject.FindGameObjectWithTag("problemPanel").GetComponent<Image>();
        sumManager = GameObject.FindGameObjectWithTag("Sun").GetComponent<SunManager>();
        theManager = this.transform.parent.gameObject;
        gameState = GameState.mainView;

    }

    void Start()
    {
        idInput.GetTextInput("helios");
        password = GeneratePassword();
        passwordNote.text = password;
    }

    void Update()
    {
       // OpenBookOnGameStart();

        switch(gameState)
        {
            case GameState.mainView:
            inMainView = true;
            inPCView = false;

            if (mainCamera.enabled == false)
            {
                gameState = GameState.pcView;
            }
            break;

            case GameState.pcView:
            inMainView = false;
            inPCView = true;

            if (pcCamera.enabled == false)
            {
                gameState = GameState.mainView;
            }
            break;
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            if(Time.timeScale == 0)
            {
                Debug.Log("Game Resumed");
                Time.timeScale = 1f;
                
            } else 
            {
                Debug.Log("Game Paused");
                Time.timeScale = 0f;
            }
        }
    }

    public ComputerFocusScript ShareComputerFocusScript()
    {
        return computerScreenFocus;
    }

    public string GeneratePassword()
    {
        if (randomLength)
        {
            length = UnityEngine.Random.Range(min_length, max_length);
        }

        string allChars = "abcdefghijklmnopqrstuvwxyz0123456789";
       // string allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
       // string allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        string password = "";

        for (int i = 0; i < length; i++)
        {
            password += allChars[UnityEngine.Random.Range(0, allChars.Length)];
        }

        return password;
    }
    
    public void ToggleComputer(bool computerState)
    {
        if(computerState) //Computer is on so we turn it off
        {
         //   if (hasPlayedTrigger != true)
          //  {
                monitorAnimator.SetBool("pcTurnedOff", true);
                monitorAnimator.SetBool("pcTurnedOn", false);
           //     hasPlayedTrigger = true;
           // }

            computerScreenFocus.transform.GetComponent<MonitorScript>().BreakComputer();
            idInput.ResetInputResult();
            pswdInput.ResetInputResult();
            this.computerState = false;
        } else //COmputer is off so we turn it on
        {
            monitorAnimator.SetBool("pcTurnedOff", false);
            monitorAnimator.SetBool("pcTurnedOn", true);
          //  hasPlayedTrigger = false;

            sumManager.BufferFlare();
            this.computerState = true;

            computerScreenFocus.transform.GetComponent<MonitorScript>().FixComputer();
            idInput.ResetInputResult();
            pswdInput.ResetInputResult();
            password = GeneratePassword();
            passwordNote.text = password;
        }
        
    }

    public void CloseTutorialText()
    {
        tutorialText.SetActive(false);
        Debug.Log("Disabled Text");
    }

    // void OpenBookOnGameStart()
    // {
    //     if (openedBook != true && sumManager != null)
    //     {
    //         bookInstructions.OnInteract();
    //         openedBook = true;
    //     }
    // }
}
