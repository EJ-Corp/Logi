using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemHandler : MonoBehaviour
{
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private AudioClip alarmSFX;

    [SerializeField] private float flashInterval;
    [SerializeField] private float flashCooldown;

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
    }

    // Update is called once per frame
    void Update()
    {
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
            flashCooldown = flashInterval;
        } else 
        {
            warningPanel.SetActive(true);
            active = true;
            flashCooldown = flashInterval;
            SFXManager.Instance.PlaySFXClip(alarmSFX, transform, 1f);
        }
    }

    public void FixProblem()
    {
        warningPanel.SetActive(false);
        currentState = WarningState.diabled;
        active = false;
    }

    public void StartProblem()
    {
        currentState = WarningState.active;
    }
}
