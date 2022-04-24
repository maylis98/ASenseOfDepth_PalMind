using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXMemoryManager : MonoBehaviour
{
    public VisualEffect VFXCoral;
    public GameObject enceinte;
    public GameObject palBodyFragment;
    private AudioSource audioBodyFragment;

    private Animator enceinteAnimator;
    private AudioSource enceinteAudio;
    private VisualEffect VFXenceinte;

    private BoxCollider VFXCoralCollider;

    private void Awake()
    {

        VFXCoral.SetFloat("Flux Intensity", 4f);
        VFXCoral.SetVector3("Transform_scale", new Vector3(2, 2, 2));

        //Enable memory Collider at first
        VFXCoralCollider = VFXCoral.GetComponent<BoxCollider>();
        VFXCoralCollider.enabled = false;

        enceinteAudio = enceinte.GetComponent<AudioSource>();
        enceinteAnimator = enceinte.GetComponent<Animator>();
        VFXenceinte = enceinte.GetComponent<VisualEffect>();

        audioBodyFragment = palBodyFragment.GetComponent<AudioSource>();

        //Desactivate Pal Body's fragment at first
        palBodyFragment.SetActive(false);
        audioBodyFragment.Play();
        
    }

    public void UnifiedMemory()
    {
        StopAllCoroutines();

        VFXCoral.SetFloat("Lifetime Expansion", 2f);
        VFXCoral.SetFloat("Spread", 0f);


        VFXCoral.SetFloat("Flux Intensity", 0.01f) ;
        VFXCoral.SetVector3("Transform_scale", new Vector3(5, 5, 5));

        enceinteAnimator.SetBool("endPoint", true);
        enceinteAudio.Play();

        VFXCoralCollider.enabled = true;


    }

    public void MemoryDisappearArmAppear()
    {

        FindObjectOfType<VFXGateManager>().GateDisappear();
        //enceinteAnimator.SetBool("disappear", true);
        VFXenceinte.SetFloat("Particules", 0f);

        VFXCoral.SetFloat("Flux Intensity", 1.3f);
        VFXCoral.SetFloat("Number of particules", 0f);

        palBodyFragment.SetActive(true);
        audioBodyFragment.Play();

        FindObjectOfType<CanvasManager>().sentenceInEndText("This is a part of Pal's body");
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("[ Click ]");

    }
}
