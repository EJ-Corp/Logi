using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShipIntegrity : MonoBehaviour
{
    [SerializeField] private int fullShipHealth;
    [SerializeField] public int currentShipHealth;
    [SerializeField] private int timerFull;
    [SerializeField] private float timerCountdown;
    [SerializeField] private int damageScaleFactor;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI damageIndicatorText;
    [SerializeField] private string damageIndicatorSegment = "! ";
    [SerializeField] private ProblemTimer problemTimer;
    // Start is called before the first frame update
    void Start()
    {
        fullShipHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(problemTimer.problemIDPool.Count < 2)
        {
            isTakingDamage = true;
        }else{
            isTakingDamage = false;
        }
        if(isTakingDamage)
        {
            currentShipHealth -= Time.deltaTime * damageScaleFactor;
        }*/
        damageScaleFactor = 3 - problemTimer.problemIDPool.Count;

        if(timerCountdown > 0)
        {
            timerCountdown -= Time.deltaTime;
        }else{
            CalculateDamage();
            timerCountdown = timerFull;
        }
        healthText.text = currentShipHealth.ToString() + "%";
        damageIndicatorText.text = string.Concat(Enumerable.Repeat(damageIndicatorSegment, damageScaleFactor));
    }

    void CalculateDamage()
    {
        int damageTaken = damageScaleFactor;
        currentShipHealth -= damageTaken;
    }
}
