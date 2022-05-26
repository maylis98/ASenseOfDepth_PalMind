using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistoredParticulesManager : MonoBehaviour
{
    //public GameObject postProcessingVolume;
    private bool isFinished;
    private bool followCam = false;
    public GameObject ObjToDestroy;

    void Awake()
    {
        EventManager.StartListening("disabledDistoredVision", disableDistoredVision);
        isFinished = false;

    }

    private void Update()
    {
        if (followCam == true)
        {
            Vector3 resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * 1 + Camera.main.transform.up * (-1);
            //transform.position = new Vector3(resultingPosition.x, transform.position.y, resultingPosition.z);
            transform.position = Vector3.Lerp(transform.position, resultingPosition, 2 * Time.deltaTime);
        }
    }

    public void distoredVision()
    {
        followCam = true;
        //Attach Distortion Field to camera


        /*Transform holderPos = GameObject.Find("Holder").transform;
        this.transform.position = holderPos.position;
        this.transform.parent = holderPos;*/
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
