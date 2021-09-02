using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{



    public AudioSource EventSource;
    public AudioSource OperationSource;
    public AudioClip SpawnSFX;
    public AudioClip WrongSFX;
    public AudioClip RightSFX;
    public Task CurrentTask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEventSFX(AudioClip clip)
    {
        if (!CurrentTask.Audio)
        {
            return;
        }
        EventSource.clip = clip;
        EventSource.Play();
    }

    public void PlayOperationSFX(AudioClip clip)
    {
        if (!CurrentTask.Audio)
        {
            return;
        }
        OperationSource.clip = clip;
        OperationSource.Play();
    }
}
