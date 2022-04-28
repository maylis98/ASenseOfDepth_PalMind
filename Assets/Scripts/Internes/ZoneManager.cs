using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZoneManager : MonoBehaviour
{
    
    public GameObject zone;
    public AudioSource audioZone;

    [SerializeField]
    private UnityEvent whenZoneIsShown;

    [SerializeField]
    private UnityEvent whenZoneIsHidden;

    private GameObject[] gatesInScene;

    MeshRenderer zoneMesh;

    private void Awake()
    {
        /*zoneMesh = GetComponent<MeshRenderer>();
        zoneMesh.enabled = false;*/

        EventManager.StartListening("endZone", disableZone);
    }

    public void showGate()
    {
        FindObjectOfType<VFXGateManager>().GateAppear();
    }

    private void disableZone(object data)
    {
        if((bool)data)
        {
            Debug.Log("endZoneIsReceived");
            audioZone.Stop();
            whenZoneIsHidden.Invoke();
            //zone.SetActive(false);
            
        }
        
    }

    public void showZone()
    {
        audioZone.Play();
        whenZoneIsShown.Invoke();
    }

}
