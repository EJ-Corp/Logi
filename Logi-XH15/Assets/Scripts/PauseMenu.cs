using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey;
    [SerializeField] private GameObject pauseMenuObject;
    [SerializeField] private GameObject optionsMenuObject;
    [SerializeField] private GameObject gameplayHUDObject;
    [SerializeField] private PlayerController playerController;
    public enum GameState
    {
        playing,
        paused,
        options
    }
    public GameState gameState;

    void Start()
    {
        if(pauseKey == KeyCode.None)
        {
            pauseKey = KeyCode.P;
        }
        gameState = GameState.playing;

        if (SceneManager.GetActiveScene().name == "3 - Third Build") {
            playerController = GameManager.Manager.player.GetComponent<PlayerController>();
            gameplayHUDObject = GameManager.Manager.player.transform.GetChild(2).transform.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Manager.isReading == false)
        {
            ResumeGame();
        } else
        {
            Reading();
        }

        if(SceneManager.GetActiveScene().name == "3 - Third Build")
        {
            if(GameManager.Manager != null && playerController == null)
            {
                //Debug.Log("here");
                playerController = GameManager.Manager.player.GetComponent<PlayerController>();
                gameplayHUDObject = GameManager.Manager.player.transform.GetChild(2).transform.GetChild(0).gameObject;
            }

            if(Input.GetKeyDown(pauseKey))
            {
                switch (gameState)
                {
                    case GameState.playing:
                        PauseGame();
                        break;
                    case GameState.paused:
                        ResumeGame();
                        break;
                    case GameState.options:
                        PauseGame();
                        break;
                }
            }
            if(gameState != GameState.playing)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                playerController.enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                playerController.enabled = true;
            }
        }
    }

    public void ResumeGame()
    {
        //Debug.Log("Click");
        gameplayHUDObject.SetActive(true);
        pauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        gameState = GameState.playing;
    }

    public void PauseGame()
    {
        if(gameState == GameState.playing)
        {
            gameplayHUDObject.SetActive(false);
            pauseMenuObject.SetActive(true);
            Time.timeScale = 0f;
            gameState = GameState.paused;
        }
        else if(gameState == GameState.options)
        {
            pauseMenuObject.SetActive(true);
            optionsMenuObject.SetActive(false);
            gameState = GameState.paused;
        }
    }

    public void Reading()
    {
        Time.timeScale = 0f;
        gameState = GameState.paused;
    }

    public void OptionsMenu()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);
        gameState = GameState.options;
    }

    public void QuitMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }
}
