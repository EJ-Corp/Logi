using System;
using System.Collections;
using System.Collections.Generic;
using AOT;
using TMPro;
using Unity.Mathematics;
// using UnityEditor.SearchService;
// using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable] public class OptionsSlider
{
    public string sliderName;
    public float currentValue;
    public float minValue;
    public float maxValue;
    public TextMeshProUGUI textObject;
    public Slider sliderObject;
}

public class OptionsManager : MonoBehaviour
{
    //Reference to Audio Mixer
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private OptionsSlider[] optionsSliders;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject menuCanvas;

    void Start()
    {
        menuCanvas = SunManager.Sun.mainMenu;
        InstantiateOptionsSliders(optionsSliders);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "3 - Third Build" && playerController == null)
        {
            playerController = GameManager.Manager.player.GetComponent<PlayerController>();
        } 
        
        if (SceneManager.GetActiveScene().buildIndex == 0){
            menuCanvas.SetActive(true);
        } else {
            menuCanvas.SetActive(false);
        }
    }

    void InstantiateOptionsSliders(OptionsSlider[] sliders)
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            Debug.Log(i);
            OptionsSlider option = sliders[i];

            option.sliderObject.minValue = sliders[i].minValue;
            option.sliderObject.maxValue = sliders[i].maxValue;
            option.currentValue = sliders[i].currentValue;
            option.sliderObject.value = sliders[i].currentValue;
            option.textObject.text = CalculatePercentage(option.currentValue, option.maxValue, option.minValue).ToString() + "%";
            option.sliderObject.onValueChanged.AddListener((value) => SetOptionLevel(option, value));
        }
    }

    public void SetOptionLevel(OptionsSlider option, float value)
    {
        option.currentValue = value;
        option.textObject.text = CalculatePercentage(option.currentValue, option.maxValue, option.minValue).ToString() + "%";
        if(option.sliderName != "LookSensitivity")
        {
            HandleVolumeChange(option.sliderName, value);
        }
        else
        {
            Debug.Log("Sensitivity");
            HandleLookSensitivityChange(option.sliderName, value);
        }
    }

    void HandleVolumeChange(string name, float value)
    {
        audioMixer.SetFloat(name, value);
        if(name == "SFXVolume")
        {
            audioMixer.SetFloat("FlareVolume", value);
        }
        foreach (OptionsSlider option in optionsSliders)
        {
            if (option.sliderName == name)
            {
                option.currentValue = value;
                option.sliderObject.value = value;
            }
        }
    }

    void HandleLookSensitivityChange(string name, float value)
    {
        Debug.Log("Sensitivity Change");
        if (playerController != null)
        {
            playerController.LookSpeed = value;
        }

        foreach (OptionsSlider option in optionsSliders)
        {
            if (option.sliderName == name)
            {
                option.currentValue = value;
                option.sliderObject.value = value;
            }
        }
    }

    public int CalculatePercentage(float volume, float maxValue, float minValue)
    {
        Debug.Log("Calculate Percentage");
        int percent = (int)Mathf.Ceil((volume - minValue)/(maxValue - minValue) * 100);
        return percent;
    }
}
