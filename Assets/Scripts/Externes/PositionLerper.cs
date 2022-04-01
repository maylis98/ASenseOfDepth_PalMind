using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLerper : MonoBehaviour
{
    public Camera ARCamera;
    public GameObject objectToMove;
    public Vector3 offsetPos;
    public Vector3 fromPos;
    public float seconds = 1f;

    private Vector3 moveTo;
    private Vector3 startingPosition;

    private void Start()
    {
        ChangePosition();
    }

    public void ChangePosition()
    {
        StartCoroutine(MoveOverSeconds(objectToMove, moveTo, seconds));
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 moveTo, float seconds)
    {
        float elapsedTime = 0;
        startingPosition = ARCamera.transform.position + fromPos;
        moveTo = ARCamera.transform.position + offsetPos;

        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPosition, moveTo, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = moveTo;

    }

}
