using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXGateManager : MonoBehaviour
{
    private VisualEffect VFXGate;
    private AudioSource audioGate;

    private void Start()
    {
        VFXGate = GetComponent<VisualEffect>();
        audioGate = GetComponent<AudioSource>();

        VFXGate.SetFloat("Number of particules", 0f);
    }

    public void GateAppear()
    {
        audioGate.Play();
        VFXGate.SetFloat("Number of particules", 100000f);
    }

    public void GateDisappear()
    {
        audioGate.Play();
        VFXGate.SetFloat("Number of particules", 0f);
    }


}
