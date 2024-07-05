using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] private float shakeTime;

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
            yield return null;
        }

        transform.localPosition = startPosition;
    }
}
