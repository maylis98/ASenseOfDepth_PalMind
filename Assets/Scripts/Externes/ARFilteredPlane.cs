using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class ARFilteredPlane : MonoBehaviour
{
    public GameObject[] objectsToPlace;
    /*public GameObject zoneToPlace;*/
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI floorText;
    //public Vector3 offsetFromCamera;

    public UnityEvent whenFloorIsFound;
         
    private ARPlaneManager arPlaneManager;

    private List<ARPlane> arPlanes;
    private ARPlane lowestPlane;
    private float countDown = 6;

    private void OnEnable()
    {
        arPlanes = new List<ARPlane>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
        arPlaneManager.enabled = true;

        arPlaneManager.planesChanged += OnPlanesChanged;
        floorText.text = "";
    }

     private void OnDisable()
     { 
         arPlaneManager.planesChanged -= OnPlanesChanged;
     }

    private void Update()
    {
        if(countDown > 0)
        {
            countDown -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(countDown % 60);
            //Debug.Log(countDown);
            countDownText.text = string.Format("{0}", seconds);
            floorText.text = "Looking for the floor... \n \n LOOK AT YOUR FEET";

            if (countDown < 0 && lowestPlane != null)
            {
                countDownText.text = "";
                foreach (GameObject obj in objectsToPlace)
                {
                    obj.transform.position = new Vector3(0, lowestPlane.transform.position.y, 0);
                }
                //zoneToPlace.transform.position = new Vector3(0, lowestPlane.transform.position.y, 0);
                whenFloorIsFound.Invoke();
                StartCoroutine(floorFound());

            }
            else if(countDown < 0 && lowestPlane == null)
            {
                foreach (GameObject obj in objectsToPlace)
                {
                    obj.transform.position = new Vector3(0, -1, 0);
                }
                //zoneToPlace.transform.position = new Vector3(0, -1, 0);
                countDownText.text = "";
                floorText.text = "";

                //To delete 
                whenFloorIsFound.Invoke();

                //& Replace by ... when build
                //countDown = 6;
            }
        }
    }

    private void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null && args.added.Count > 0)
        {
            //arPlanes.AddRange(args.added);

            foreach (ARPlane plane in args.added.Where(plane => plane.extents.x * plane.extents.y >= 0.2f))
            {
                if(lowestPlane == null || plane.transform.position.y < lowestPlane.transform.position.y)
                {
                    lowestPlane = plane;
                    floorText.text = "ar Plane y is" + lowestPlane.transform.position.y;
                    countDown = 6;
                }


            }

        }

    }

    IEnumerator floorFound()
    {
        floorText.text = "Floor found";

        yield return new WaitForSeconds(3);

        floorText.text = "";
        arPlaneManager.planePrefab.GetComponent<MeshRenderer>().enabled = false;
        arPlaneManager.enabled = false;

        foreach (ARPlane plane in arPlanes)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
