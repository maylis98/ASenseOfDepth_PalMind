using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WaterPPManager : MonoBehaviour
{
    public float currentWeight;

    //private float highWeight = 1;
    private float lowWeight = 0;
    public float seconds = 5;

    private Volume ppVolume;
    private void Awake()
    {
        ppVolume = GetComponent<Volume>();
        currentWeight = lowWeight;
        ppVolume.weight = currentWeight;
    }

    public void PPPresence(float ppAlpha)
    {
        StartCoroutine(ExpandPP(ppAlpha, seconds));
    }

    IEnumerator ExpandPP(float endPos, float durationFade)
    {
        float time = 0;
        float startValue = currentWeight;
        while (time < durationFade)
        {
            currentWeight = Mathf.Lerp(startValue, endPos, time / durationFade);
            time += Time.deltaTime;
            ppVolume.weight = currentWeight;
            yield return null;
        }

    }

}
