using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float timeLeft;
    public bool timerOn = false;

    public TextMeshProUGUI TimerTxt;
   
    void Start()
    {
        timerOn = true;
    }

    void Update()
    {
        if(timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                updateTimer(timeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}