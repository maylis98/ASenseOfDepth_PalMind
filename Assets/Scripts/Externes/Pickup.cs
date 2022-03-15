using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform holdParent;
    public GameObject postProcessingVolume;

    private void Start()
    {
        postProcessingVolume.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Holder"))
        {
       
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = holdParent.position;
            this.transform.parent = GameObject.Find("Holder").transform;
            postProcessingVolume.SetActive(true);
        }

        /*if (other.gameObject.tag == "Dropper")
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
        }*/
    }

}


