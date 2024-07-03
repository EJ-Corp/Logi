using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem solarFlareParticleSystem;
    [SerializeField] private GameObject solarFlarePrefab;
    [SerializeField] private Transform playerLocation;
    private int randomWait;
    public int minWait = 10;
    public int maxWait = 30;

    // Start is called before the first frame update
    void Start()
    {
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        StartCoroutine(FireSolarFlare());
    }

    private IEnumerator FireSolarFlare()
    {
        yield return new WaitForSeconds(randomWait);
        solarFlareParticleSystem.Play();
        Quaternion directionOfPlayer = Quaternion.LookRotation(playerLocation.position);
        Instantiate(solarFlarePrefab, new Vector3(0, 0, 0), new Quaternion(directionOfPlayer.x, 0, directionOfPlayer.z, directionOfPlayer.w));
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        FireSolarFlare();
    }
}
