using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TapToActivate : MonoBehaviour
{

    public AudioClip[] aClips;
    public AudioSource myAudioSource;
    string ObjectToTouch;

    public GameObject timelineObj;
    public PlayableDirector timeline;

    public GameObject ObjTouched;
    public Animator ObjTouchedAnimator;


    public int timeToStop;


    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        timelineObj.SetActive(false);

    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                ObjectToTouch = Hit.transform.name;
                switch (ObjectToTouch)
                {
                    case "pal":
                        ObjTouchedAnimator = ObjTouched.GetComponent<Animator>();
                        ObjTouchedAnimator.Play("Blink");

                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();

                        timelineObj.SetActive(true);
                        timeline = timelineObj.GetComponent<PlayableDirector>();
                        timeline.Play();
                        Destroy(timelineObj, timeToStop);
                        break;

                    case "video":
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();
                        break;

                    case "Cube2":
                        myAudioSource.clip = aClips[2];
                        myAudioSource.Play();
                        break;
                    default:
                        break;

                }

            }
        }
    }

    /*public void Play()
    {
        timeline.Play();
    }*/
}