using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PalBodyManager : MonoBehaviour
{
    public GameObject palBody;
    public GameObject lightOrb;
    public GameObject veil;
    public Material veilM;

    public Color currentColor_A;
    public Color currentColor_B;
    public Color blueFill;
    public Color whiteFill;
    public Color disappearC_A;
    public Color disappearC_B;

    public Color initVeilC;
    public Color transparentVeilC;
    public float transitionTime;

    private Material palMaterial;
    private Animator palAnimator;
    private CapsuleCollider palCollider;
    private Color currentVeilC;

    private int state = 0;
    private float countDown = 2;


    void Awake()
    {
        lightOrb.SetActive(false);
        veil.SetActive(false);
        palCollider = GetComponent<CapsuleCollider>();
        palCollider.enabled = false;

        currentVeilC = initVeilC;
        veilM.color = currentVeilC;

        palMaterial = palBody.GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        currentColor_A = disappearC_A;
        palMaterial.SetColor("_Color_A", currentColor_A);

        currentColor_B = disappearC_B;
        palMaterial.SetColor("_Color_B", currentColor_B);
        palAnimator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (state == 1)
        {
            currentColor_A = Color.Lerp(currentColor_A, blueFill, 0.2f * Time.deltaTime);
            currentColor_B = Color.Lerp(currentColor_B, whiteFill, 1f * Time.deltaTime);
            palMaterial.SetColor("_Color_A", currentColor_A);
            palMaterial.SetColor("_Color_B", currentColor_B);
        }



        if (state == 2)
        {
            currentVeilC = Color.Lerp(currentVeilC, transparentVeilC, 0.5f * Time.deltaTime);
            veilM.color = currentVeilC;
        }



        if (state == 3)
        {
            currentColor_A = Color.Lerp(currentColor_A, disappearC_A, 0.2f * Time.deltaTime);
            currentColor_B = Color.Lerp(currentColor_B, disappearC_A, 1f * Time.deltaTime);
            palMaterial.SetColor("_Color_A", currentColor_A);
            palMaterial.SetColor("_Color_B", currentColor_B);
        }
    }

    public void showBody()
    {
        EventManager.TriggerEvent("clearCanvas", true);
        state = 1;
        StartCoroutine(LerpAlpha());
    }

    public void palDance()
    {
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");
        StopAllCoroutines();
        palAnimator.SetBool("endDance", true);
        state = 4;
        StartCoroutine(Disappear());
    }

    IEnumerator LerpAlpha()
    {

        state = 1; 

        yield return new WaitForSeconds(5);

        veil.SetActive(true);
        state = 2;
        lightOrb.SetActive(true);

        yield return new WaitForSeconds(2);

        palCollider.enabled = true;
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("Click on Pal");

    }

    IEnumerator Disappear()
    {
        veilM.color = transparentVeilC;
        palAnimator.SetBool("endDance", true);

        yield return new WaitForSeconds(5);

        state = 3;

        yield return new WaitForSeconds(1);

        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("end Screen");
        //FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("end of Game");

        Destroy(this.gameObject);
    }

}
