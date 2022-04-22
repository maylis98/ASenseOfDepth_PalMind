using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMaterialManager : MonoBehaviour
{
    [SerializeField]
    private Color colorToChange;
    [SerializeField]
    private Color targetColor;
    public float transitionTime;
   
    private Material objMaterial;
    private void Start()
    {
        objMaterial = GetComponent<Renderer>().sharedMaterial;
        objMaterial.color = colorToChange;

    }

    public void changeColor()
    {
        StartCoroutine(LerpFunction(targetColor, transitionTime));
    }

    IEnumerator LerpFunction(Color endColor, float duration)
    {
        float time = 0;
        Color startValue = colorToChange;
        while (time < duration)
        {
            colorToChange = Color.Lerp(colorToChange, targetColor, time / duration);
            time += Time.deltaTime;
            objMaterial.color = colorToChange;
            yield return null;
        }
        colorToChange = endColor;

    }
}


