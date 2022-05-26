using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public GameObject MainControls;
    public GameObject whiteBckgrd;
    public TextMeshProUGUI instructionsBox;

    void Start()
    {
        MainControls.SetActive(false);

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


}
