using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXBugsManager : MonoBehaviour
{
    public VisualEffect VFXBugs;
    public Vector3 offsetFromCamera;

    private void Awake()
    {
        this.transform.position = Camera.main.transform.position + offsetFromCamera;
        //this.transform.LookAt(Camera.main.transform.position);

        VFXBugs.SetInt("Number of Particules", 0);
    }

    public void BugsAppear()
    {
        VFXBugs.SetInt("Number of Particules", 100000);
        VFXBugs.SetFloat("Attractive Strength", 11f);
    }

    public void BugsSpread()
    {
        VFXBugs.SetFloat("Attractive Strength", 1.6f);
    }
    public void BugsAttracted()
    {
        VFXBugs.SetFloat("Attractive Strength", 9f);
    }

    public void BugsDisappear()
    {
        if(this.gameObject.activeSelf == true)
        {
            VFXBugs.SetInt("Number of Particules", 0);
        }
        
    }



}
