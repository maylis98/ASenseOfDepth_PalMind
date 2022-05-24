using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalPresenceWithText: MonoBehaviour
{
    public GameObject objToFollow;
    public Color targetColor;
    public Color initialColor;
    public Material particulesM;

    [SerializeField]
    private ParticleSystem particules;
    private ParticleSystem.MainModule sysmain;
    private bool followText;
    private float distanceFromObj = 2;
    private float speed = 0.5f;
    private Color currentColor;

    void OnEnable()
    {
        transform.position = new Vector3((int)Camera.main.transform.position.x, (int)Camera.main.transform.position.y, (int)Camera.main.transform.position.z);
        followText = false;
        sysmain = particules.main;

        startFollowing();

    }

    private void Update()
    {
        if(followText == true)
        {
            Vector3 resultingPosition = objToFollow.transform.position + objToFollow.transform.forward * distanceFromObj;
            //transform.position = new Vector3(resultingPosition.x, transform.position.y, resultingPosition.z);
            transform.position = Vector3.Lerp(transform.position, resultingPosition, speed * Time.deltaTime);
        }
        
    }

    public void startFollowing()
    {
        followText = true;
        StartCoroutine(particulesAppear());
    }

    public void stopFollowing()
    {
        followText = false;
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


    }


}
