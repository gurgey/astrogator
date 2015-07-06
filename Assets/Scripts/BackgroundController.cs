using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public float scrollSpeed;
    public Camera cam;

    float initialHeight;
    float initialZoom;
    Vector3 initialScale;

	// Use this for initialization
	void Start () {
        initialHeight = transform.localPosition.z;
        initialScale = transform.localScale;
        initialZoom = cam.orthographicSize;
	}

    void FixedUpdate()
    {
        Vector3 transVec = new Vector3(cam.velocity.x, cam.velocity.y, 0f);
        transform.position -= transVec * scrollSpeed * Time.fixedDeltaTime;

        //Vector3 finalScale = cam.orthographicSize / initialZoom * initialScale;
        transform.localScale = ( (cam.orthographicSize - initialZoom) * scrollSpeed  * 0.1f   + 1 ) * initialScale;

    }


	// Update is called once per frame
	void Update () {

	}
}
