using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoriesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SpheresOfMemory;

    [SerializeField]
    private GameObject[] GatesOfMemory;

    public GameObject triggerZone;
    [SerializeField]
    private Vector3 offsetFromObj;
    public Vector3 initGateRotation;

    void Awake()
    {
        EventManager.StartListening("UnlockMemory", UnlockMemory);
    }

    private void UnlockMemory(object data)
    {
        Debug.Log("we received {data}");
        GameObject spawnedSphere = Instantiate(SpheresOfMemory[(int)data], Vector3.zero,Quaternion.identity);
        GameObject spawnedGate = Instantiate(GatesOfMemory[(int)data], triggerZone.transform.position + offsetFromObj, Quaternion.Euler(0, 90, 0));

        MeshRenderer gateMeshR = spawnedGate.GetComponent<MeshRenderer>();
        gateMeshR.enabled = false;
    }

       
}
