using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalPresenceManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particules;
    private ParticleSystem.MainModule sysmain;
    private bool rotateTowards;
    private float distanceFromCamera = 2;
    private float speed = 0.5f;

    void Start()
    {
        rotateTowards = false;
        sysmain = particules.main;
        sysmain.startSize = 0f;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(rotateTowards == true)
        {
            Vector3 resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera;
            //transform.position = new Vector3(resultingPosition.x, transform.position.y, resultingPosition.z);
            transform.position = Vector3.Lerp(transform.position, resultingPosition, speed * Time.deltaTime);
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
        sysmain.startSize = 0f;

        yield return new WaitForSeconds(1);

        sysmain.startSize = 2f;
       
    }

    IEnumerator particulesDisappear()
    {
        sysmain.startSize = 2f;

        yield return new WaitForSeconds(1);

        sysmain.startSize = 0f;
        this.gameObject.SetActive(false);

    }


}
