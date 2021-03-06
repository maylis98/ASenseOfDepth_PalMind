using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMemory : MonoBehaviour
{
    private bool appear;
    private bool disappear;
    private GameObject objectToScale;
    private Vector3 scaleTo;

    private BoxCollider armCollider;

    //public GameObject goalPos;
    public float speedToScreen;
    public float seconds = 1f;
    private void Start()
    {
        appear = true;
        disappear = false;

        objectToScale = this.gameObject;
        scaleTo = new Vector3(0, 0, 0);

        armCollider = this.gameObject.GetComponent<BoxCollider>();
        armCollider.enabled = false;


    }

    void Update()
    {
        if (appear)
        {
            this.transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        }

        if (disappear)
        {
            appear = false;
            this.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, Time.deltaTime * speedToScreen);
            StartCoroutine(ScaleTo(objectToScale, scaleTo, seconds));

            StartCoroutine(deleteMemory(seconds));

        }
        
    }

    public void ableToClickOnArm()
    {
        armCollider.enabled = true;
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("Catch this");
    }

    public void returnToScreen()
    {
        disappear = true;
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("This memory is now complete");
    }

    private IEnumerator ScaleTo(GameObject objectToScale, Vector3 scaleTo, float seconds)
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

    private IEnumerator deleteMemory(float seconds)
    {

        yield return new WaitForSeconds(seconds);

        EventManager.TriggerEvent("endOfMemory", true);
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");
        FindObjectOfType<CanvasManager>().showWhiteBckgrd();
        FindObjectOfType<SoundManager>().defaultState();
        Debug.Log("END sent to Game");
    }
}
