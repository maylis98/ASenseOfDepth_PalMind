using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoriesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SpheresOfMemory;

    void Awake()
    {
        EventManager.StartListening("UnlockMemory", UnlockMemory);
    }

    private void UnlockMemory(object data)
    {
        Debug.Log("we received {data}");
        GameObject spawnedSphere = Instantiate(SpheresOfMemory[(int)data], Vector3.zero,Quaternion.identity);
    }

       
}
