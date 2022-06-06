using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UncompleteSphereManager : MonoBehaviour
{
    public GameObject[] enceinteHalfs;
    public GameObject sphere;
    private float duration;

    private VisualEffect sphereVFX;
    private VisualEffect enceinteVFX;
    private Animator sphereA;
    private Vector3 resultingPosition;
    private AudioSource sphereAudio;
    private float valueToChangeSphere;
    private float valueToChangeHalf;

    private bool appearFromWater = false;

    // Start is called before the first frame update
    void Awake()
    {
        sphereA = sphere.GetComponent<Animator>();
        sphereAudio = sphere.GetComponent<AudioSource>();
        sphereAudio.Play();
        sphereVFX = sphere.GetComponent<VisualEffect>();
        resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * 3 + Camera.main.transform.up * 2;
        appearFromWater = true;

        foreach (GameObject enceinte in enceinteHalfs)
        {
            enceinteVFX = enceinte.GetComponent<VisualEffect>();
            enceinteVFX.SetFloat("Lifetime Expansion", 0f);
        }

        sphereVFX.SetFloat("Lifetime Expansion", 0.48f);
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("Catch this");
    }

    private void Update()
    {
        if (appearFromWater == true)
        {
            transform.position = Vector3.Lerp(transform.position, resultingPosition, 0.5f * Time.deltaTime);

        }
    }

    public void openSphere()
    {
        sphereA.SetBool("assemble", true);
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");
    }

    private void showObj()
    {

        foreach (GameObject enceinte in enceinteHalfs)
        {
            enceinteVFX = enceinte.GetComponent<VisualEffect>();
            enceinteVFX.SetFloat("Lifetime Expansion", 0.8f);
        }

        FindObjectOfType<SoundManager>().morecalmAfterText();
    }

    public void increaseObject()
    {
        foreach (GameObject enceinte in enceinteHalfs)
        {
            enceinteVFX = enceinte.GetComponent<VisualEffect>();
            enceinteVFX.SetFloat("Lifetime Expansion", 1.7f);
        }

        sphereVFX.SetFloat("Lifetime Expansion", 0.9f);
    }
    public void fadeObject()
    {
        sphereAudio.Play();
        appearFromWater = false;

        foreach (GameObject enceinte in enceinteHalfs)
        {
            enceinteVFX = enceinte.GetComponent<VisualEffect>();
            enceinteVFX.SetFloat("Lifetime Expansion", 0f);
        }

        sphereVFX.SetFloat("Lifetime Expansion", 0f);

        //StartCoroutine(fadeAway(0f, 0f, duration));
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
