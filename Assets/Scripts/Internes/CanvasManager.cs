using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainControls;
    public GameObject endText;
    public TextMeshProUGUI instructionsBox;
    private TextMeshProUGUI endTextBox;
    

    private bool memoryAppear;
    private Animator endTextAnimator;
    private AudioSource endTextAudio;

    void Start()
    {
        memoryAppear = false;
        endText.SetActive(false);
        endTextAnimator = endText.GetComponent<Animator>();
        endTextAudio = endText.GetComponent<AudioSource>();
        endTextBox = endText.GetComponent<TextMeshProUGUI>();

        EventManager.StartListening("cleanCanvas", memoryCanvas);
        EventManager.StartListening("returnGame", returnGameControls);
    }

    private void memoryCanvas(object data)
    {
        if(memoryAppear = (bool)data)
        {
            Debug.Log("a memory has appeared");
            FindObjectOfType<NativeWebsocketChat>().SendChatMessage("player static");
            MainControls.SetActive(false);
        }
    }

    private void returnGameControls(object data)
    {
        if (memoryAppear = (bool)data)
        {
            Debug.Log("return to game received");
            MainControls.SetActive(true);
            endText.SetActive(true);
            sentenceInEndText("...I can feel myself again...");
            endTextBox.text = "I can feel myself";
            endTextAudio.Play();
            endTextAnimator.SetBool("disappear", false);
        }
    }

    public void deleteEndText()
    {
        endTextAnimator.SetBool("disappear", true);
        endTextAudio.Play();
    }

    public void sentenceInEndText(string sentence)
    {
        endTextBox.text = sentence;
    }

    public void sentenceInInstructionsBox(string instructions)
    {
        instructionsBox.text = instructions;
    }
   

}
