using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SunManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem solarFlareParticleSystem;
    [SerializeField] private GameObject solarFlarePrefab;
    [SerializeField] private Transform playerLocation;
    private Vector3 targetedParticlePosition;
    [SerializeField] private float randomWait;
    [SerializeField] private float minWait = 10;
    [SerializeField] private float maxWait = 30;
    [SerializeField] private float targetedFlareDistance = 0.5f;
    [SerializeField] Vector3 flareYOffset;

    [SerializeField] private bool bufferState = true;
    [SerializeField] private bool canFire = false;
    [SerializeField] private bool isFiring = false;
    [SerializeField] private bool hasFired = false;

    [SerializeField] private float bufferTime = 10f;


    //all stuff that has been moved from problem 
    [Header("Problems")]
    [SerializeField] private Image warningPanel;
    [SerializeField] private float flashInterval;
    [SerializeField] private float flashCooldown;
    [SerializeField] private Light[] problemLights = new Light[4];
    enum WarningState
    {
        buffer, firing, fired
    };
    [SerializeField] private WarningState currentState;
    private bool panelActive = false;
    [SerializeField] private AudioClip alarmSFX;


    [Header("OtherStuff")]
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


            //problem stuff
            warningPanel = GameManager.Manager.problemPanel;
            warningPanel.enabled = false;
            for (int i = 0; i < problemLights.Length; i++)
            {
                problemLights[i] = gameManager.warningLights[i];  
                problemLights[i].enabled = false;
            }


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
        
        if (bufferState == true)
        {
            currentState = WarningState.buffer;
        } else
        if (canFire == true)
        {
            currentState = WarningState.firing;
        } else 
        if (isFiring == true)
        {
            currentState = WarningState.fired;
        }

        if (startedGame)
        {
            LoopThroughLights();

            switch(currentState)
            {
                case WarningState.buffer:
                    if (bufferTime > 0)
                    {
                        bufferTime -= Time.deltaTime;
                    }
                    if (bufferTime <= 0)
                    {
                        SetBooleans(false, true, false, false);
                    }
                break;

                case WarningState.firing:
                    if (randomWait > 0)
                    {
                        randomWait -= Time.deltaTime;
                    }
                    if (randomWait <= 0)
                    {
                        SetBooleans(false, false, true, false);
                    }

                break;

                case WarningState.fired:
                    if (hasFired != true)
                    {
                        FireSolarFlare();
                        hasFired = true;
                    }

                    if (flashCooldown > 0)
                    {
                        flashCooldown -= Time.deltaTime;
                    }
                    if (flashCooldown <= 0)
                    {
                        FlashPanel();
                    }
                break;
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
        SetBooleans(false, false, false, true);
        solarFlareParticleSystem.Play();
        
        SFXManager.Instance.PlaySFXClip(alarmSFX, transform, 0.75f);

        Instantiate(solarFlarePrefab, transform.position + flareYOffset, Quaternion.LookRotation(playerLocation.position - this.transform.position));
        cameraShakeScript.StartCameraShake();
        // randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
    }

    public void ResetFlare()
    {
        SetBooleans(false, true, false, false);
        warningPanel.enabled = false;
        panelActive = false;
        flashCooldown = 0;
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
    }

    public void BufferFlare()
    {
        if (currentState != WarningState.buffer)
        {
            SetBooleans(true, false, false, false);
            bufferTime = UnityEngine.Random.Range(minWait, maxWait);
        }

    }

    public void FlashPanel()
    {
        warningPanel.enabled = !warningPanel.enabled;
        panelActive = !panelActive;
        flashCooldown = flashInterval;
    }

    void LoopThroughLights() 
    {
        for (int i = 0; i < problemLights.Length; i++)
        {
            if (currentState == WarningState.buffer || currentState == WarningState.firing) {
                problemLights[i].enabled = false;
            } else {
                problemLights[i].enabled = true;
            }
            
        }
    }

    void SetBooleans(bool buffer, bool fireStart, bool fireMid, bool fireEnd)
    {
        bufferState = buffer;
        canFire = fireStart;
        isFiring = fireMid;
        hasFired = fireEnd;
    }
}
