using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem sparksSystem;
    [SerializeField] private bool enableSparks;
    [SerializeField] private bool isSparking;
    [SerializeField] private float timer;

    void Start()
    {
        isSparking = true;
        timer = 0f;
    }

    void Update()
    {
        if(enableSparks)
        {
            HandleSparks();
        }
        else
        {
            DisableSparks();
        }
    }

    void HandleSparks()
    {
        if(timer <= 0 && isSparking)
        {
            sparksSystem.Play();
            isSparking = false;
            timer = 1f;
        }
        else if(timer <= 0 && !isSparking)
        {
            isSparking = true;
            timer = 1f;
        }

        timer -= Time.deltaTime;
    }

    void DisableSparks()
    {
        sparksSystem.Stop();
        timer = 0f;
        isSparking = true;
    }
}
