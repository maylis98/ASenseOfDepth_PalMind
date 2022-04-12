using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneTriggers : MonoBehaviour
{
    [SerializeField]
    private UnityEvent triggerWithCam;
    [SerializeField]
    private UnityEvent triggerWithSphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            triggerWithCam.Invoke();
        }

        /*if (other.CompareTag("Sphere"))
        {
            Debug.Log("collision here");
            triggerWithSphere.Invoke();
        }*/
    }


}
