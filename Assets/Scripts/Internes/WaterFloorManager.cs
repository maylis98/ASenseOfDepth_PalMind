using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloorManager : MonoBehaviour
{
    public Material floorM;
    public float currentAlpha;
    private float lowAlpha = 0;
    public float highAlpha = 0.2f;
    public float duration = 5;

    private MeshRenderer floorR;

    private void OnEnable()
    {
        floorR = GetComponent<MeshRenderer>();
        floorR.enabled = false;
        currentAlpha = lowAlpha;
        floorM.SetFloat("_AlphaC", currentAlpha);
    }
    public void floorWater()
    {
        floorR.enabled = true;
        StartCoroutine(floorTimeline(duration));
    }

    IEnumerator floorTimeline(float durationF)
    {
        StartCoroutine(lerpAlpha(highAlpha, durationF));

        yield return new WaitForSeconds(durationF + 10);

        StartCoroutine(lerpAlpha(lowAlpha, durationF));

        yield return new WaitForSeconds(durationF);

        floorR.enabled = false;
        this.gameObject.SetActive(false);
    }

    IEnumerator lerpAlpha(float targetAlpha, float durationFade)
    {
        float time = 0;

        while (time < durationFade)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, time / durationFade);
            time += Time.deltaTime;
            floorM.SetFloat("_AlphaC", currentAlpha);
            yield return null;
        }
    }
 
}
