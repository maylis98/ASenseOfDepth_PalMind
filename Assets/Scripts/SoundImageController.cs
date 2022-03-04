using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SoundImageController : MonoBehaviour
{

    public AudioClip[] aClips;
    public AudioSource myAudioSource;
    string ObjectToTouch;

    public GameObject videoPlayer;
    public PlayableDirector timeline;

    public int timeToStop;


    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        timeline = GetComponent<PlayableDirector>();
        videoPlayer.SetActive(false);

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
                    case "fragment":
                        myAudioSource.clip = aClips[0];
                        myAudioSource.Play();
                        /*videoPlayer.SetActive(true);*/
                        timeline.Play();
                        Destroy(videoPlayer, timeToStop);
                        break;

                    case "video":
                        myAudioSource.clip = aClips[1];
                        myAudioSource.Play();
                        break;

                    /*case "Cube2":
                        myAudioSource.clip = aClips[2];
                        myAudioSource.Play();
                        break;*/
                    default:
                        break;

                }

            }
        }
    }
}