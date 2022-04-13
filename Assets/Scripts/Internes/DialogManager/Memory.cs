using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Memory
{
    public string memoryNumber;

    [TextArea(5,10)]
    public string[] sentences;

}
