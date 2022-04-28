using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ThoughtsManager : MonoBehaviour
{
    public UnityEvent ifWalkButtonIsInactive;

    public TextMeshProUGUI thoughtsText;
    public Animator thoughtsBoxAnimator;
    public AudioSource thoughtsNotif;
    public AudioSource thoughtsBreath;
    public GameObject continueButton;
    public GameObject palPresence;
    public GameObject walkButton;

    private Queue<string> sentences;
    private bool isDone = false;

    void Start()
    {
        continueButton.SetActive(false);
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (continueButton.activeSelf)
        {
            walkButton.SetActive(false);

            if (!isDone)
            {
                ifWalkButtonIsInactive.Invoke();
                isDone = true;
            }
            
        }
        else
        {
            walkButton.SetActive(true);
            isDone = false;
        }
    }

    public void StartThoughts(Dialog dialogue)
    {
        thoughtsBoxAnimator.SetBool("blink", false);
        thoughtsBoxAnimator.SetBool("appear", true);
        thoughtsNotif.Play();
        thoughtsBreath.Play();
        continueButton.SetActive(true);
        palPresence.SetActive(true);
        FindObjectOfType<PalPresenceManager>().startParticules();

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
        FindObjectOfType<PalPresenceManager>().stopParticules();
        thoughtsBoxAnimator.SetBool("appear", false);
        continueButton.SetActive(false);
        thoughtsBreath.Stop();

        Debug.Log("End of conversation");      
   }


}
