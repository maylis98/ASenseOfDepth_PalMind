using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField, Range(0,7)]
    private float spread = 0;

    // Update is called once per frame
    void Update()
    {
        visualEffect.SetFloat("Spread", spread);  
    }
}
