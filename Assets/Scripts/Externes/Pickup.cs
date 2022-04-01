using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    //Launcher
    public Transform holdParent;
    public GameObject postProcessingVolume;
    public GameObject grabbedObj;

    //Sounds
    public AudioClip[] aClips;
    public AudioSource myAudioSource;

    //Objects to hide until Gate is reached
    public GameObject[] floatingObjs;

    //VFX
    [SerializeField]
    private VisualEffect visualEffect;

    //VFX properties
    [SerializeField, Range(0, 7)]
    private float spread = 0;

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
        if (other.gameObject.tag =="MainCamera")
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
            visualEffect.SetFloat("Spread", spread);
            foreach (GameObject floatObj in floatingObjs)
            {
                floatObj.SetActive(true);
            }
            
            StartCoroutine(revealObj());
        }

        /*if (other.gameObject.tag == "Dropper")
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
        }*/
    }

    IEnumerator revealObj()
    {
        yield return new WaitForSeconds(3);
        visualEffect.SetFloat("Spread", 0);
    }

}


