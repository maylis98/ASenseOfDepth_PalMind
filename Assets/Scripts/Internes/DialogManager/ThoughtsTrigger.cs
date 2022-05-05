using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtsTrigger : MonoBehaviour
{
    public Dialog[] dialogues;
    //int indexOfDialogue
    public void TriggerThoughts(string nameOfDialogue)
    {
        foreach (Dialog dialogue in dialogues)
        {
            if (dialogue.name == nameOfDialogue)
            {
                FindObjectOfType<ThoughtsManager>().StartThoughts(dialogue);
            }
        }
        //FindObjectOfType<ThoughtsManager>().StartThoughts(dialogues[indexOfDialogue]);
    }

}
