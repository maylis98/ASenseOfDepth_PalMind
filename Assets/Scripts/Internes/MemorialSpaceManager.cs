using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MemorialSpaceManager : MonoBehaviour
{
    public float duration;
    public UnityEvent whenSpaceAppear;
    public Volume PP;

    private float valueToChange;
    private float PPValueToChange;
    private float highParticules = 100000f;
    private float lowParticules = -100f;

    private VisualEffect VFXGate;
    private AudioSource audioGate;

    private void Awake()
    {
        VFXGate = GetComponent<VisualEffect>();
        audioGate = GetComponent<AudioSource>();

        VFXGate.SetFloat("Number of particules", 0f);
        PP.weight = 0f;

        memorialSpaceAppearance();
    }

    private void memorialSpaceAppearance()
    {
        StartCoroutine(memorialSpaceTimeline());
        whenSpaceAppear.Invoke();
    }

    private void showMemorialSpace()
    {
        StartCoroutine(fadeAway(highParticules, 1f, duration));
    }

    private void hideMemorialSpace()
    {
        StartCoroutine(fadeAway(lowParticules, 0f, duration));
    }

    IEnumerator memorialSpaceTimeline()
    {
        showMemorialSpace();

        yield return new WaitForSeconds(duration + 5);

        hideMemorialSpace();

        //yield return new WaitForSeconds(duration + 5);

        EventManager.TriggerEvent("disabledDistoredVision", true);

        yield return new WaitForSeconds(duration + 5);

        EventManager.TriggerEvent("clearMemorialSpace", true);
        
    }

    IEnumerator fadeAway(float endValue, float PPEndValue, float durationToFade)
    {
        float time = 0;
        float startValue = valueToChange;
        float PPstartValue = PPValueToChange;

        while (time < duration)
        {
            valueToChange = Mathf.Lerp(startValue, endValue, time / durationToFade);
            PPValueToChange = Mathf.Lerp(PPstartValue, PPEndValue, time / durationToFade);
            time += Time.deltaTime;
            PP.weight = PPValueToChange;
            VFXGate.SetFloat("Number of particules", endValue);
            yield return null;
        }

    }
}
