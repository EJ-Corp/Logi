using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] private float shakeTime;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject handle;
    [SerializeField] private GameObject anchor;

    public void StartCameraShake()
    {
        StartCoroutine(ShakeThatCamera());
    }

    public IEnumerator ShakeThatCamera()
    {
        Vector3 startPosition = transform.localPosition;
        float timeUsed = 0f;

        while (timeUsed < shakeTime)
        {
            timeUsed += Time.deltaTime;
            float strength = curve.Evaluate(timeUsed / shakeTime);
            transform.localPosition = startPosition + Random.insideUnitSphere * strength;
            // Debug.Log(handle.transform.parent);
            // Debug.Log(playerCamera.transform);
            if(handle.transform.parent == playerCamera.transform)
            {
                Debug.Log("Dropping Handle");
                handle.transform.parent = anchor.transform;
                handle.GetComponent<Rigidbody>().isKinematic = false;
            }
            yield return null;
        }

        transform.localPosition = startPosition;
    }
}
