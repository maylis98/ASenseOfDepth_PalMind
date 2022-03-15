using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyingGate : MonoBehaviour
{
    public GameObject objToDestroy;
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Holder"))
        {
        Destroy(objToDestroy);
        }
    }
}
