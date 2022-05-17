using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject pivotObj;
    public Vector3 rotationOrientation;

    private bool rotationEnabled;

    private void Awake()
    {
        rotationEnabled = true;
        EventManager.StartListening("stopRotation", conditionUpdater);
    }

    void Update()
    {
        if(rotationEnabled == true)
        {
            rotationAround();
        }
    }

    private void rotationAround()
    {
        transform.RotateAround(pivotObj.transform.position, rotationOrientation, rotationSpeed * Time.deltaTime);
    }

    private void conditionUpdater(object data)
    {
        bool answer = (bool)data;
        rotationEnabled = answer;
        
    }
}
