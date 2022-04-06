using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterOrder : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("data has been sent");
            EventManager.TriggerEvent("UnlockMemory", 0);
        }
        
    }
}
