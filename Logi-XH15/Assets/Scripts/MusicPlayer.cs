using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource loopSource;
    // Start is called before the first frame update
    void Awake()
    {
        if(!introSource || !loopSource)
        {
            Debug.Log("No Audio Sources Found!");
        }
        else
        {
            StartCoroutine(WaitForLoop());
        }
    }

    IEnumerator WaitForLoop(){
        yield return new WaitForSeconds(introSource.clip.length);
        loopSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
