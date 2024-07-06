using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool canFire = true;
    public float bufferTime = 0;
    private bool bufferState = false;

    [SerializeField] private bool pressedPlay = false;
    [SerializeField] private bool startedGame = false;
    [SerializeField] GameManager gameManager;
    [SerializeField] CameraShake cameraShakeScript;

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

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            cameraShakeScript = gameManager.mainCamera.GetComponent<CameraShake>();
            playerLocation = gameManager.player.GetComponent<Transform>();
            randomWait = UnityEngine.Random.Range(minWait, maxWait);
            MoveParticleEmitter();
        }

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            startedGame = false;
        } else 
        if (SceneManager.GetActiveScene().buildIndex == 1) 
        {
            startedGame = true;
            if (pressedPlay != true)
            {
                Start();
                pressedPlay = true;
            }
        }
        
        if (startedGame)
        {
            if (bufferTime > 0)
            {
                bufferTime -= Time.deltaTime;
            }
            else if (bufferTime <= 0 && bufferState == true)
            {
                canFire = true;
                bufferState = false;
            }

            if (canFire)
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
        bufferState = false;
        solarFlareParticleSystem.Play();
        Instantiate(solarFlarePrefab, transform.position + flareYOffset, Quaternion.LookRotation(playerLocation.position - this.transform.position));
        cameraShakeScript.StartCameraShake();
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
        canFire = false;
    }

    public void ResetFlare()
    {
        canFire = true;
    }

    public void BufferFlare()
    {
        bufferState = true;
        canFire = false;
        bufferTime = UnityEngine.Random.Range(minWait, maxWait);
    }
}
