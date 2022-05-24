using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXBugsManager : MonoBehaviour
{
    public VisualEffect VFXBugs;
    public GameObject bugTarget;
    public Vector3 offsetFromCamera;

    private void Awake()
    {
        this.transform.position = Camera.main.transform.position;

        Vector3 resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * 200;
        bugTarget.transform.position = Vector3.Lerp(transform.position, resultingPosition, 0.5f * Time.deltaTime);
        //this.transform.position = Camera.main.transform.position + offsetFromCamera;
        //this.transform.LookAt(Camera.main.transform.position);

        VFXBugs.SetInt("Number of Particules", 0);
    }

    public void BugsAppear()
    {
        VFXBugs.SetInt("Number of Particules", 100000);
        VFXBugs.SetFloat("Attractive Strength", 1.7f) ;
    }

    public void BugsSpread()
    {
        VFXBugs.SetFloat("Attractive Strength", 0f) ;
    }
    public void BugsAttracted()
    {
        VFXBugs.SetFloat("Attractive Strength", 0.19f) ;
    }

    public void BugsDisappear()
    {
        if(this.gameObject.activeSelf == true)
        {
            VFXBugs.SetInt("Number of Particules", 0);
        }
        
    }



}
