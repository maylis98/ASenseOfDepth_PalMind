using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmitterOrder : MonoBehaviour
{
    public void SendData(int memoryNumber)
    {
        Debug.Log("data has been sent");
        EventManager.TriggerEvent("UnlockMemory", memoryNumber);
        EventManager.TriggerEvent("cleanCanvas", true);
    }

}
