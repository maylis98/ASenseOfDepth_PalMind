using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLerper : MonoBehaviour
{
    public GameObject objectToScale;
    public Vector3 scaleTo;
    public float seconds = 1f;

    private void Start()
    {
        ChangeScale();
    }

    public void ChangeScale()
    {
        StartCoroutine(ScaleOverSeconds(objectToScale, scaleTo, seconds));
    }

    public IEnumerator ScaleOverSeconds(GameObject objectToScale, Vector3 scaleTo, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingScale = objectToScale.transform.localScale;
        while (elapsedTime < seconds)
        {
            objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToScale.transform.localScale = scaleTo;
    }
}
