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

    //computer objects
    [SerializeField] public GameObject computerScreen;

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

        gameState = GameState.mainView;
        computerScreen = GameObject.Find("Main Screen");
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

}
