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
    public GameObject zoneToPlace;
    public GameObject palPresence;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI floorText;
    //public Vector3 offsetFromCamera;

    public UnityEvent whenFoorIsFound;
         
    private ARPlaneManager arPlaneManager;

    private List<ARPlane> arPlanes;
    private ARPlane lowestPlane;
    private float countDown = 5;

    private void OnEnable()
    {
        arPlanes = new List<ARPlane>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();

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
                floorText.text = "";
                zoneToPlace.transform.position = new Vector3(Camera.main.transform.position.x, lowestPlane.transform.position.y, Camera.main.transform.position.z);
                palPresence.transform.position = new Vector3(Camera.main.transform.position.x, lowestPlane.transform.position.y, Camera.main.transform.position.z);
                whenFoorIsFound.Invoke();


            }
            else if(countDown < 0 && lowestPlane == null)
            {
                zoneToPlace.transform.position = new Vector3(Camera.main.transform.position.x, -1, Camera.main.transform.position.z);
                palPresence.transform.position = new Vector3(Camera.main.transform.position.x, -1, Camera.main.transform.position.z);
                countDownText.text = "";
                floorText.text = "";

                //To delete 
                whenFoorIsFound.Invoke();

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
               /* if (plane.transform.position.y < -0.6)
                {
                    //plane.alignment.IsHorizontal() &&
                    ARPlane arPlane = args.added[0];
                    debugText.text = "ar Plane y is" + arPlane.transform.position.y;
                    //zoneToPlace.transform.position = arPlane.transform.position;
                    //FindObjectOfType<PositionZoneOnMainPlane>().placeOnCenterOfPlane(arPlane);
                    whenHorizontalSmallPlaneIsFound.Invoke();
                }
                else
                {
                    ARPlane arPlane = args.removed[0];
                    debugText.text = "the plane is under -0.6";
                    return;
                }*/

            }

        }

        




    }
}
