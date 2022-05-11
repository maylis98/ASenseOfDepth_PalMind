using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PPManager : MonoBehaviour
{
    public float seconds = 0.1f;

    private Volume ppVolume;
    private void Awake()
    {
        ppVolume = GetComponent<Volume>();
        ppVolume.enabled = false;
    }

    public void IsBlinking()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        ppVolume.enabled = true;

        //1

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = false;

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = true;

        //2

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = false;

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = true;

        //3

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = false;

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = true;

        yield return new WaitForSeconds(seconds);

        ppVolume.enabled = false;
    }
}
