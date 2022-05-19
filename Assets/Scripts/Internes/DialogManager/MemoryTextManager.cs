using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryTextManager : MonoBehaviour
{
    public GameObject textBoxObj;
    public GameObject[] wayPointsParent;
    public GameObject centerButton;


    private TextMeshPro textBox;
    private AudioSource textAudio;
    private Transform[] wayPoints;
    private int currentPos;
    private int lastPos;
    private int memoryLength;
    private CanvasGroup alphaButton;

    private Queue<string> sentences;

    void Start()
    {
        textBox = textBoxObj.GetComponent<TextMeshPro>();
        textAudio = textBoxObj.GetComponent<AudioSource>();
        wayPoints = wayPointsParent[0].GetComponentsInChildren<Transform>();
        alphaButton = centerButton.GetComponent<CanvasGroup>();

        sentences = new Queue<string>();
        alphaButton.alpha = 0f;
    }

    public void StartMemory(Memory memory)
    {
        sentences.Clear();

        foreach (string sentence in memory.sentences)
        {
            sentences.Enqueue(sentence);
            memoryLength = memory.sentences.Length;
        }

        FindObjectOfType<ProgressBar>().GetCurrentFill(sentences.Count, 0, memoryLength);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndMemory();
            return;
        }

        if(sentences.Count > 5 )
        {
            FindObjectOfType<SoundManager>().discoverText();
        }

        if (sentences.Count < 5)
        {
            FindObjectOfType<SoundManager>().morecalmAfterText();
        }


        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");

        FindObjectOfType<ProgressBar>().GetCurrentFill(sentences.Count, 0, memoryLength);

        moveToPoints();

        string sentence = sentences.Dequeue();
        alphaButton.alpha = 0f;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        textBox.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textBox.text += letter;
            alphaButton.alpha = 1f;
            yield return null;
        }
    }

   void EndMemory()
    {
        alphaButton.alpha = 0f;
        FindObjectOfType<ProgressBar>().GetCurrentFill(0, 0, memoryLength);
        //Clear text
        sentences.Clear();
        textBox.text = "";
        FindObjectOfType<ThoughtsTrigger>().TriggerThoughts("Memory plant");
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("NOW THE VISION IS CLEAR");
        FindObjectOfType<SoundManager>().calmDownAfterText();
        Debug.Log("End of conversation");


        //VFX State to "Unified"
        FindObjectOfType<VFXMemoryManager>().UnifiedMemory();

    }

    private void moveToPoints()
    {
        currentPos = Random.Range(0, wayPoints.Length);

        if (currentPos >= wayPoints.Length)
        {
            currentPos = 0;
        }

        if (currentPos == lastPos)
        {
            currentPos = Random.Range(0, wayPoints.Length);
        }
        lastPos = currentPos;

        textBoxObj.transform.position = wayPoints[currentPos].transform.position;
        textBoxObj.transform.rotation = wayPoints[currentPos].transform.rotation;

        textAudio.Play();

    }


}
