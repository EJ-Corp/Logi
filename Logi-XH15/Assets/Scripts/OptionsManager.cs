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

    //Current Values
    [Header("Current Values")]
    [SerializeField] private float masterVolume;
    [SerializeField] private float musicVolume;
    [SerializeField] private float sfxVolume;

    //Max Volumes
    [Header("Maximum Values")]
    [SerializeField] private float masterMax;
    [SerializeField] private float musicMax;
    [SerializeField] private float sfxMax;

    //Min Volumes
    [Header("Minimum Values")]
    [SerializeField] private float masterMin;
    [SerializeField] private float musicMin;
    [SerializeField] private float sfxMin;

    //TMP Objects
    [Header("TMP Objects")]
    [SerializeField] private TextMeshProUGUI masterVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    //Slider Objects
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private OptionsSlider[] optionsSliders;

    void Start()
    {
        for (int i = 1; i < optionsSliders.Length; i++)
        {
            OptionsSlider option = optionsSliders[i];

            option.sliderObject.minValue = optionsSliders[i].minValue;
            option.sliderObject.maxValue = optionsSliders[i].minValue;

            if(!option.hasBeenChanged)
            {
                option.sliderObject.value = optionsSliders[i].maxValue;
                option.textObject.text = "100%";
            }
        }
    }

    public void SetVolumeLevel(OptionsSlider option, float value)
    {
        audioMixer.SetFloat(option.sliderName, value);
        option.currentValue = value;
        option.textObject.text = CalculatePercentage(option.currentValue, option.maxValue, option.minValue).ToString() + "%";
    }

    public void SetMusicVolumeLevel(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        musicVolume = volume;
        musicVolumeText.text = CalculatePercentage(musicVolume, musicMax, musicMin).ToString() + "%";
    }

    public void SetSFXVolumeLevel(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("FlareVolume", volume);
        sfxVolume = volume;
        sfxVolumeText.text = CalculatePercentage(sfxVolume, sfxMax, sfxMin).ToString() + "%";
    }

    public int CalculatePercentage(float volume, float maxValue, float minValue)
    {
        int percent = (int)Mathf.Ceil((volume - minValue)/(maxValue - minValue) * 100);
        return percent;
    }
}
