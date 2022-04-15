using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainControls;
    public GameObject TemporaryLauncher;
    public GameObject endText;

    private bool memoryAppear;
    private Animator endTextAnimator;
    private AudioSource endTextAudio;

    void Start()
    {
        memoryAppear = false;
        endText.SetActive(false);
        endTextAnimator = endText.GetComponent<Animator>();
        endTextAudio = endText.GetComponent<AudioSource>();

        EventManager.StartListening("cleanCanvas", memoryCanvas);
        EventManager.StartListening("returnGame", returnGameControls);
    }

    private void memoryCanvas(object data)
    {
        if(memoryAppear = (bool)data)
        {
            Debug.Log("a memory has appeared");
            MainControls.SetActive(false);
            TemporaryLauncher.SetActive(false);
        }
    }

    private void returnGameControls(object data)
    {
        if (memoryAppear = (bool)data)
        {
            Debug.Log("return to game received");
            MainControls.SetActive(true);
            endText.SetActive(true);
            endTextAudio.Play();
            endTextAnimator.SetBool("disappear", false);
        }
    }

    public void deleteEndText()
    {
        endTextAnimator.SetBool("disappear", true);
        endTextAudio.Play();
    }
}
