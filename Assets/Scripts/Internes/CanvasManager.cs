using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainControls;
    public GameObject openCapsuleButton; 

    public GameObject whiteBckgrd;
    public TextMeshProUGUI instructionsBox;

    void Start()
    {
        MainControls.SetActive(false);
        whiteBckgrd.SetActive(false);
        openCapsuleButton.SetActive(false);
        EventManager.StartListening("returnGame", returnGameControls);
    }

    private void returnGameControls(object data)
    {
        if ((bool)data == true)
        {
            Debug.Log("return to game received " + (bool)data);
            FindObjectOfType<ThoughtsManager>().enableWalkButton(true);
        }
    }

    public void sentenceInInstructionsBox(string instructions)
    {
        instructionsBox.text = instructions;
    }

    public void showWhiteBckgrd()
    {
        whiteBckgrd.SetActive(true);
    }

    public void showCapsuleButton(int indexB)
    {
        openCapsuleButton.SetActive(true);
        EventManager.TriggerEvent("indexNumber", indexB);
    }



}
