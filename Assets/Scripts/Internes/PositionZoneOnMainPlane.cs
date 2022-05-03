using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PositionZoneOnMainPlane : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager arPlaneManager;

    void Awake()
    {

        //this.transform.position = Camera.main.transform.position + offset;
    }

    public void placeOnCenterOfPlane(ARPlane planeToPlaceObj)
    {
        this.transform.position = planeToPlaceObj.transform.position;
    }

}
