using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ChangeText : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] sentences;

    public TextMeshPro textBox;

    public UnityEvent showText;

    public GameObject speaker;

    public void ChangeTextTo(int indexOf)
    {
        showText.Invoke();

        textBox.text = sentences[indexOf];
        speaker.SetActive(true);
    }
}
