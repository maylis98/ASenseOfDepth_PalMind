using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionController : MonoBehaviour
{
    public GameObject introButtons;
    public GameObject mainButtons;
    public GameObject closingSentence;

    public TextFilled closingText;

    private void Start()
    {
        introButtons.SetActive(true);
        mainButtons.SetActive(false);
        closingSentence.SetActive(false);
    }
    public void ShowControls()
    {
        mainButtons.SetActive(true);
        //FindObjectOfType<DialogManager>().StartDialogue(dialogue);
        //FindObjectOfType<TextManager>().StartClosingTexts(closingText);
    }

    public void DialogControls()
    {
       /* DisplayNextSentence();
        if (sentences.Count == 0)
        {
            endDialogue();
            return;
        }*/
    }

    private void endDialogue()
    {
        introButtons.SetActive(false);
        closingSentence.SetActive(true);
    }
}
