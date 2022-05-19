using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistoredParticulesManager : MonoBehaviour
{
    //public GameObject postProcessingVolume;
    private bool isFinished;
    public GameObject ObjToDestroy;

    void Awake()
    {
        EventManager.StartListening("disabledDistoredVision", disableDistoredVision);
        isFinished = false;

    }

    public void distoredVision()
    {
        //Attach Distortion Field to camera
        Transform holderPos = GameObject.Find("Holder").transform;
        this.transform.position = holderPos.position;
        this.transform.parent = holderPos;
        //postProcessingVolume.SetActive(true);
    }

    private void disableDistoredVision(object data)
    {

        if(isFinished = (bool)data)
        {
            Destroy(ObjToDestroy);
        }
        
    }
}
