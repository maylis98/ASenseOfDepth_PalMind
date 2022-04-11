using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTextManager : MonoBehaviour
{
    public TextMesh[] textBoxes;
    public AudioSource[] textAudios;
    
    private int randomNumber;

    //public GameObject startMemory;

    //public Animator textBoxAnimator;
    //public Animator endButton;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartMemory(Memory memory)
    {
        //textBoxAnimator.SetBool("appear", true);
        sentences.Clear();

        foreach (string sentence in memory.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndMemory();
            return;
        }

        //startMemory.SetActive(false);
        string sentence = sentences.Dequeue();
        //StopAllCoroutines();
        TypeSentence(sentence);
        //textBoxes.text = sentence;

    }

    private void TypeSentence(string sentence)
    {

        randomNumber = Random.Range(0, 5);
        Debug.Log(randomNumber);
        textBoxes[randomNumber].text = sentence;
        textAudios[randomNumber].Play();

        /*foreach (char letter in sentence.ToCharArray())
        {
            textBoxes[randomNumber].text += letter;
            yield return null;
        }*/
    }

   void EndMemory()
    {
        //textBoxAnimator.SetBool("appear", false);

        Debug.Log("End of conversation");
        //endButton.SetBool("appear", true);
    }

   /* private void Update()
    {
        randomNumber = Random.Range(0, 10);
        Debug.Log(randomNumber);
    }*/


}
