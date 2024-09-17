using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialInteracts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        
        GameManager.Manager.player.GetComponent<PlayerController>().CanMove = true;
        GameManager.Manager.player.GetComponent<PlayerController>().PlayerReticle.enabled = true;
        GameManager.Manager.isReading = false;
        Cursor.visible = false;
        GameManager.Manager.CloseTutorialText();
    }
}
