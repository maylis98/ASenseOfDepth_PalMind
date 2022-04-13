using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoryTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;

    public Memory memory;

    public GameObject[] floatingObjs;

    private void Start()
    {
        foreach(GameObject floatObj in floatingObjs)
        {
            floatObj.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            onTrigger.Invoke();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<MemoryTextManager>().StartMemory(memory);
    }

    public void ShowParallelDimension()
    {
        foreach (GameObject floatObj in floatingObjs)
        {
            floatObj.SetActive(true);
        }
    }
}
