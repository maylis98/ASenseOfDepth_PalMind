using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXGateManager : MonoBehaviour
{
    private VisualEffect VFXGate;

    private void Start()
    {
        VFXGate = GetComponent<VisualEffect>();

        VFXGate.SetFloat("Number of particules", 0f);
    }

    public void GateAppear()
    {
        VFXGate.SetFloat("Number of particules", 100000f);
    }

    public void GateDisappear()
    {
        VFXGate.SetFloat("Number of particules", 0f);
    }


}
