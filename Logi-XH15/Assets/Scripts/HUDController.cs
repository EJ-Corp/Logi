using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image warningPanel;
    [SerializeField] private AudioSource warningAlarm;

    [SerializeField] private float flashInterval;
    [SerializeField] private float flashCooldown;

    enum WarningState
    {
        disabled, active
    };

    [SerializeField] private WarningState currentState;
    private bool panelActive = false;

    // Start is called before the first frame update
    void Start()
    {
        warningPanel.enabled = false;
        currentState = WarningState.disabled;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case WarningState.disabled:

            break;

            case WarningState.active:

                if(flashCooldown > 0)
                {
                    flashCooldown -= Time.deltaTime;
                }

                if(flashCooldown <= 0)
                {
                    Trigger();
                }
            break;
        }
    }

    public void Trigger()
    {
        if(panelActive)
        {
            warningPanel.enabled = false;
            panelActive = false;
            flashCooldown = flashInterval;
        } else 
        {
            warningPanel.enabled = true;
            panelActive = true;
            flashCooldown = flashInterval;    
        }
    }

    public void StartWarning()
    {
        currentState = WarningState.active;
        if(warningAlarm != null)
        {
            warningAlarm.Play();
        }
    }

    public void NoProblems()
    {
        warningPanel.enabled = false;
        currentState = WarningState.disabled;
        panelActive = false;
        if(warningAlarm != null)
        {
            warningAlarm.Stop();
        }
    }
}
