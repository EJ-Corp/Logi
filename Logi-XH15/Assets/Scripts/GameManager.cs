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
    [SerializeField] public PlayerController playerController;
    [SerializeField] public Canvas playerCanvas;

    //computer objects
    [SerializeField] public ComputerFocusScript computerScreenFocus;
    [SerializeField] public Canvas monitorBoundary;
    [SerializeField] public IDandPasswordInputScript idInput;
    [SerializeField] public IDandPasswordInputScript pswdInput;

    //problem objects
    [SerializeField] public Image problemPanel;
    [SerializeField] public Light[] warningLights;

    enum GameState
    {
        mainView,
        pcView
    };

    [SerializeField] private GameState gameState;
    public bool inMainView = true;
    public bool inPCView = false;

    [Header("Problems")]
    public SwitchProblem switches;

    //Password Generator
    [Header("Password Generation")]
    [SerializeField] private int length = 7;
    [SerializeField] private int max_length = 8;
    [SerializeField] private int min_length = 4;
    [SerializeField] private bool randomLength = false;
    [SerializeField] private TMP_Text passwordNote;

    public string password;

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
    }

    public ComputerFocusScript ShareComputerFocusScript()
    {
        return computerScreenFocus;
    }

    public string GeneratePassword()
    {
        if (randomLength)
        {
            length = Random.Range(min_length, max_length);
        }

        string allChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        string password = "";

        for (int i = 0; i < length; i++)
        {
            password += allChars[Random.Range(0, allChars.Length)];
        }

        return password;
    }
    
    public void ToggleComputer(bool computerState)
    {
        if(computerState) //Computer is on so we turn it off
        {
            computerScreenFocus.transform.GetComponent<MonitorScript>().BreakComputer();
            idInput.ResetInputResult();
            pswdInput.ResetInputResult();
        } else //COmputer is off so we turn it on
        {
            computerScreenFocus.transform.GetComponent<MonitorScript>().FixComputer();
            idInput.ResetInputResult();
            pswdInput.ResetInputResult();
            password = GeneratePassword();
            passwordNote.text = password;
        }
        
    }
}
