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
    [SerializeField] public GameManager gameManager;
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
            pauseKey = KeyCode.Escape;
        }
        gameState = GameState.playing;

        if (SceneManager.GetActiveScene().name == "3 - Third Build") {
            playerController = gameManager.player.GetComponent<PlayerController>();
            gameplayHUDObject = gameManager.player.transform.GetChild(2).transform.GetChild(0).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "3 - Third Build")
        {
            if(gameManager != null && playerController == null)
            {
                Debug.Log("here");
                playerController = gameManager.player.GetComponent<PlayerController>();
                gameplayHUDObject = gameManager.player.transform.GetChild(2).transform.GetChild(0).gameObject;
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
        Debug.Log("Click");
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

    public void OptionsMenu()
    {
        pauseMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);
        gameState = GameState.options;
    }

    public void QuitMenu()
    {
        SceneManager.LoadScene(0);
    }
}
