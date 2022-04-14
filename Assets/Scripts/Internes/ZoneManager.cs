using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    bool deleteZone;

    public GameObject zone;

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
            zone.SetActive(false);
        }
        
    }



    /*public void EnableMesh()
    {
        zoneMesh.enabled = true;
    }*/
}
