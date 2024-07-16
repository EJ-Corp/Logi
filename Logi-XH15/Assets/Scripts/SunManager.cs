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
    [SerializeField] private float minWait;
    [SerializeField] private float maxWait;
    [SerializeField] private float targetedFlareDistance = 0.5f;
    [SerializeField] Vector3 flareYOffset;

    [SerializeField] private bool bufferState = true;
    [SerializeField] private bool canFire = false;
    [SerializeField] private bool isFiring = false;
    [SerializeField] private bool hasFired = false;

    [SerializeField] private float bufferTime;
    [SerializeField] private bool hasSunBuffered;


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
    public bool StartedGame
    {
        get { return startedGame; }
    }
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
            DontDestroyOnLoad(this.gameObject);
        } else 
        if (sun != this)
        {
            Destroy(gameObject);
        }


    }

    void Start()
    {

        //for debug reasons
        startedGame = true;
        pressedPlay = true;
        
        if (SceneManager.GetActiveScene().name == "3 - Third Build") {
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

            bufferTime = UnityEngine.Random.Range(minWait, maxWait);
            randomWait = UnityEngine.Random.Range(minWait, maxWait);
            MoveParticleEmitter();
        }

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            startedGame = false;
            pressedPlay = false;
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

        if (SceneManager.GetActiveScene().name.Contains("Scene"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SetStateBooleans(false, false, false);
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

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                LoopThroughLights();
            }

            switch(currentState)
            {
                case WarningState.buffer:
                    hasSunBuffered = true;
                    if (bufferTime > 0)
                    {
                        bufferTime -= Time.deltaTime;
                    }
                    if (bufferTime <= 0)
                    {
                        SetStateBooleans(false, true, false);
                    }
                break;

                case WarningState.firing:
                    if (randomWait > 0)
                    {
                        randomWait -= Time.deltaTime;
                    }
                    if (randomWait <= 0)
                    {
                        SetStateBooleans(false, false, true);
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
        SetStateBooleans(false, false, false);
        solarFlareParticleSystem.Play();
        
        SFXManager.Instance.PlaySFXClip(alarmSFX, transform, 0.3f);

        Instantiate(solarFlarePrefab, transform.position + flareYOffset, Quaternion.LookRotation(playerLocation.position - this.transform.position));
        cameraShakeScript.StartCameraShake();
        // randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
    }

    public void ResetFlare()
    {
        SetStateBooleans(false, true, false);
        hasFired = false;
        warningPanel.enabled = false;
        panelActive = false;
        hasSunBuffered = false;
        flashCooldown = 0;
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
    }

    public void BufferFlare()
    {
        if (hasSunBuffered != true)
        {
            if (currentState != WarningState.buffer)
            {
                SetStateBooleans(true, false, false);
                bufferTime = UnityEngine.Random.Range(minWait, maxWait);
            }
            hasSunBuffered = true;
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

    void SetStateBooleans(bool buffer, bool fireStart, bool fireMid)
    {
        bufferState = buffer;
        canFire = fireStart;
        isFiring = fireMid;
    }
}
