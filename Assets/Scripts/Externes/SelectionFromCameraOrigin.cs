using UnityEngine;
using UnityEngine.UI;

public class SelectionFromCameraOrigin : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private PlacementObj[] placedObjects;

    [SerializeField]
    private Color activeColor = Color.red;

    [SerializeField]
    private Color inactiveColor = Color.gray;

    [SerializeField]
    private Button dismissButton;

    [SerializeField]
    private Camera arCamera;

    /*private Vector2 touchPosition = default;*/

    [SerializeField]
    private bool displayOverlay = false;

    [SerializeField]
    private float rayDistanceFromCamera = 10.0f;

    [SerializeField]
    private float generateRayAfterSeconds = 2.0f;

    private float rayTimer = 0;

    [SerializeField]
    private GameObject selector;

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
    }

    void Start() => ChangeSelectedObject(placedObjects[0]);

    private void Dismiss() => welcomePanel.SetActive(false);

    void Update()
    {
        // do not capture events unless the welcome panel is hidden
        if (welcomePanel.activeSelf)
            return;

        if (rayTimer >= generateRayAfterSeconds)
        {
            // creates a ray from the screen point origin 
            Ray ray = arCamera.ScreenPointToRay(selector.transform.position);

            RaycastHit hitObject;
            if (Physics.Raycast(ray, out hitObject, rayDistanceFromCamera))
            {
                PlacementObj PlacementObj = hitObject.transform.GetComponent<PlacementObj>();
                if (PlacementObj != null)
                {
                    ChangeSelectedObject(PlacementObj);
                }
            }
            else
            {
                ChangeSelectedObject();
            }

            rayTimer = 0;
        }
        else
        {
            rayTimer += Time.deltaTime * 1.0f;
        }
    }

    void ChangeSelectedObject(PlacementObj selected = null)
    {
        foreach (PlacementObj current in placedObjects)
        {
            MeshRenderer meshRenderer = current.GetComponent<MeshRenderer>();
            if (selected != current)
            {
                current.Selected = false;
                meshRenderer.material.color = inactiveColor;
            }
            else
            {
                current.Selected = true;
                meshRenderer.material.color = activeColor;
            }

            if (displayOverlay)
                current.ToggleOverlay();
        }
    }
}