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

    private GameObject spawnedSphere;
    private GameObject spawnedGate;
    private int indexOfMemory;
    private bool theEnd;

    void Awake()
    {
        theEnd = false;
        EventManager.StartListening("UnlockMemory", UnlockMemory);
        EventManager.StartListening("endOfMemory", endThisMemory);
    }

    private void UnlockMemory(object data)
    {
        spawnedSphere = Instantiate(SpheresOfMemory[(int)data], Vector3.zero,Quaternion.identity);
        spawnedGate = Instantiate(GatesOfMemory[(int)data], triggerZone.transform.position + offsetFromObj, Quaternion.Euler(0, 90, 0));

        data = indexOfMemory;
        Debug.Log("index of current memory is " + indexOfMemory);

    }

    private void endThisMemory(object data)
    {
        if(theEnd = (bool)data)
        {
            Destroy(spawnedSphere);
            Destroy(spawnedGate);
            FindObjectOfType<CanvasManager>().deleteEndText();
            Debug.Log("c'est la fin de ce souvenir");
        }
    }

       
}
