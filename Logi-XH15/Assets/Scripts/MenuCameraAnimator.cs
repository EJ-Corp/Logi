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
    private bool isHomeTransition;
    private bool isOptionsTransition;
    private bool isCreditsTransition;
    [SerializeField] private float fadeoutSpeed;

    enum MenuState
    {
        homeMenu,
        optionMenu,
        creditsMenu
    };

    [SerializeField] private MenuState menuState;

    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
        options.SetActive(false);
        credits.SetActive(false);
    }

    void Update()
    {
       // this.transform.rotation = Quaternion.Euler(0.0f, 0, 0.0f);
        // if (SunManager.Sun != null)
        // {
        //     home = SunManager.Sun.mainMenu.transform.GetChild(0).gameObject;
        //     homeCanvasGroup = home.GetComponent<CanvasGroup>();

        //     options = SunManager.Sun.mainMenu.transform.GetChild(1).gameObject;
        //     optionsCanvasGroup = options.GetComponent<CanvasGroup>();

        //     credits = SunManager.Sun.mainMenu.transform.GetChild(2).gameObject;
        //     creditsCanvasGroup = credits.GetComponent<CanvasGroup>();
        // }

        if (isHomeTransition)
        {
            menuState = MenuState.homeMenu;
        }
        else if (isOptionsTransition)
        {
            menuState = MenuState.optionMenu;
        }
        else if (isCreditsTransition)
        {
            menuState = MenuState.creditsMenu;
        }

        switch(menuState)
        {
            case MenuState.homeMenu:

                if(homeCanvasGroup.alpha <= 1)
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
            break;

            case MenuState.optionMenu:

                if(optionsCanvasGroup.alpha <= 1)
                {
                    optionsCanvasGroup.alpha += Time.deltaTime;
                    homeCanvasGroup.alpha -= Time.deltaTime * fadeoutSpeed;

                    if(optionsCanvasGroup.alpha >= 1)
                    {
                        isOptionsTransition = false;
                        home.SetActive(false);
                    }
                }
            break;

            case MenuState.creditsMenu:

                if(creditsCanvasGroup.alpha <= 1)
                {
                    creditsCanvasGroup.alpha += Time.deltaTime;
                    homeCanvasGroup.alpha -= Time.deltaTime * fadeoutSpeed;

                    if(creditsCanvasGroup.alpha >= 1)
                    {
                        isCreditsTransition = false;
                        home.SetActive(false);
                    }
                }
            break;
        }
    }

    public void ToOptions()
    {
        cameraAnimator.SetTrigger("toOptions");

        isHomeTransition = false;
        isOptionsTransition = true;
        isCreditsTransition = false;

        options.SetActive(true);
    }
    public void ToCredits()
    {
        cameraAnimator.SetTrigger("toCredits");

        isHomeTransition = false;
        isOptionsTransition = false;
        isCreditsTransition = true;

        credits.SetActive(true);
    }
    public void ToHome()
    {
        cameraAnimator.SetTrigger("toHome");

        isHomeTransition = true;
        isOptionsTransition = false;
        isCreditsTransition = false;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
    void OldUpdate()
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
    */
}
