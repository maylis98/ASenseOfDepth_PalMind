using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainControls;
    public TextMeshProUGUI instructionsBox;
    private TextMeshProUGUI endTextBox;
    

    private bool memoryAppear;
    private Animator endTextAnimator;
    private AudioSource endTextAudio;

    void Start()
    {
        memoryAppear = false;
        MainControls.SetActive(false);

        EventManager.StartListening("returnGame", returnGameControls);
    }


    private void returnGameControls(object data)
    {
        if (memoryAppear = (bool)data)
        {
            Debug.Log("return to game received");
            FindObjectOfType<ThoughtsManager>().enableWalkButton(true);
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
