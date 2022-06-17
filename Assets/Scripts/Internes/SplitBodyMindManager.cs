using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBodyMindManager : MonoBehaviour
{
    public GameObject splitBody;
    public GameObject veil;
    public GameObject palOrb;
    public GameObject fadeCanvas;
    public GameObject title;
    public GameObject instructions;
    public Material veilMat;

    public Color initColor;
    public Color visibleColor;
    private Color currentColor;

    public float degreesPerSecond = 4.0f;
    public float amplitude = 0.1f;
    public float frequency = 0.5f;
    public Vector3 targetScale;

    private Animator splitBodyA;
    private Animator fadeCanvasA;
    private Animator titleA;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    private bool floatingBody = false;
    private bool walkingBody = false;
    void Awake()
    {
        currentColor = initColor;
        veilMat.color = currentColor;
        veil.SetActive(false);
        floatingBody = true;
        palOrb.SetActive(false);
        instructions.SetActive(false);
        splitBodyA = splitBody.GetComponent<Animator>();
        fadeCanvasA = fadeCanvas.GetComponent<Animator>();
        fadeCanvasA.gameObject.SetActive(false);
        titleA = title.GetComponent<Animator>();
    }

    public void startSplitBody()
    {
        titleA.SetBool("fadeOut", true);
        StartCoroutine(splitBodyMind());
    }
    void Update()
    {
        if (floatingBody == true)
        {
            splitBody.transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

            posOffset = Camera.main.transform.position + Camera.main.transform.forward * (3);
            tempPos = Vector3.Lerp(splitBody.transform.position, posOffset, 1f * Time.deltaTime);
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            splitBody.transform.position = tempPos;
        }

        if (walkingBody == true)
        {
            splitBody.transform.position = Vector3.MoveTowards(splitBody.transform.position, Camera.main.transform.position + Camera.main.transform.forward * (10) + Camera.main.transform.up * (2), Time.deltaTime * 0.5f);
            splitBody.transform.rotation = Quaternion.Lerp(splitBody.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 1f);
            splitBody.transform.localScale = Vector3.Lerp(splitBody.transform.localScale, targetScale, Time.deltaTime * 0.1f);
            veil.transform.localScale = Vector3.Lerp(veil.transform.localScale, targetScale, Time.deltaTime * 0.5f);
            currentColor = Color.Lerp(currentColor, visibleColor, Time.deltaTime * 0.3f);
            veilMat.color = currentColor;

        }

    }

    IEnumerator splitBodyMind()
    {
        palOrb.SetActive(true);

        yield return new WaitForSeconds(2);

        floatingBody = false;
        splitBodyA.SetBool("standing", true);

        yield return new WaitForSeconds(6);

        walkingBody = true;
        veil.SetActive(true);

        yield return new WaitForSeconds(2);

        instructions.SetActive(true);

        yield return new WaitForSeconds(3);

        fadeCanvas.gameObject.SetActive(true);
        fadeCanvasA.SetBool("toBlack", true);
        FindObjectOfType<NativeWebsocketChat>().SendChatMessage("pal is here");
    }


}
