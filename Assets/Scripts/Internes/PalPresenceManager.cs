using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalPresenceManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particules;
    private ParticleSystem.MainModule sysmain;
    void Start()
    {
        sysmain = particules.main;
        sysmain.startSize = 0f;
        this.gameObject.SetActive(false);
    }

    public void startParticules()
    {
        StartCoroutine(particulesAppear());
    }

    public void stopParticules()
    {
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
