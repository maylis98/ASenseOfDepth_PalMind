using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PalBodyManager : MonoBehaviour
{
    public GameObject palBody;
    private Material palMaterial;
    private Animator palAnimator;
    private BoxCollider palCollider;
    public Color startColor;
    public Color endColor;
    public Color finalColor;
    public float transitionTime;

    void Awake()
    {
        palCollider = GetComponent<BoxCollider>();
        palCollider.enabled = false;

        palMaterial = palBody.GetComponent<SkinnedMeshRenderer>().sharedMaterial;
        palMaterial.SetColor("_Color_A", startColor);
        palAnimator = GetComponent<Animator>();

    }

    public void showBody()
    {
        StartCoroutine(LerpAlpha(endColor, transitionTime));
    }

    public void palDance()
    {
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");
        StopAllCoroutines();
        palAnimator.SetBool("endDance", true);
        StartCoroutine(Disappear(finalColor, 10));
    }

    IEnumerator LerpAlpha(Color lastColor, float durationFade)
    {
        float time = 0;
        Color startValue = startColor;
        while (time < durationFade)
        {
            startColor = Color.Lerp(startValue, lastColor, time / durationFade);
            time += Time.deltaTime;
            palMaterial.SetColor("_Color_A", startColor);
            yield return null;
        }

        yield return new WaitForSeconds(transitionTime + 2);

        palAnimator.SetBool("up", true);
        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("I feel myself again");
        palCollider.enabled = true;
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("Click on Pal");

    }

    IEnumerator Disappear(Color lastInvisibleColor, float durationFade)
    {
        float time = 0;
        Color startValue = startColor;
        while (time < durationFade)
        {
            startColor = Color.Lerp(startValue, lastInvisibleColor, time / durationFade);
            time += Time.deltaTime;
            palMaterial.SetColor("_Color_A", startColor);
            yield return null;
        }
       
        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("end Screen");
        this.gameObject.SetActive(false);

    }

}
