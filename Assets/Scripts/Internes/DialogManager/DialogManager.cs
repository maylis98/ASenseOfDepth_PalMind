using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
    public UnityEvent whenFirstDialogueIsEnd;
    public UnityEvent whenSecondDialogueIsEnd;

    public TextMeshProUGUI dialogueText;

    public GameObject intro;

    public GameObject startButton;
    public GameObject nextButton;
    public GameObject palPresence;
    public AudioSource palBreath;

    public Animator dialogueBoxAnimator;
    public Animator endButton;

    private Queue<string> sentences;
    private int dialogNumber;

    void Start()
    {
        nextButton.SetActive(false);

        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialog dialogue, int dialogIndex)
    {
        dialogNumber = dialogIndex;

        dialogueBoxAnimator.SetBool("blink", false);
        dialogueBoxAnimator.SetBool("appear", true);
        nextButton.SetActive(true);
        

        if (dialogNumber == 1)
        {
            palPresence.SetActive(true);
            FindObjectOfType<PalPresenceManager>().startParticules();
            palBreath.Play();
        }

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

        startButton.SetActive(false);

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
      
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

        if (dialogNumber == 0)
        {
            whenFirstDialogueIsEnd.Invoke();
        }
        else if (dialogNumber == 1)
        {
            palBreath.Stop();
            FindObjectOfType<PalPresenceManager>().stopParticules();
            whenSecondDialogueIsEnd.Invoke();
            showEndButton();
        }

     
   }

    public void showEndButton()
    {
        intro.SetActive(false);
        dialogueBoxAnimator.SetBool("appear", false);

        endButton.SetBool("appear", true);
    }


}
