using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    public Transform holdParent;
    public GameObject postProcessingVolume;
    public GameObject grabbedObj;

    public AudioClip[] aClips;
    public AudioSource myAudioSource;

    public GameObject[] floatingObjs;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();

        postProcessingVolume.SetActive(false);
        foreach(GameObject floatObj in floatingObjs)
        {
            floatObj.SetActive(false);
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Holder")
        {
            Debug.Log("Collision detected");

            //GetComponent<Rigidbody>().useGravity = false;

            //Play music
            myAudioSource.clip = aClips[0];
            myAudioSource.Play();

            //Attach Obj to camera
            grabbedObj.transform.position = holdParent.position;
            grabbedObj.transform.parent = GameObject.Find("Holder").transform;
            postProcessingVolume.SetActive(true);

            //Make other objects appearing
            foreach (GameObject floatObj in floatingObjs)
            {
                floatObj.SetActive(true);
            }
        }

        /*if (other.gameObject.tag == "Dropper")
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
        }*/
    }

}


