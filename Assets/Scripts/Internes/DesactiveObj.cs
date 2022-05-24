using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveObj : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(true);
    }
    public void Disactivate()
    {
        this.gameObject.SetActive(false);
    }

}
