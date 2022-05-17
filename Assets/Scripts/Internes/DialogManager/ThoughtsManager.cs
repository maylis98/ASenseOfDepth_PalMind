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
    public float durationVolume;

    private Queue<string> sentences;
    private bool isDone = false;
    private bool memoryAppear = false;

    private float currentVolume;
    private float highVolume = 1f;
    private float lowVolume = 0f;

    void Start()
    {
        continueButton.SetActive(false);
        sentences = new Queue<string>();
        EventManager.StartListening("clearCanvas", disableWalkButton);
    }

    private void Update()
    {
        if (continueButton.activeSelf == true)
        {
            walkButton.SetActive(false);

            if (!isDone)
            {
                ifWalkButtonIsInactive.Invoke();
                isDone = true;
            }

        }
        else if (continueButton.activeSelf == false && memoryAppear == false)
        {
            walkButton.SetActive(true);
            isDone = false;
        }
        else
        {
            walkButton.SetActive(false);
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

        if(dialogue.name == "end Screen")
        {
            memoryAppear = true;
        }
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
        StartCoroutine(LerpVolume(highVolume, durationVolume));

    }

    IEnumerator TypeSentence(string sentence)
    {
        thoughtsText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            thoughtsText.text += letter;
            yield return null;
        }
    }

    public void EndThoughts()
    {
        sentences.Clear();
        FindObjectOfType<PalPresenceManager>().stopParticules();
        FindObjectOfType<VFXBugsManager>().BugsDisappear();
        StartCoroutine(LerpVolume(lowVolume, durationVolume));
        thoughtsBoxAnimator.SetBool("appear", false);
        continueButton.SetActive(false);

        Debug.Log("End of conversation");
    }

    private void disableWalkButton(object data)
    {
        if (memoryAppear = (bool)data && continueButton.activeSelf == false)
        {
            walkButton.SetActive(false);
        }
        
    }

    public void enableWalkButton(bool memoryIsEnd)
    {
        if (memoryIsEnd == true)
        {
            memoryAppear = false;
        }
        else
        {
            return;
        }
    }

    IEnumerator LerpVolume(float endVolume, float durationFade)
    {
        float time = 0;
        float startValue = currentVolume;
        while (time < durationFade)
        {
            currentVolume = Mathf.Lerp(startValue, endVolume, time / durationFade);
            time += Time.deltaTime;

            thoughtsBreath.volume = currentVolume;
            yield return null;  
        }

        if (currentVolume < 0.1)
        {
            thoughtsBreath.Stop();
        }

    }




}
