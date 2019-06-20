using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControlScript : MonoBehaviour {

    // Reference to player.
    public PlayerController Player;
    public bool touchingInfectableCell;

    // Reference to the Main Camera.
    public CameraController MainCamera;

    // Referenced variables from above Camera object script.
    public int cameraCurrentZoom;


    // Reference Text objects used to display real time scale data to player.
    public Text CurrentScaleNano;
    public Text CurrentScaleMicro;
    public Text CurrentScaleMilli;
    public Text CellTouchIndicator;

    // Variables to store data to output.
    public double convertedToNanoCameraScale;
    public double convertedToMicro;
    public double convertedToMilli;
    public string canInteractDisplayString;

    // Methods
    void ConvertCameraScaleToNanoMetre()
    {
        convertedToNanoCameraScale = MainCamera.cameraCurrentZoom * 3.675;
    }

    void ConvertNanoToAllUnits()
    {
        convertedToMicro = convertedToNanoCameraScale * 0.001;
        convertedToMilli = convertedToMicro * 0.001;
    }

    bool CheckTouchingInfectableCell()
    {
        return touchingInfectableCell = Player.touchingInfectableCell;
    }

    void DisplayHUDIndicators()
    {
        CurrentScaleNano.text = convertedToNanoCameraScale.ToString();
        CurrentScaleMicro.text = convertedToMicro.ToString();
        CurrentScaleMilli.text = convertedToMilli.ToString();

        if (CheckTouchingInfectableCell() == true)
        {
            canInteractDisplayString = "Can Interact";
        }

        else
        {
            canInteractDisplayString = "Cannot Interact";
        }

        CellTouchIndicator.text = canInteractDisplayString;
    }

	// Use this for initialization
	void Start () {
        ConvertCameraScaleToNanoMetre();
        ConvertNanoToAllUnits();
        DisplayHUDIndicators();
    }
	
	// Update is called once per frame
	void Update () {
        
        ConvertCameraScaleToNanoMetre();
        ConvertNanoToAllUnits();
        DisplayHUDIndicators();
	}
}
