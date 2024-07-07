using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCalculator : MonoBehaviour
{
    [SerializeField] private TimerScript timerScript;
    [SerializeField] private ShipIntegrity shipIntegrity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerScript.timeLeft <= 0)
        {
            SceneManager.LoadScene(2);
        }
        if(shipIntegrity.currentShipHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
}
