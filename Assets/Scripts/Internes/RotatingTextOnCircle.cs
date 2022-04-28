using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTextOnCircle : MonoBehaviour
{
    private AudioSource audioText;
    private MeshRenderer textMeshR;

    private void Start()
    {
        textMeshR = GetComponent<MeshRenderer>();
        audioText = GetComponent<AudioSource>();
        reInitialiseText();
    }

    private void reInitialiseText()
    {
        textMeshR.enabled = false;
    }

    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    public void Blink()
    {

            if(textMeshR.enabled == false)
            {
                textMeshR.enabled = true;
            }

            else
            {
                textMeshR.enabled = false;
            }
    }

    public void disableBlink()
    {
        CancelInvoke("Blink");
        reInitialiseText();
    }

}
