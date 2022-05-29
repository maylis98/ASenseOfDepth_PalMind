using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capsuleButtonManager : MonoBehaviour
{
    private Animator buttonA;
    private int indexButton;

    private void Awake()
    {
        buttonA = GetComponent<Animator>();
        EventManager.StartListening("indexNumber", storedValue);
    }

    public void storedValue(object data)
    {
        indexButton = (int)data;
    }

    public void onceClicked()
    {
        if (indexButton == 1)
        {
            FindObjectOfType<NativeWebsocketChat>().SendChatMessage("open capsule");
        }
        else if (indexButton == 2)
        {
            FindObjectOfType<NativeWebsocketChat>().SendChatMessage("open capsule 2");
        }

        StartCoroutine(hideAfterSeconds());
        buttonA.SetBool("isClicked", true);
    }

    IEnumerator hideAfterSeconds()
    {
        yield return new WaitForSeconds(2);

        this.gameObject.SetActive(false);
        buttonA.SetBool("isClicked", true);
    
    }

}
