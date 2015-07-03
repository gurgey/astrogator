using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    float xDistance;
    float yDistance;
    float zDistance;
    public float zoomCoeff;
    public float zoomT;
    public float travelT;
    public float xDistBound;

    //Matrix4x4 nrProjection;

    new Camera camera;

    void Start()
    {
        xDistance = transform.position.x;
        yDistance = transform.position.y;
        zDistance = transform.position.z;

        vel = new Vector3(0, 0);
        prevXL = 0f;
        prevYL = 0f;

        camera = GetComponent<Camera>();
       // nrProjection = camera.projectionMatrix;
    }

    Vector3 vel;
    float prevXL;
    float prevYL;
    public Vector3 velocity
    {
        get
        {
            return vel;
        }
    }

    
    void Update()
    {
        camera.ResetProjectionMatrix();
        //float mxd = Mathf.Min(target.GetComponent<PlayerController>().maxXDiff, xDistBound);
        //PlayerController pc = target.GetComponent<PlayerController>();
        //if (pc.mass != float.MaxValue && target.gameObject.activeSelf)
        //{
        //    Bounds bounds = target.GetComponent<PlayerController>().camBounds;
        //    float mxd = Mathf.Max(bounds.size.x * pc.gamma.x, bounds.size.y * pc.gamma.y);

        //    GetComponent<Camera>().orthographicSize = Mathf.Lerp
        //    (
        //        GetComponent<Camera>().orthographicSize,
        //        zoomCoeff * mxd,
        //        Time.deltaTime * zoomT
        //    );

        //    Vector3 cent = bounds.center;
        //    float xl = Mathf.Lerp(transform.position.x*pc.gamma.x, cent.x + xDistance * pc.gamma.x, travelT);
        //    float yl = Mathf.Lerp(transform.position.y*pc.gamma.y, cent.y + yDistance * pc.gamma.y, travelT);
        //    vel = new Vector3(xl - prevXL, yl - prevYL);
        //    transform.position = new Vector3
        //    (
        //        xl,
        //        yl,
        //        zDistance
        //    );
        //}
        //camera.projectionMatrix = nrProjection;
        PlayerController pc = target.GetComponent<PlayerController>();
        if (pc.mass != float.MaxValue && target.gameObject.activeSelf)
        {
            Bounds bounds = target.GetComponent<PlayerController>().camBounds;
            float mxd = Mathf.Max(bounds.size.x, bounds.size.y);

            GetComponent<Camera>().orthographicSize = Mathf.Lerp
            (
                GetComponent<Camera>().orthographicSize,
                zoomCoeff * mxd,
                Time.deltaTime * zoomT
            );

            Vector3 cent = bounds.center;
            float xl = Mathf.Lerp(transform.position.x, cent.x + xDistance, travelT);
            float yl = Mathf.Lerp(transform.position.y, cent.y + yDistance, travelT);
            vel = new Vector3(xl - prevXL, yl - prevYL);
            transform.position = new Vector3
            (
                xl,
                yl,
                zDistance
            );
        }
        //nrProjection = camera.projectionMatrix;
        //transform.position = new Vector3
        //(
        //    target.position.x + xDistance,
        //    target.position.y + yDistance,
        //    target.position.z + zDistance

        //);
        Matrix4x4 p = camera.projectionMatrix;
        p.m00 *= pc.gamma.x;
        p.m11 *= pc.gamma.y;

        camera.projectionMatrix = p;
    }

	
}
