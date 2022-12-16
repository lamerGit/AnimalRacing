using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    AudioSource myAudio;
    private static ClickSound instance = null;
    public static ClickSound Instance
    {
        get
        {
            
            return instance; 
        }
    }

    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.mute=true;
        StartCoroutine(muteOff());
        instance = this;
    }


    public void ClickPlay()
    {
        myAudio.Play();
    }

    IEnumerator muteOff()
    {
        yield return new WaitForSeconds(0.5f);
        myAudio.mute = false;
    }
}
