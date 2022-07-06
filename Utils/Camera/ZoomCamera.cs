using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour {

	public int min_zoom = 5;
	public int max_zoom = 20;
	public float zoom_speed = 1.0f;
	public Camera cam;

    private float dest_size;

	void FixedUpdate () {


            float v = Input.mouseScrollDelta.y;

        dest_size -= v * zoom_speed;
        dest_size = Mathf.Clamp(dest_size, min_zoom, max_zoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, dest_size, 0.2f);
        
        
        
	}

}

