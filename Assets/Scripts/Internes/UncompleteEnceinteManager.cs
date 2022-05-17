using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncompleteEnceinteManager : MonoBehaviour
{
    public GameObject pivotObj;
    public Vector3 rotationOrientation;
    public float rotationSpeed;

    void Update()
    {
        transform.RotateAround(pivotObj.transform.position, rotationOrientation, rotationSpeed * Time.deltaTime);
    }
}
