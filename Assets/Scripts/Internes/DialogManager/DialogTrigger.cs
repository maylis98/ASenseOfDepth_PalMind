using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    public Dialog[] dialogues;

    public void TriggerDialogue(int indexOfDialogue)
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogues[indexOfDialogue], indexOfDialogue);

    }


}
