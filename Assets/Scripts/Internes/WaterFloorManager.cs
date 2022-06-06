using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloorManager : MonoBehaviour
{
    public Material floorM;
    public float currentAlpha;
    //private float lowAlpha = 0;
    public float highAlpha = 0.2f;
    public float duration = 5;
    private bool appearFromFloor = false;

    private MeshRenderer floorR;
    private Vector3 resultingPosition;

    public Vector3 bigSize;
    private Vector3 currentSize;
    private Vector3 smallSize;

    private void OnEnable()
    {
        floorR = GetComponent<MeshRenderer>();
        floorR.enabled = false;
        //currentAlpha = lowAlpha;
        //floorM.SetFloat("_AlphaC", currentAlpha);

        smallSize = new Vector3(0, 0, 0);
        currentSize = smallSize;
        transform.localScale = currentSize;
        floorWater();
    }

    private void Update()
    {
        if (appearFromFloor == true)
        {
            transform.position = Vector3.Lerp(transform.position, resultingPosition, 1f * Time.deltaTime);
        }
    }
    public void floorWater()
    {
        floorR.enabled = true;
        StartCoroutine(floorTimeline(duration));
        FindObjectOfType<SoundManager>().anxiousWithWater();
        appearFromFloor = true;
        resultingPosition = Camera.main.transform.position + Camera.main.transform.up * (-7);
    }

    IEnumerator floorTimeline(float durationF)
    {
        StartCoroutine(lerpAlpha(bigSize, durationF));
        FindObjectOfType<WaterPPManager>().PPPresence(1);

        yield return new WaitForSeconds(durationF + 10);

        appearFromFloor = false;
        StartCoroutine(lerpAlpha(smallSize, durationF));
        FindObjectOfType<WaterPPManager>().PPPresence(0);

        yield return new WaitForSeconds(durationF);

        floorR.enabled = false;
        EventManager.TriggerEvent("disabledDistoredVision", true);
        Destroy(this.gameObject);
    }

    IEnumerator lerpAlpha(Vector3 targetSize, float durationFade)
    {
        float time = 0;

        while (time < durationFade)
        {
            //currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, time / durationFade);
            currentSize = Vector3.Lerp(currentSize, targetSize, time / durationFade);
            time += Time.deltaTime;
            transform.localScale = currentSize;
            //floorM.SetFloat("_AlphaC", currentAlpha);
            yield return null;
        }
    }
 
}
