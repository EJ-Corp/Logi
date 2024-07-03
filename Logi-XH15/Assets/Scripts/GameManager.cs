using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //camera objects
    [SerializeField] public Camera roomCamera;
    [SerializeField] public AudioListener roomListener;
    [SerializeField] public Camera pcFocusCamera; 
    [SerializeField] public AudioListener pcFocusListener; 

    //player objects
    [SerializeField] public PlayerController playerController;

    //computer objects
    [SerializeField] public GameObject computerScreen;

    enum GameState
    {
        roomView,
        pcFocusView
    };

    [SerializeField] private GameState gameState;

    void Start()
    {
        gameState = GameState.roomView;
    }

    void Update()
    {
        switch(gameState)
        {
            case GameState.roomView:
            if (roomCamera.enabled == false)
            {
                gameState = GameState.pcFocusView;
            }
            break;

            case GameState.pcFocusView:
            if (pcFocusCamera.enabled == false)
            {
                gameState = GameState.roomView;
            }
            break;
        }
    }

}
