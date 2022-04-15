using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    bool deleteZone;

    public GameObject zone;
    public AudioSource audioZone;

    private GameObject[] gatesInScene;

    MeshRenderer zoneMesh;
    MeshRenderer childrenZoneMesh;

    private void Start()
    {
        zoneMesh = GetComponent<MeshRenderer>();
        zoneMesh.enabled = false;
        deleteZone = false;

        EventManager.StartListening("endZone", endZone);
    }

    private void ShowGate()
    {
        FindObjectOfType<VFXGateManager>().GateAppear();
    }

    private void endZone(object data)
    {
        if(deleteZone = (bool)data)
        {
            audioZone.Stop();
            zone.SetActive(false);
            
        }
        
    }

}
