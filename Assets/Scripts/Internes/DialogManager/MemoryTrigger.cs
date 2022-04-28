using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

public class MemoryTrigger : MonoBehaviour
{
    public UnityEvent onTrigger;

    public UnityEvent mouseDown;

    public Memory memory;

    public GameObject[] floatingObjs;

    public Animator vaseAnimator;

    private AudioSource audioDistortionField;


    private void Start()
    {
        audioDistortionField = this.gameObject.GetComponent<AudioSource>();

        foreach (GameObject floatObj in floatingObjs)
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

    private void OnMouseDown()
    {
        mouseDown.Invoke();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<MemoryTextManager>().StartMemory(memory);
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("[ Click on text to continue the memory ]");
    }

    public void ShowParallelDimension()
    {
        audioDistortionField.Play();

        foreach (GameObject floatObj in floatingObjs)
        {
            floatObj.SetActive(true);
        }
    }

    public void HideParallelDimension()
    {
        audioDistortionField.Play();

        vaseAnimator.SetBool("disappear", true);
        floatingObjs[0].SetActive(false);
    }

    public void SendisFinished()
    {
        Debug.Log("isFinished is sent");
        EventManager.TriggerEvent("disabledDistoredVision", true);
    }
    public void SendendZone()
    {
        Debug.Log("EndZone is sent");
        EventManager.TriggerEvent("endZone", true);
    }
}
