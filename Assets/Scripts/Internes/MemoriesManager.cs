using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoriesManager : MonoBehaviour
{
    public GameObject MemorialSpace;

    public GameObject UncompleteSphere;

    [SerializeField]
    private GameObject[] SpheresOfMemory;

    [SerializeField]
    private GameObject[] GatesOfMemory;

    [SerializeField]
    private GameObject PalBody;


    public UnityEvent onEnd;

    public GameObject triggerZone;
    [SerializeField]
    private Vector3 offsetFromObj;
    [SerializeField]
    private Vector3 offsetFromCamera;
    public Vector3 initGateRotation;

    private GameObject spawnedUncompleteS;
    private GameObject spawnedMemorialSpace;
    private GameObject spawnedSphere;
    private GameObject spawnedGate;
    private int indexOfMemory;
    private bool theEnd;

    void Awake()
    {
        theEnd = false;
        EventManager.StartListening("UnlockMemory", UnlockMemory);
        EventManager.StartListening("endOfMemory", endThisMemory);
        EventManager.StartListening("clearMemorialSpace", hideMSpace);
        EventManager.StartListening("clearUncompleteS", hideUncompleteS);
    }

    public void showUncompleteS()
    {
        spawnedUncompleteS = Instantiate(UncompleteSphere, Camera.main.transform.position + offsetFromCamera, Quaternion.Euler(0, 0, 0));
    }

    public void showMSpace()
    {
        spawnedMemorialSpace = Instantiate(MemorialSpace, triggerZone.transform.position + offsetFromObj, Quaternion.Euler(0, 90, 0));
    }

    private void hideMSpace(object data) 
    {
        bool hideSpace = true;
        if (hideSpace == (bool)data)
        {
            Destroy(spawnedMemorialSpace);
        }
    }

    private void hideUncompleteS(object data)
    {
        bool hideUncompleteS = true;
        if (hideUncompleteS == (bool)data)
        {
            Destroy(spawnedUncompleteS);
        }
    }

    private void UnlockMemory(object data)
    {
        FindObjectOfType<NativeWebsocketChat>().SendChatMessage("player static");
        FindObjectOfType<SoundManager>().sphereAppear();
        EventManager.TriggerEvent("clearCanvas", true);
        EventManager.TriggerEvent("showZone", true);

        spawnedSphere = Instantiate(SpheresOfMemory[(int)data], Vector3.zero,Quaternion.identity);
        spawnedGate = Instantiate(GatesOfMemory[(int)data], triggerZone.transform.position + offsetFromObj, Quaternion.Euler(0, 90, 0));

    }

    private void endThisMemory(object data)
    {
        if(theEnd = (bool)data)
        {
            Destroy(spawnedSphere);
            Destroy(spawnedGate);
            //FindObjectOfType<CanvasManager>().deleteEndText();
            onEnd.Invoke();
        }
    }
    public void showPalBody()
    {
        spawnedGate = Instantiate(PalBody, Camera.main.transform.position + offsetFromCamera, Quaternion.Euler(0, 180, 0));
        FindObjectOfType<PalBodyManager>().showBody();
    }

       
}
