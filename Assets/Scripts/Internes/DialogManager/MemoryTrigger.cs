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
    public GameObject bodyPart;

    //public Animator vaseAnimator;

    private AudioSource audioDistortionField;


    private void Start()
    {
        audioDistortionField = this.gameObject.GetComponent<AudioSource>();
        bodyPart.SetActive(false);

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
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("Follow the light");
    }

    public void ShowParallelDimension()
    {
        audioDistortionField.Play();

        FindObjectOfType<SoundManager>().enterGate();

        foreach (GameObject floatObj in floatingObjs)
        {
            floatObj.SetActive(true);
        }
    }

    public void HideParallelDimension()
    {
        audioDistortionField.Play();

        //vaseAnimator.SetBool("disappear", true);
        floatingObjs[0].SetActive(false);
    }

    public void SendisFinished()
    {
        EventManager.TriggerEvent("disabledDistoredVision", true);
    }
    public void SendendZone()
    {
        EventManager.TriggerEvent("endZone", true);
    }

}
