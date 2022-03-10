using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(AudioSource))]

public class FloorPlacement : MonoBehaviour
{
    private GameObject spawnNew;

    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;

    [SerializeField]
    public GameObject[] objectArray;
    AudioSource audioClip;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        audioClip = GetComponent<AudioSource>();
    }
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
    }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if(Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true; 
            }

            touchPosition = default;
            return false;
        }

    void Update()
    {
       if(!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes))
            {
                var hitPose = hits[0].pose;

                foreach (var plane in arPlaneManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }

                arPlaneManager.enabled = false;

                spawnNew = Instantiate(objectArray[0], hitPose.position, hitPose.rotation);
                audioClip.Play(0);
            }
        }

    }
}
