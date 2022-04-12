using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTextOnCircle : MonoBehaviour
{

    private MeshRenderer textMeshR;

    private void Start()
    {
        textMeshR = GetComponent<MeshRenderer>();
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

}
