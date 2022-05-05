using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryTextManager : MonoBehaviour
{
    public GameObject textBoxObj;
    private TextMeshPro textBox;
    private AudioSource textAudio;

    public GameObject[] wayPointsParent;
    private Transform[] wayPoints;
    private int currentPos;
    private int lastPos;

    private Queue<string> sentences;

    void Start()
    {
        textBox = textBoxObj.GetComponent<TextMeshPro>();
        textAudio = textBoxObj.GetComponent<AudioSource>();
        wayPoints = wayPointsParent[0].GetComponentsInChildren<Transform>();

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

        if(sentences.Count > 5 )
        {
            FindObjectOfType<SoundManager>().discoverText();
        }

        if (sentences.Count < 5)
        {
            FindObjectOfType<SoundManager>().morecalmAfterText();
        }


        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");

        moveToPoints();
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {

        textBox.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            textBox.text += letter;
            yield return null;
        }
    }

   void EndMemory()
    {
        //Clear text
        textBox.text = "";
        //FindObjectOfType<CanvasManager>().sentenceInEndText("Pal's memory is now complete");
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("[ Click on the memory ]");
        FindObjectOfType<SoundManager>().calmDownAfterText();
        Debug.Log("End of conversation");

        //VFX State to "Unified"
        FindObjectOfType<VFXMemoryManager>().UnifiedMemory();


        //Start Pal's body fragment animation

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
