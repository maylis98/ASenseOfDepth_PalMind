using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThoughtsManager : MonoBehaviour
{
    public TextMeshProUGUI thoughtsText;
    public Animator thoughtsBoxAnimator;
    public GameObject continueButton;

    private Queue<string> sentences;

    void Start()
    {
        continueButton.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartThoughts(Dialog dialogue)
    {
        thoughtsBoxAnimator.SetBool("blink", false);
        thoughtsBoxAnimator.SetBool("appear", true);
        continueButton.SetActive(true);
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextThoughts();
    }

    public void DisplayNextThoughts()
    {
        if (sentences.Count == 0)
        {
            EndThoughts();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
      
    }

    IEnumerator TypeSentence (string sentence)
    {
        thoughtsText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            thoughtsText.text += letter;
            yield return null;
        }
    }

   private void EndThoughts()
   {
        sentences.Clear();
        continueButton.SetActive(false);
        thoughtsBoxAnimator.SetBool("appear", false);

        Debug.Log("End of conversation");      
   }


}
