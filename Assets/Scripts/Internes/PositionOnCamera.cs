using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Camera.main.transform.position;
    }

}
