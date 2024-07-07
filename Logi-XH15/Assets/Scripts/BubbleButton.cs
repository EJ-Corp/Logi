using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleButton : MonoBehaviour
{
    private MonitorScript computerScreen;
    // Start is called before the first frame update
    void Start()
    {
        computerScreen = GameObject.FindGameObjectWithTag("PCScreen").transform.GetComponent<MonitorScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseClick(int annoyID)
    {
        computerScreen.ClosedAnnoy(annoyID);
        Destroy(transform.parent.gameObject);
    }
}
