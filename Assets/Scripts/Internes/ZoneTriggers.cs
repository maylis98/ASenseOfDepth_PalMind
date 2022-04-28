using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneTriggers : MonoBehaviour
{
    [SerializeField]
    private UnityEvent triggerWithCam;

    [SerializeField]
    private UnityEvent mouseDown;

    private void Start()
    {
        BoxCollider triggerCollider = GetComponent<BoxCollider>();
        triggerCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            triggerWithCam.Invoke();
        }
    }

    private void OnMouseDown()
    {
        mouseDown.Invoke();
    }




}
