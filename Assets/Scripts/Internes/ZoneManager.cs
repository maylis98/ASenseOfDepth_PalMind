using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    private GameObject[] gatesInScene;

    MeshRenderer zoneMesh;
    MeshRenderer childrenZoneMesh;

    private void Start()
    {
        zoneMesh = GetComponent<MeshRenderer>();
        zoneMesh.enabled = false;
    }

    private void ShowGate()
    {
        FindObjectOfType<VFXGateManager>().GateAppear();
    }
    

    /*public void EnableMesh()
    {
        zoneMesh.enabled = true;
    }*/
}
