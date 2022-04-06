using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Triggers : MonoBehaviour
{
    [SerializeField]
    private UnityEvent trigger;

    [SerializeField]
    private UnityEvent after1Second;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trigger.Invoke();
            StartCoroutine(stopAnim());
        }
    }

    IEnumerator stopAnim()
    {
        yield return new WaitForSeconds(1);
        after1Second.Invoke();
    }
}
