using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager instance;
    public static SFXManager Instance
    {
        get
        {
            return instance;
        }
    }
    [SerializeField] AudioSource sfxObject;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if(audioClip != null)
        {
            //Spawn in gameObject
            AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

            //Load Audio Clip
            audioSource.clip = audioClip;

            //Assign Volume
            audioSource.volume = volume;

            //Play the clip
            audioSource.Play();

            //Destroy the gameObject
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
        
    }

    public void PlayRandomSFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume)
    {
        //Assign random integer
        int rand = Random.Range(0, audioClip.Length);

        //Spawn in gameObject
        AudioSource audioSource = Instantiate(sfxObject, spawnTransform.position, Quaternion.identity);

        //Load Audio Clip
        audioSource.clip = audioClip[rand];

        //Assign Volume
        audioSource.volume = volume;

        //Play the clip
        audioSource.Play();

        //Destroy the gameObject
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
