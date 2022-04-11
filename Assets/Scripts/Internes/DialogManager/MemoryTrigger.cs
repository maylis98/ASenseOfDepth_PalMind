using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoryTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;

    public Memory memory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            Debug.Log("collision detected");
            onTrigger.Invoke();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<MemoryTextManager>().StartMemory(memory);
    }
}
