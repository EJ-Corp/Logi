using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteracts : MonoBehaviour
{
    private static TutorialInteracts tutManager;
    public static TutorialInteracts TutManager
    {
        get
        {
            return tutManager;
        }
    }

    [SerializeField] private GameObject stellaText;

    // Start is called before the first frame update
    void Start()
    {
        if(tutManager == null)
        {
            tutManager = this;
        }

        stellaText = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        Debug.Log("Button Pressed");
        
        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = true;
        GameManager.Manager.player.GetComponent<PlayerController>().PlayerReticle.enabled = true;
        GameManager.Manager.isReading = false;
        Cursor.visible = false;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void SpawnTutorial()
    {
        gameObject.SetActive(true);
        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = false;
        GameManager.Manager.player.GetComponent<PlayerController>().PlayerReticle.enabled = false;
        GameManager.Manager.isReading = true;
        Cursor.visible = true;

    }
}
