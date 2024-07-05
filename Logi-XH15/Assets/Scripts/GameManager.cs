using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }

        computerScreenFocus = GameObject.FindGameObjectWithTag("PCScreen").GetComponent<ComputerFocusScript>();
        idInput = GameObject.FindGameObjectWithTag("idInput").GetComponent<IDandPasswordInputScript>();
        pswdInput = GameObject.FindGameObjectWithTag("pswdInput").GetComponent<IDandPasswordInputScript>();
        gameState = GameState.mainView;
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
        }
        
    }
}
