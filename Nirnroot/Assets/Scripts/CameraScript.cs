using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    Camera cam;
    Vector3 targetPos, camVelocity;
    const float ZOOM_PADDING = 5f, CAMERA_SPEED = 8f;
    float targetZoom, zoomVelocity;
    public float normalSize { get; set; }
    public Vector2 normalPos{get; set; }
    bool framing = false;

	// Use this for initialization
	void Start () {
        cam = gameObject.GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position != targetPos && framing == true)
            UpdateFraming();
        else
            framing = false;
    }

    //Moves Camera to selected position, and changes it's size based on a factor
    public void ResetCamera()
    {
        cam.transform.position = new Vector3(normalPos.x, -normalPos.y + 11, -10);
        cam.orthographicSize = normalSize;
    }


    //Moves Camera between 2 Hexes, and zooms it so to optimize their sizes
    public void InitiateFraming(string hex1, string hex2)
    {
        framing = true;

        Vector2 pos1 = GameObject.Find(hex1).transform.position;
        Vector2 pos2 = GameObject.Find(hex2).transform.position;

        targetPos = new Vector3((pos2.x + pos1.x) / 2, (pos2.y + pos1.y) / 2, -10);

        if (Mathf.Abs(pos2.y - pos1.y)> Mathf.Abs(pos2.x - pos1.x) / Camera.main.aspect)
            targetZoom = Mathf.Abs(pos2.y - pos1.y) + ZOOM_PADDING;
        else
            targetZoom = (Mathf.Abs(pos2.x - pos1.x) + ZOOM_PADDING) / Camera.main.aspect;
    }
    //Updates the Camera's position and zoom smoothly
    public void UpdateFraming()
    {
        camVelocity = new Vector3((targetPos.x - transform.position.x) * CAMERA_SPEED, (targetPos.y - transform.position.y) * CAMERA_SPEED, 0);
        zoomVelocity = (targetZoom / 2 - Camera.main.orthographicSize) * CAMERA_SPEED;
        cam.transform.position += camVelocity * Time.deltaTime;
        cam.orthographicSize += zoomVelocity * Time.deltaTime;
    }
}
