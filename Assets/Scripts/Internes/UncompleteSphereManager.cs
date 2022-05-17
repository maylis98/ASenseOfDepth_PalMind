using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UncompleteSphereManager : MonoBehaviour
{
    public GameObject[] enceinteHalfs;
    public float duration;

    private VisualEffect sphereVFX;
    private VisualEffect enceinteVFX;

    private AudioSource sphereAudio;
    private float valueToChangeSphere;
    private float valueToChangeHalf;

    // Start is called before the first frame update
    void Awake()
    {

        valueToChangeHalf = 0f;
        valueToChangeSphere = 0f;

        sphereAudio = GetComponent<AudioSource>();
        sphereAudio.Play();
        sphereVFX = GetComponent<VisualEffect>();
        sphereVFX.SetFloat("Lifetime Expansion", valueToChangeHalf);

        foreach (GameObject enceinte in enceinteHalfs)
        {
            enceinteVFX = enceinte.GetComponent<VisualEffect>();
            enceinteVFX.SetFloat("Lifetime Expansion", valueToChangeSphere);
        }

        showObj();
    }

    private void showObj()
    {
        StartCoroutine(fadeAway(0.48f, 0.8f, 20));
    }

    public void increaseObject()
    {
        StartCoroutine(fadeAway(0.9f, 1.7f, duration));
    }
    public void fadeObject()
    {
        sphereAudio.Play();
        StartCoroutine(fadeAway(0f, 0f, duration));
    }

    public void hideUncompleteS()
    {
        EventManager.TriggerEvent("clearUncompleteS", true);
    }

    IEnumerator fadeAway(float sphereEndValue, float halfEndValue, float durationToFade)
    {
        float time = 0;

        while (time < duration)
        {
            valueToChangeHalf = Mathf.Lerp(valueToChangeHalf, halfEndValue, time / durationToFade);
            valueToChangeSphere = Mathf.Lerp(valueToChangeSphere, sphereEndValue, time / durationToFade);
            time += Time.deltaTime;

            foreach (GameObject enceinte in enceinteHalfs)
            {
                enceinteVFX = enceinte.GetComponent<VisualEffect>();
                enceinteVFX.SetFloat("Lifetime Expansion", valueToChangeHalf);
            }

            sphereVFX.SetFloat("Lifetime Expansion", valueToChangeSphere);

            yield return null;
        }

    }
}
