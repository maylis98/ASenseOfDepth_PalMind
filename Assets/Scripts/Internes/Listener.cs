using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Listener : MonoBehaviour
{
    private PlayableDirector timeline;
    bool isPlayed = false;
    // Start is called before the first frame update
    void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
        EventManager.StartListening("StartTimeline", StartTimeline);
    }

    private void StartTimeline(object data)
    {
        Debug.Log($"{data}");

        if (isPlayed = (bool)data)
        {
            Debug.Log("timeline is played");
            timeline.Play();
        }
    }
}
