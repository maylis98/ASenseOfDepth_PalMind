using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteColorManager : MonoBehaviour
{
    [SerializeField]
    private Color colorToChange;
    [SerializeField]
    private Color targetColor;
    public float transitionTime;
   
    private SpriteRenderer zoneSprite;
    private void Start()
    {
        zoneSprite = GetComponent<SpriteRenderer>();
        colorToChange.a = 0;
        zoneSprite.color = colorToChange;
        

    }

    public void showZone()
    {
        colorToChange.a = 255;
        zoneSprite.color = colorToChange;
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
            zoneSprite.color = colorToChange;
            yield return null;
        }
        colorToChange = endColor;

    }
}


