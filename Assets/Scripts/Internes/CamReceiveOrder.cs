using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamReceiveOrder : MonoBehaviour
{
    private bool theEnd;
    private AudioSource mainAudio;
    void Start()
    {
        theEnd = false;
        mainAudio = GetComponent<AudioSource>();
        EventManager.StartListening("UnlockMemory", startSound);
        EventManager.StartListening("endOfMemory", endSound);
    }

    private void startSound(object data)
    {
        if ((int)data>= 0)
        {
            mainAudio.Play();
        }
    }

    private void endSound(object data)
    {
        if (theEnd = (bool)data)
        {
            mainAudio.Stop();
        }
    }

}
