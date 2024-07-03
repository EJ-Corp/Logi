using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemHandler : MonoBehaviour
{

    [SerializeField] private float timeToBreak;
    [SerializeField] private float breakageTimer;

    [SerializeField] private GameObject warningPanel;

    [SerializeField] private float flashInterval;
    [SerializeField] private float flashCooldown;
    [SerializeField] private float warningFlashCount;

    enum WarningState
    {
        diabled, active
    };

    [SerializeField] private WarningState currentState;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        warningPanel.SetActive(false);
        breakageTimer = timeToBreak;
    }

    // Update is called once per frame
    void Update()
    {
        //Count time for time to break -> down to 0
        if(breakageTimer > 0)
        {
            breakageTimer -= Time.deltaTime;
        }

        //When timer reaches 0 -> break
        if(breakageTimer <= 0)
        {
            currentState = WarningState.active;
        }

        switch(currentState)
        {
            case WarningState.diabled:
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
        if(active)
        {
            warningPanel.SetActive(false);
            active = false;
            flashInterval = warningFlashCount;
            flashCooldown = flashInterval;
        } else 
        {
            warningPanel.SetActive(true);
            active = true;
            flashInterval = warningFlashCount;
            flashCooldown = flashInterval;
        }
    }
}
