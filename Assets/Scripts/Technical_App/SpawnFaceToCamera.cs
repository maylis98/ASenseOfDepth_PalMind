using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFaceToCamera : MonoBehaviour
{
    public bool rotateTowards = false;
    public bool followCam = false;
    public float distanceFromCamera = 3;

    private bool placeTowards = false;
    private void Update()
    {
        if (placeTowards == true)
        {
            Vector3 resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * distanceFromCamera;

            transform.position = resultingPosition;

            if(followCam == true)
            {
                transform.position = Vector3.Lerp(transform.position, resultingPosition, 1f * Time.deltaTime);
            }

            if (rotateTowards == true)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, 1f * Time.deltaTime);
            }
        }
    }
    public void FrontCamera()
    {
        placeTowards = true;
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        rotateTowards = false;
        this.gameObject.SetActive(false);
    }
}
