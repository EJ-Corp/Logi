using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareEater : MonoBehaviour
{
    [SerializeField] private LayerMask flareLayer;
    [SerializeField] private AudioClip flareCrackle;
    [SerializeField] private AudioClip pcPowerDown;

    private void OnTriggerEnter(Collider other) {
        GameObject collided = other.gameObject;

        if(flareLayer.Contains(collided))
        {
            //Debug.Log("Eat Flare");
            collided.transform.GetComponent<SolarFlare>().BreakComputer();
            SFXManager.Instance.PlaySFXClip(flareCrackle, transform, 1f);
            SFXManager.Instance.PlaySFXClip(pcPowerDown, transform, 1f);
            Destroy(collided.gameObject);
            SunManager.Sun.ResetFlare();
        }
    }
}
