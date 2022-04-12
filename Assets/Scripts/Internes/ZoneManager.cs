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

    public void ShowGate()
    {
        gatesInScene = GameObject.FindGameObjectsWithTag("Gate");
        MeshRenderer gateMeshR = gatesInScene[0].GetComponent<MeshRenderer>();
        gateMeshR.enabled = true;
    }

    /*public void EnableMesh()
    {
        zoneMesh.enabled = true;
    }*/
}
