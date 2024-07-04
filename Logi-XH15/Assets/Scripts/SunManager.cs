using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem solarFlareParticleSystem;
    [SerializeField] private GameObject solarFlarePrefab;
    [SerializeField] private Transform playerLocation;
    private Vector3 targetedParticlePosition;
    private int randomWait;
    public int minWait = 10;
    public int maxWait = 30;
    public float targetedFlareDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
    }

    void MoveParticleEmitter()
    {
        solarFlareParticleSystem.Stop();
        Vector3 direction = playerLocation.position - this.transform.position;
        Vector3 unitDirection = direction.normalized;
        targetedParticlePosition = this.transform.position + unitDirection * targetedFlareDistance;
        solarFlareParticleSystem.gameObject.transform.position = targetedParticlePosition;
        solarFlareParticleSystem.gameObject.transform.LookAt(playerLocation.transform.position);
        solarFlareParticleSystem.transform.Rotate(180, 0, 0);
        StartCoroutine(FireSolarFlare());
    }

    private IEnumerator FireSolarFlare()
    {
        yield return new WaitForSeconds(randomWait);
        solarFlareParticleSystem.Play();
        yield return new WaitForSeconds(1.5f);
        Instantiate(solarFlarePrefab, transform.position, Quaternion.LookRotation(playerLocation.position - this.transform.position));
        randomWait = UnityEngine.Random.Range(minWait, maxWait);
        MoveParticleEmitter();
    }
}
