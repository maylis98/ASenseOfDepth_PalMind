using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInGate : MonoBehaviour
{
    public GameObject fragment ;

    public AudioClip[] aClips;
    public AudioSource myAudioSource;
    string ObjectToTouch;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        fragment.SetActive(false);
    }
    void OnTriggerEnter(Collider camera)
    {
        if (camera.gameObject.tag == "Holder")
        {
            myAudioSource.clip = aClips[0];
            myAudioSource.Play();
            fragment.SetActive(true);
            /*StartCoroutine("WaitForSec");*/
        }
    }
    /*IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(fragment);
    }*/
}
