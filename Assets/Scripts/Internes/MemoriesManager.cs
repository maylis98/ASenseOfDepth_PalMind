using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoriesManager : MonoBehaviour
{
    public GameObject MemorialSpace;

    public GameObject UncompleteSphere;

    public GameObject FallingCylinders;

    public GameObject WaterFloor;

    [SerializeField]
    private GameObject[] SpheresOfMemory;

    [SerializeField]
    private GameObject[] GatesOfMemory;

    [SerializeField]
    private GameObject PalBody;


    public UnityEvent onEnd;

    public GameObject triggerZone;
    [SerializeField]
    private Vector3 offsetFromObj;
    [SerializeField]
    private Vector3 offsetFromCamera;
    public Vector3 initGateRotation;

    private float countDown;
    private Vector3 frontCameraPos;
    private Vector3 resultingPosition;
    private GameObject spawnedUncompleteS;
    private GameObject spawnedMemorialSpace;
    private GameObject spawnedFallingC;
    private GameObject spawnedSphere;
    private GameObject spawnedGate;
    private int indexOfMemory;
    private bool theEnd;

    private ParticleSystem fallingCParticules;

    private float wait = 2f;
    private float distanceFromCamera = 2;
    private bool rotateAround = false;

    void Awake()
    {
        theEnd = false;

        EventManager.StartListening("UnlockMemory", UnlockMemory);
        EventManager.StartListening("endOfMemory", endThisMemory);
        EventManager.StartListening("clearMemorialSpace", hideMSpace);
        EventManager.StartListening("clearUncompleteS", hideUncompleteS);
    }

    private void Update()
    {
        if (rotateAround == true)
        {
            /*Vector3 objPos = Camera.main.transform.position + Camera.main.transform.up * 4 + Camera.main.transform.forward * 7;
            spawnedFallingC.transform.position = new Vector3(objPos.x, objPos.y, objPos.z);*/

            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
                //float seconds = Mathf.FloorToInt(countDown % 60);

                if (countDown < 0)
                {
                    fallingCParticules.Stop();
                    StartCoroutine(waitBeforeDestroy(wait));
                    rotateAround = false;
                    
                }
            }
        }
    }


    public void showUncompleteS()
    {
        resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * 2 + Camera.main.transform.up * (-10);
        spawnedUncompleteS = Instantiate(UncompleteSphere, resultingPosition, Quaternion.Euler(0, 0, 0));
    }

    public void showMSpace()
    {
        spawnedMemorialSpace = Instantiate(MemorialSpace, triggerZone.transform.position + offsetFromObj, Quaternion.Euler(0, 90, 0));
    }

    public void showWaterFloor()
    {
        spawnedMemorialSpace = Instantiate(WaterFloor, Camera.main.transform.position + Camera.main.transform.up * (-20), Quaternion.Euler(0, 0, 0)) ;
    }

    public void showFallingC()
    {
        countDown = 20;
        spawnedFallingC = Instantiate(FallingCylinders, Camera.main.transform.position + Camera.main.transform.up * 10 + Camera.main.transform.forward * 7, Quaternion.Euler(90, 0, 0));
        fallingCParticules = spawnedFallingC.GetComponent<ParticleSystem>();
        FindObjectOfType<SoundManager>().anxiousWithWater();
        rotateAround = true;
    }

    private void hideMSpace(object data) 
    {
        bool hideSpace = true;
        if (hideSpace == (bool)data)
        {
            Destroy(spawnedMemorialSpace);
        }
    }

    private void hideUncompleteS(object data)
    {
        bool hideUncompleteS = true;
        if (hideUncompleteS == (bool)data)
        {
            Destroy(spawnedUncompleteS);
        }
    }

    private void UnlockMemory(object data)
    {
        Vector3 screenPosition = Camera.main.transform.position + Camera.main.transform.forward * 15;

        FindObjectOfType<NativeWebsocketChat>().SendChatMessage("player static");
        FindObjectOfType<SoundManager>().sphereAppear();
        EventManager.TriggerEvent("clearCanvas", true);
        EventManager.TriggerEvent("showZone", true);

        spawnedSphere = Instantiate(SpheresOfMemory[(int)data], screenPosition,Quaternion.identity);
        spawnedGate = Instantiate(GatesOfMemory[(int)data], triggerZone.transform.position + offsetFromObj, Quaternion.Euler(0, 90, 0));


    }

    private void endThisMemory(object data)
    {
        if(theEnd = (bool)data)
        {
            Destroy(spawnedSphere);
            Destroy(spawnedGate);
            //FindObjectOfType<CanvasManager>().deleteEndText();
            onEnd.Invoke();
        }
    }
    public void showPalBody()
    {
        resultingPosition = Camera.main.transform.position + Camera.main.transform.up * (-1) + Camera.main.transform.forward * distanceFromCamera;
        spawnedGate = Instantiate(PalBody, resultingPosition, Quaternion.Euler(0, 0, 0));
        FindObjectOfType<PalBodyManager>().showBody();
        FindObjectOfType<WaterPPManager>().PPPresence(1);
    }

    IEnumerator waitBeforeDestroy(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);

        Destroy(spawnedFallingC);
    }
       
}
