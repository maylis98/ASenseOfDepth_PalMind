using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmitterOrder : MonoBehaviour
{
    public void SendData(int memoryNumber)
    {
        EventManager.TriggerEvent("UnlockMemory", memoryNumber);
        EventManager.TriggerEvent("cleanCanvas", true);
    }

}
