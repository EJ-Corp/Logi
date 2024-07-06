using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareEater : MonoBehaviour
{
    [SerializeField] private LayerMask flareLayer;

    private void OnTriggerEnter(Collider other) {
        GameObject collided = other.gameObject;

        if(flareLayer.Contains(collided))
        {
            Debug.Log("Eat Flare");
            collided.transform.GetComponent<SolarFlare>().BreakComputer();
            Destroy(collided.gameObject);
            SunManager.Sun.ResetFlare();
        }
    }
}
