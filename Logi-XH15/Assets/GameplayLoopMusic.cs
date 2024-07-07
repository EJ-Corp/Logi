using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoopMusic : MonoBehaviour
{
    [SerializeField] private AudioClip gameplayIntro;
    [SerializeField] private AudioClip gameplayLoopClip;
    [SerializeField] private AudioSource gameplayLoop;
    void Start()
    {
        StartCoroutine(InitiateGameplayLoop());
        gameplayLoopClip.LoadAudioData();
    }
    IEnumerator InitiateGameplayLoop()
    {
        yield return new WaitForSeconds(gameplayIntro.length);
        gameplayLoop.Play();
    }
}
