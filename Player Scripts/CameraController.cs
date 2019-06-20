using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // References to GameObjects in Scene.
    public UnityEngine.GameObject player;
    private Vector3 offset;

    public int cameraCurrentZoom = 50;
    public int cameraZoomMax = 1700000;
    public int cameraZoomMin = 20;

    // Use this for initialization
    void Start ()
    {
        // Offset camera based on player position.
        offset = transform.position - player.transform.position;

        Camera.main.orthographicSize = cameraCurrentZoom;
        
	}

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // Back, zoom out.
        {
            // Zoom out rate near size of virus.
            if (cameraCurrentZoom <= 300)
            {
                cameraCurrentZoom += 10;

                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 10);

            }

            // Zoom out rate at interior cell scale.
            else if ((cameraCurrentZoom > 300) && (cameraCurrentZoom < 2000) && (cameraCurrentZoom < cameraZoomMax))
            {
                cameraCurrentZoom += 100;

                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 100);
            }

            // Zoom out rate at cell scale.
            else if ((cameraCurrentZoom >= 2000) && (cameraCurrentZoom < cameraZoomMax) && (cameraCurrentZoom < 5000))
            {
                cameraCurrentZoom += 200;

                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 200);

            }

            // Zoom out rate at blood droplet scale.
            else if ((cameraCurrentZoom >= 5000) && (cameraCurrentZoom < cameraZoomMax) && (cameraCurrentZoom < 20000))
            {
                cameraCurrentZoom += 500;

                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 500);

            }

            // Zoom out rate at microscope slide scale.
            else if ((cameraCurrentZoom >= 20000) && (cameraCurrentZoom <= cameraZoomMax))
            {
                cameraCurrentZoom += 25000;

                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize + 25000);

            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) // Forward, zoom in.
        {
            // Zoom in rate if is near size of virus.
            if ((cameraCurrentZoom <= 300) && (cameraCurrentZoom >= cameraZoomMin))
            {
                cameraCurrentZoom -= 10;
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 10);
            }

            // Zoom in rate at interior cell scale.
            else if ((cameraCurrentZoom >= cameraZoomMin) && (cameraCurrentZoom > 300) && (cameraCurrentZoom < 3000))
            {
                cameraCurrentZoom -= 100;
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 100);
            }

            // Zoom in rate at cell scale.
            else if ((cameraCurrentZoom >= cameraZoomMin) && (cameraCurrentZoom >= 3000) && (cameraCurrentZoom < 10000))
            {
                cameraCurrentZoom -= 400;
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 400);
            }

            // Zoom in rate at droplet scale.
            else if ((cameraCurrentZoom >= cameraZoomMin) && (cameraCurrentZoom >= 10000) && (cameraCurrentZoom < 60000))
            {
                cameraCurrentZoom -= 3000;
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 3000);
            }

            // Zoom in rate at microscope slide scale.
            else if ((cameraCurrentZoom >= cameraZoomMin) && (cameraCurrentZoom >= 60000))
            {
                cameraCurrentZoom -= 25000;
                Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize - 25000);
            }
        }

    }
    // LateUpdate is called every frame after all code is called in Update.
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }
}
