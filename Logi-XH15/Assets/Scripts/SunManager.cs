using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem solarFlareParticleSystem;
    [SerializeField] private GameObject solarFlarePrefab;
    [SerializeField] private Transform playerLocation;
    private Vector3 targetedParticlePosition;
    [SerializeField] private float randomWait;
    public float minWait = 10;
    public float maxWait = 30;
    public float targetedFlareDistance = 0.5f;
    [SerializeField] Vector3 flareYOffset;
    private bool canFire = true;

    private static SunManager sun;
    public static SunManager Sun
    {
        get
        {
            return sun;
        }
    }

    void Awake()
    {
        if(sun == null)
        {
            sun = this;
        }
    }

    void Start()
    {
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
    }

    void Update()
    {
        if(canFire)
        {
            if(randomWait > 0)
            {
                randomWait -= Time.deltaTime;
            }

            if(randomWait <= 0)
            {
                FireSolarFlare();
            }
        }
    }

    void MoveParticleEmitter()
    {
        solarFlareParticleSystem.Stop();
        Vector3 direction = playerLocation.position - this.transform.position;
        Vector3 unitDirection = direction.normalized;
        targetedParticlePosition = this.transform.position + unitDirection * targetedFlareDistance;
        solarFlareParticleSystem.gameObject.transform.position = targetedParticlePosition;
        solarFlareParticleSystem.gameObject.transform.LookAt(playerLocation.transform.position);
        solarFlareParticleSystem.transform.Rotate(180, 0, 0);
    }

    public void FireSolarFlare()
    {
        solarFlareParticleSystem.Play();
        Instantiate(solarFlarePrefab, transform.position + flareYOffset, Quaternion.LookRotation(playerLocation.position - this.transform.position));
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
        canFire = false;
    }

    public void ResetFlare()
    {
        canFire = true;
    }
}
