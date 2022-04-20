using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtsTrigger : MonoBehaviour
{
    public Dialog[] dialogues;

    public void TriggerThoughts(int indexOfDialogue)
    {
        FindObjectOfType<ThoughtsManager>().StartThoughts(dialogues[indexOfDialogue]);
    }

}
