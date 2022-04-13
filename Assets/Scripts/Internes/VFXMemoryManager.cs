using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXMemoryManager : MonoBehaviour
{
    public VisualEffect VFXCoral;
    public Animator enceinteAnimator;

    private void Awake()
    {

        //VFXCoral = GetComponent<VisualEffect>();
        VFXCoral.SetFloat("Flux Intensity", 4f);
        VFXCoral.SetVector3("Transform_scale", new Vector3(2, 2, 2));
    }

    public void UnifiedMemory()
    {
        StopAllCoroutines();

        VFXCoral.SetFloat("Lifetime Expansion", 2f);
        VFXCoral.SetFloat("Spread", 0f);
        VFXCoral.SetFloat("Flux Intensity", 0.01f) ;
        VFXCoral.SetVector3("Transform_scale", new Vector3(5, 5, 5));

        enceinteAnimator.SetBool("endPoint", true);



    }
}
