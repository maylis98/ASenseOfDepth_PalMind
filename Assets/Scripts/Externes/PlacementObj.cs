using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacementObj: MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    [SerializeField]
    private bool IsLocked;

    public bool Selected
    {
        get
        {
            return this.IsSelected;
        }
        set
        {
            IsSelected = value;
        }
    }

    public bool Locked
    {
        get
        {
            return this.IsLocked;
        }
        set
        {
            IsLocked = value;
        }
    }

    [SerializeField]
    private GameObject TextBox;

    [SerializeField]
    private TextMeshProUGUI OverlayText;

    [SerializeField]
    private string OverlayDisplayText;

    [SerializeField]
    private GameObject canvasGameObject;

    [SerializeField]
    private Vector3 canvasPosition = Vector3.zero;


    private Canvas canvasComponent;

    public void SetOverlayText(string text)
    {
            if(OverlayText != null)
            {
                OverlayText.gameObject.SetActive(true);
                OverlayText.text = text;
            }
    }


    void Awake()
    {
        OverlayText = TextBox.GetComponent<TextMeshProUGUI>();

        if(OverlayText != null)
        {
            OverlayText.gameObject.SetActive(false);
        }
    }

    public void ToggleOverlay()
    {
        OverlayText.gameObject.SetActive(IsSelected);
        OverlayText.text = OverlayDisplayText;
    }

    public void ToggleCanvas()
    {
        if(canvasComponent != null && canvasComponent == null)
        {
            canvasGameObject = Instantiate(canvasGameObject, canvasPosition, Quaternion.identity);
            canvasComponent = canvasGameObject.GetComponent<Canvas>();
            
        }
        canvasComponent?.gameObject.SetActive(IsSelected);

    }
}
