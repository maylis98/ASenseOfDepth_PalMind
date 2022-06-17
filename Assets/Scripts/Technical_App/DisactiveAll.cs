using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisactiveAll : MonoBehaviour
{
    public GameObject[] objectToDisable;
    public GameObject TextMenu;

    private void Start()
    {
        foreach (GameObject obj in objectToDisable)
        {
            obj.SetActive(false);
        }

        TextMenu.SetActive(false);
        this.gameObject.SetActive(false);
    }
    public void DisableObjs()
    {
        foreach(GameObject obj in objectToDisable)
        {
            obj.SetActive(false);
        }
    }
}
