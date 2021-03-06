using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;

public class SphereExpansion : MonoBehaviour
{
    //Sounds
    public AudioSource sound;
    //VFX
    public VisualEffect vfx;

    //VFX properties
    [SerializeField, Range(0, 6)]
    private float expansion;

    [SerializeField, Range(0, 7)]
    private float spread;

    [SerializeField, Range(0, 4)]
    private float fluxIntensity;

    public Vector3 resultingPosition;
    private GameObject[]wayPointsParent;
    private Transform[] wayPoints;

    [SerializeField]
    private UnityEvent updateWithConditions;

    //forScalingOverTime
    public float speedOfPulse;

    //delay between change of Pos
    public float delay = 10;

    //For Waypoint function
    private bool timeToMove = false;
    private int current;
    private int lastPos;

    //For movingSphere to Zone
    private GameObject[] zonesInScene;
    [SerializeField] 
    private Vector3 offsetFromObj;

    //Text blink time
    public float waitTime;

    private bool deleteZone = false;
    private bool moveTowards;
    private bool appearFromScreen;
    private float speed = 0.5f;
    private float degreesPerSecond = 30;


    private void Awake()
    {
        sound.Play();

        vfx.SetFloat("Lifetime Expansion", expansion);
        vfx.SetFloat("Spread", spread);
        vfx.SetFloat("Flux Intensity", fluxIntensity);

        StartCoroutine(sphereExpand());

        wayPointsParent = GameObject.FindGameObjectsWithTag("wayPointsParent");
        //wayPoints = wayPointsParent[0].GetComponentsInChildren<Transform>();

        deleteZone = false;
        moveTowards = false;
        appearFromScreen = true;
        EventManager.StartListening("endZone", endZone);

        resultingPosition = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        if (timeToMove)
        {
            updateWithConditions.Invoke();
        }

        if (appearFromScreen == true)
        {
            transform.position = Vector3.Lerp(transform.position, resultingPosition, 1f * Time.deltaTime);
        }

        if (moveTowards == true)
        {
            transform.position = Vector3.Lerp(transform.position, zonesInScene[0].transform.position + offsetFromObj, speed * Time.deltaTime);

            if(transform.position == zonesInScene[0].transform.position + offsetFromObj)
            {
                moveTowards = false;
            }
        }

    }

    public void changePos(bool isActivate)
    {
        if (isActivate)
        {
            InvokeRepeating("moveToPoints", 1, delay);
        }
        else
        {
            CancelInvoke("moveToPoints");
            FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("");
            return;
        }
        
    }

    IEnumerator sphereExpand()
    {
        yield return new WaitForSeconds(10);

        
        //Change VFX values
        vfx.SetFloat("Lifetime Expansion", 0.55f);
        vfx.SetFloat("Spread", 0f);
        vfx.SetFloat("Flux Intensity", 2.67f);

        yield return new WaitForSeconds(3);

        /*sound.Play();
        changePos(true);
        timeToMove = true;

        yield return new WaitForSeconds(20);

        changePos(false);*/
        //timeToMove = false;
        appearFromScreen = false;
        FindObjectOfType<CanvasManager>().sentenceInInstructionsBox("A memory has been opened");
        sphereToZone();

    }

    private void moveToPoints()
    {
        wayPoints = wayPointsParent[0].GetComponentsInChildren<Transform>();
        current = Random.Range(0, wayPoints.Length);

        if(current >= wayPoints.Length)
        {
            current = 0;
        }

        if (current == lastPos)
        {
            current = Random.Range(0, wayPoints.Length);
        }
        lastPos = current;

        transform.position = wayPoints[current].transform.position;
        sound.Play();

    }

    private void sphereToZone()
    {
        //Place sphere above zone
        zonesInScene = GameObject.FindGameObjectsWithTag("Zone");
        moveTowards = true;
        //transform.position = zonesInScene[0].transform.position + offsetFromObj;

        //Make zone appear
        FindObjectOfType<ZoneManager>().showZone();
        FindObjectOfType<RotatingTextOnCircle>().InvokeRepeating("Blink", 0, waitTime);
        sound.Play();
    }

    public void scaleOverTime()
    {
        Vector3 vec = new Vector3(Mathf.Sin(Time.time * speedOfPulse), Mathf.Sin(Time.time * speedOfPulse), Mathf.Sin(Time.time * speedOfPulse));
        transform.localScale = vec;
    }


    private void endZone(object data)
    {
        deleteZone = true;
        if (deleteZone == (bool)data)
        {
            this.gameObject.SetActive(false);
        }
    }


}
