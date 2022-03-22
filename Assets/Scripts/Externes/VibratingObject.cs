using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibratingObject : MonoBehaviour
{

    [SerializeField]
    private bool vibrating = false;

    [SerializeField]
    private float vibAmount = 8;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (vibrating)
        {

            Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.deltaTime * vibAmount);
            newPos.y = transform.position.y;
            newPos.z = transform.position.z;

            transform.position = newPos;

        }
    }

    public void Vibration()
    {
        StartCoroutine("VibrateNow");
    }

    IEnumerator VibrateNow()
    {
        if (vibrating == false)
        {
            vibrating = true;
        }

        yield return new WaitForSeconds(0.25f);

        vibrating = false;
        transform.position = originalPos;
    }
}
