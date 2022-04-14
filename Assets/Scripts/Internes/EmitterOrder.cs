using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterOrder : MonoBehaviour
{
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            
        }
        
    }*/

    public void SendData()
    {
        Debug.Log("data has been sent");
        EventManager.TriggerEvent("UnlockMemory", 0);
        EventManager.TriggerEvent("cleanCanvas", true);
    }
}
