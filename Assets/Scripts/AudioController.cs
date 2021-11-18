using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : MonoBehaviour
{


    //used for new order show audiosource
    public AudioSource EventSource;
    //used for the result of operation 
    public AudioSource OperationSource;
    //new order spawn sfx
    public AudioClip SpawnSFX;
    //triggered wrong button sfx
    public AudioClip WrongSFX;
    //triggered right button sfx
    public AudioClip RightSFX;
    //TouchPanel.Instance.CurrentTask
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
