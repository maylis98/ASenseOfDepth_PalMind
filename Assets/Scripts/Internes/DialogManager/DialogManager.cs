using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public GameObject intro;

    public GameObject startGame;

    public Animator dialogueBoxAnimator;
    public Animator endButton;

    private Queue<string> sentences;
    private bool introIsEnd;

    void Start()
    {
        introIsEnd = false;
        sentences = new Queue<string>();
        EventManager.StartListening("show End Button", showEndButton);
    }

    public void StartDialogue(Dialog dialogue)
    {
        dialogueBoxAnimator.SetBool("blink", false);
        dialogueBoxAnimator.SetBool("appear", true);
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        startGame.SetActive(false);

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
      
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

   private void EndDialogue()
   {
        dialogueBoxAnimator.SetBool("blink", true);
        dialogueText.text = "LISTEN";
        Debug.Log("End of conversation");
            
   }

    public void showEndButton(object data)
    {
        if (introIsEnd = (bool)data)
        {
            intro.SetActive(false);
            dialogueBoxAnimator.SetBool("appear", false);

            endButton.SetBool("appear", true);
        }
    }


}
