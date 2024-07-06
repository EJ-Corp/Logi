using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WarningLights : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private Renderer rend;
    [SerializeField] private Light[] warningLights;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject lightRotator;
    [SerializeField] private bool isFlashing;
    [SerializeField] private int flashingSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rend.enabled = true;
        rend.sharedMaterial = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(isFlashing)
        {
            Flash();
        }
        else
        {
            StopFlash();
        }
    }

    void Flash()
    {
        rend.sharedMaterial = materials[1];
        foreach(Light warningLight in warningLights)
        {
            warningLight.intensity = 100;
        }
        lightRotator.transform.Rotate(0, flashingSpeed, 0);
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopFlash()
    {
        rend.sharedMaterial = materials[0];
        foreach(Light warningLight in warningLights)
        {
            warningLight.intensity = 0;
        }
        audioSource.Stop();
    }  

    public void SetFlashing(bool flashingState)
    {
        isFlashing = flashingState;
    }
}