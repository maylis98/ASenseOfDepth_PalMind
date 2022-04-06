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
    private float expansion = 0;

    [SerializeField, Range(0, 7)]
    private float spread = 0;

    [SerializeField, Range(0, 4)]
    private float fluxIntensity = 0;

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
    private int current = 0;
    private float rotSpeed;
    public float speed;
    float WPradius = 1;


    private void Awake()
    {
        sound.Play();

        vfx.SetFloat("Lifetime Expansion", expansion);
        vfx.SetFloat("Spread", spread);
        vfx.SetFloat("Flux Intensity", fluxIntensity);

        StartCoroutine(sphereExpand());

        wayPointsParent = GameObject.FindGameObjectsWithTag("wayPointsParent");
        wayPoints = wayPointsParent[0].GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (timeToMove)
        {
            updateWithConditions.Invoke();
        }
        
    }

    public void changePos()
    {
        if (timeToMove)
        {
        InvokeRepeating("moveToPoints", 0, 2);
        }
    }

    IEnumerator sphereExpand()
    {
        yield return new WaitForSeconds(6);

        //Change VFX values
        vfx.SetFloat("Lifetime Expansion", 0.55f);
        vfx.SetFloat("Spread", 0f);
        vfx.SetFloat("Flux Intensity", 2.67f);

        yield return new WaitForSeconds(3);
        timeToMove = true;

    }

    private void moveToPoints()
    {
            current = Random.Range(0, wayPoints.Length);
        Debug.Log(current);

            if(current >= wayPoints.Length)
            {
                current = 0;
            }
        
            transform.position = wayPoints[current].transform.position;
        
        //transform.position = Vector3.MoveTowards(transform.position, wayPoints[current].transform.position, Time.deltaTime * speed);

    }

    public void scaleOverTime()
    {
        Vector3 vec = new Vector3(Mathf.Sin(Time.time * speedOfPulse), Mathf.Sin(Time.time * speedOfPulse), Mathf.Sin(Time.time * speedOfPulse));

        transform.localScale = vec;
    }


}
