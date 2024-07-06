using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCameraAnimator : MonoBehaviour
{
    private Animator cameraAnimator;
    [SerializeField] CanvasGroup homeCanvasGroup;
    [SerializeField] CanvasGroup optionsCanvasGroup;
    [SerializeField] CanvasGroup creditsCanvasGroup;
    [SerializeField] GameObject home;
    [SerializeField] GameObject options;
    [SerializeField] GameObject credits;
    public bool isHomeTransition;
    public bool isOptionsTransition;
    public bool isCreditsTransition;
    public float fadeoutSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
        options.SetActive(false);
        credits.SetActive(false);
    }
    public void ToOptions()
    {
        cameraAnimator.SetTrigger("toOptions");
        isOptionsTransition = true;
        options.SetActive(true);
    }
    public void ToCredits()
    {
        cameraAnimator.SetTrigger("toCredits");
        isCreditsTransition = true;
        credits.SetActive(true);
    }
    public void ToHome()
    {
        cameraAnimator.SetTrigger("toHome");
        isHomeTransition = true;
        home.SetActive(true);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void LoadScene()
    {   
        SceneManager.LoadScene("Level2");
    }

    void Update()
    {
        if(isOptionsTransition && optionsCanvasGroup.alpha <= 1)
        {
            optionsCanvasGroup.alpha += Time.deltaTime;
            homeCanvasGroup.alpha -= Time.deltaTime * fadeoutSpeed;
            if(optionsCanvasGroup.alpha >= 1)
            {
                isOptionsTransition = false;
                home.SetActive(false);
            }
        }
        if(isCreditsTransition && creditsCanvasGroup.alpha <= 1)
        {
            creditsCanvasGroup.alpha += Time.deltaTime;
            homeCanvasGroup.alpha -= Time.deltaTime * fadeoutSpeed;
            if(creditsCanvasGroup.alpha >= 1)
            {
                isCreditsTransition = false;
                home.SetActive(false);
            }
        }
        if(isHomeTransition && homeCanvasGroup.alpha <= 1)
        {
            homeCanvasGroup.alpha += Time.deltaTime;
            creditsCanvasGroup.alpha -= Time.deltaTime * fadeoutSpeed;
            optionsCanvasGroup.alpha -= Time.deltaTime * fadeoutSpeed;
            if(homeCanvasGroup.alpha >= 1)
            {
                isHomeTransition = false;
                options.SetActive(false);
                credits.SetActive(false);
            }
        }
    }
}
