using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXMemoryManager : MonoBehaviour
{
    public VisualEffect VFXCoral;
    //public GameObject enceinte;
    public GameObject palBodyFragment;
    public float currentAlpha;
    private float fadeIn = 0;
    public float fadeOut = 1;

    private AudioSource audioBodyFragment;

    /*private Animator enceinteAnimator;
    private AudioSource enceinteAudio;
    private VisualEffect VFXenceinte;*/

    private BoxCollider VFXCoralCollider;

    public Vector3 UnifiedVFXscale;

    private void Awake()
    {
       
        currentAlpha = fadeIn;

        VFXCoral.SetFloat("Spread", 3f);
        VFXCoral.SetFloat("Flux Intensity", 0.3f);
        VFXCoral.SetVector3("Transform_scale", UnifiedVFXscale);

        //Enable memory Collider at first
        VFXCoralCollider = VFXCoral.GetComponent<BoxCollider>();
        VFXCoralCollider.enabled = false;

        //enceinteAudio = enceinte.GetComponent<AudioSource>();
        //enceinteAnimator = enceinte.GetComponent<Animator>();
        //VFXenceinte = enceinte.GetComponent<VisualEffect>();

        audioBodyFragment = palBodyFragment.GetComponent<AudioSource>();

        //Desactivate Pal Body's fragment at first
        palBodyFragment.SetActive(false);
        audioBodyFragment.Play();
        
    }

    public void UnifiedMemory()
    {
        VFXCoral.SetFloat("Lifetime Expansion", 2f);
        VFXCoral.SetFloat("Spread", 0f);


        VFXCoral.SetFloat("Flux Intensity", 0.01f) ;
        VFXCoral.SetVector3("Transform_scale", UnifiedVFXscale);

        /*enceinteAnimator.SetBool("endPoint", true);
        enceinteAudio.Play();*/

        VFXCoralCollider.enabled = true;
        
    }

    public void MemoryDisappearArmAppear()
    {

        FindObjectOfType<VFXGateManager>().GateDisappear();
        //StartCoroutine(lerpAlpha(fadeOut, 2));
        //enceinteAnimator.SetBool("disappear", true);
       /* VFXenceinte.SetFloat("Particules", 0f);*/

        VFXCoral.SetFloat("Flux Intensity", 1.3f);
        VFXCoral.SetFloat("Number of particules", 0f);
        VFXCoralCollider.enabled = false;
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");
        EventManager.TriggerEvent("disabledDistoredVision", true);

        palBodyFragment.SetActive(true);
        audioBodyFragment.Play();

        //FindObjectOfType<CanvasManager>().sentenceInEndText("This is a part of Pal's body");
        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Body fragment");

    }

    IEnumerator lerpAlpha(float targetAlpha, float durationFade)
    {
        float time = 0;

        while (time < durationFade)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, time / durationFade);
            time += Time.deltaTime;
            //rippleM.SetFloat("_AlphaCC", currentAlpha);
            yield return null;
        }
    }
}
