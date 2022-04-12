using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistoredParticulesManager : MonoBehaviour
{
    //public GameObject postProcessingVolume;

    public void distoredVision()
    {
        //Attach Distortion Field to camera
        Transform holderPos = GameObject.Find("Holder").transform;
        this.transform.position = holderPos.position;
        this.transform.parent = holderPos;
        //postProcessingVolume.SetActive(true);
    }
}
