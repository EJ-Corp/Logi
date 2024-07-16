using System;
using System.Collections;
using System.Collections.Generic;
using AOT;
using TMPro;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[Serializable] public class OptionsSlider
{
    public string sliderName;
    public float currentValue;
    public float minValue;
    public float maxValue;
    public TextMeshProUGUI textObject;
    public Slider sliderObject;
    public bool hasBeenChanged;
}

public class OptionsManager : MonoBehaviour
{
    //Reference to Audio Mixer
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private OptionsSlider[] optionsSliders;

    void Start()
    {
        for (int i = 0; i < optionsSliders.Length - 1; i++)
        {
            OptionsSlider option = optionsSliders[i];

            option.sliderObject.minValue = optionsSliders[i].minValue;
            option.sliderObject.maxValue = optionsSliders[i].maxValue;

            if(!option.hasBeenChanged)
            {
                option.currentValue = optionsSliders[i].maxValue;
                option.sliderObject.value = optionsSliders[i].maxValue;
                option.textObject.text = "100%";
            }

            option.sliderObject.onValueChanged.AddListener((value) => SetOptionLevel(option, value));
        }
    }

    public void SetOptionLevel(OptionsSlider option, float value)
    {
        option.currentValue = value;
        option.textObject.text = CalculatePercentage(option.currentValue, option.maxValue, option.minValue).ToString() + "%";
        option.hasBeenChanged = true;
        if(option.sliderName != "LookSensitivity")
        {
            HandleVolumeChange(option.sliderName, value);
        }
        else
        {
            HandleLookSensitivityChange();
        }
    }

    void HandleVolumeChange(string name, float value)
    {
        audioMixer.SetFloat(name, value);
        if(name == "SFXVolume")
        {
            audioMixer.SetFloat("FlareVolume", value);
        }
    }

    void HandleLookSensitivityChange()
    {

    }

    public int CalculatePercentage(float volume, float maxValue, float minValue)
    {
        int percent = (int)Mathf.Ceil((volume - minValue)/(maxValue - minValue) * 100);
        return percent;
    }
}
