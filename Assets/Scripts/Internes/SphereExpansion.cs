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

    bool deleteZone;


    private void Awake()
    {
        sound.Play();

        vfx.SetFloat("Lifetime Expansion", expansion);
        vfx.SetFloat("Spread", spread);
        vfx.SetFloat("Flux Intensity", fluxIntensity);

        StartCoroutine(sphereExpand());

        wayPointsParent = GameObject.FindGameObjectsWithTag("wayPointsParent");
        wayPoints = wayPointsParent[0].GetComponentsInChildren<Transform>();

        deleteZone = false;
        EventManager.StartListening("endZone", endZone);
    }

    private void Update()
    {
        if (timeToMove)
        {
            updateWithConditions.Invoke();
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

        changePos(true);
        timeToMove = true;

        yield return new WaitForSeconds(30);

        changePos(false);
        //timeToMove = false;
        sphereToZone();

    }

    private void moveToPoints()
    {
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
        zonesInScene = GameObject.FindGameObjectsWithTag("Zone");
        transform.position = zonesInScene[0].transform.position + offsetFromObj;

        //make Zone appear
        MeshRenderer zoneMesh = zonesInScene[0].GetComponent<MeshRenderer>();
        zoneMesh.enabled = true;
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
        if (deleteZone = (bool)data)
        {
            Debug.Log("deleteSphere has been received");
            this.gameObject.SetActive(false);
        }
    }


}
