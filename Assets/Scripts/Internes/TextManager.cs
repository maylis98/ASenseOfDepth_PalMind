using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI closingText;

    public TextFilled texts;

    private Queue<string> closingSentences;

    private void Start()
    {
        closingSentences = new Queue<string>();
    }

    public void StartClosingTexts(TextFilled closingText)
    {
        closingSentences.Clear();

        foreach(string closingSentence in closingText.closingSentences)
        {
            closingSentences.Enqueue(closingSentence);
        }

        DisplayNextClosingText();
    }

    public void DisplayNextClosingText()
    {
        if (closingSentences.Count == 0)
        {
            endTexts();
            return;
        }

        string closingSentence = closingSentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(closingSentence));
    }

    IEnumerator TypeSentence(string closingSentence)
    {
        closingText.text = "";

        foreach (char letter in closingSentence.ToCharArray())
        {
            closingText.text += letter;
            yield return null;
        }
    }

    void endTexts()
    {
        closingText.enabled = false;
        //load next scene;
    }
}
