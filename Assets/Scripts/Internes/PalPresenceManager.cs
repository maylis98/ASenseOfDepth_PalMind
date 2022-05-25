using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalPresenceManager : MonoBehaviour
{
    public Color targetColor;
    public Color initialColor;
    public Material particulesM;
    public float distanceFromCamera = 10;

    public GameObject palOrb;

    [SerializeField]
    private ParticleSystem particules;
    private ParticleSystem.MainModule sysmain;
    private bool rotateTowards;
    private float speed = 1f;
    private Color currentColor;

    void Start()
    {
        currentColor = initialColor;
        particulesM.SetColor("_TintColor", currentColor);
        rotateTowards = false;
        sysmain = particules.main;

        palOrb.SetActive(false);
    }

    private void Update()
    {
        if(rotateTowards == true)
        {
            Vector3 resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera;
            //transform.position = new Vector3(resultingPosition.x, transform.position.y, resultingPosition.z);
            transform.position = Vector3.Lerp(transform.position, resultingPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, speed * Time.deltaTime);
        }
        
    }

    public void startParticules()
    {
        rotateTowards = true;
        StartCoroutine(particulesAppear());
    }

    public void stopParticules()
    {
        rotateTowards = false;
        StartCoroutine(particulesDisappear());
    }

    IEnumerator particulesAppear()
    {
        currentColor = targetColor;
        particulesM.SetColor("_TintColor", currentColor);

        sysmain.startSize = 0f;

        float time = 0;
        float durationFade = 2;

        while (time < durationFade)
        {
            currentColor = Color.Lerp(currentColor, initialColor, time / durationFade);
            time += Time.deltaTime;
            particulesM.SetColor("_TintColor", currentColor);
            yield return null;
        }

        yield return new WaitForSeconds(durationFade);

        sysmain.startSize = 2f; 
       
    }

    IEnumerator particulesDisappear()
    {
        float time = 0;
        float durationFade = 2;

        while (time < durationFade)
        {
            currentColor = Color.Lerp(currentColor, targetColor, time / durationFade);
            time += Time.deltaTime;
            particulesM.SetColor("_TintColor", currentColor);
            yield return null;
        }

        palOrb.SetActive(false);

    }


}
