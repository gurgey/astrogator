using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    float xDistance;
    float yDistance;
    float zDistance;
    public float zoomCoeff;
    //public float zoomT;
    //public float travelT;
    //public float xDistBound;
    //Matrix4x4 nrProjection;
    public float minZoom;
    public float speed;

    public GameObject playerInterfaceCameraGO;
    public GameObject mainCameraGO;

    Camera playerInterfaceCamera;
    Camera mainCamera;

    public bool relativistic;
    public float speedOfLight;

    void Start()
    {
        xDistance = transform.position.x;
        yDistance = transform.position.y;
        zDistance = transform.position.z;

        playerInterfaceCamera = playerInterfaceCameraGO.GetComponent<Camera>();
        mainCamera = mainCameraGO.GetComponent<Camera>();

    }




    public Vector3 gamma
    {
        get
        {
            if (!relativistic)
                return new Vector3(1.0f, 1.0f);
            else
            {
                float beta2x = mainCamera.velocity.x * mainCamera.velocity.x / speedOfLight / speedOfLight;
                float gammaX = 1f / Mathf.Sqrt(1 - beta2x);

                float beta2y = mainCamera.velocity.y * mainCamera.velocity.y / speedOfLight / speedOfLight;
                float gammaY = 1f / Mathf.Sqrt(1 - beta2y);
                return new Vector3(gammaX, gammaY);
            }
        }
    }

    void LateUpdate()
    {
        //mainCamera.ResetProjectionMatrix();

        PlayerController pc = target.GetComponent<PlayerController>();
        if (pc.mass != float.MaxValue && target.gameObject.activeSelf)
        {
            Bounds bounds = target.GetComponent<PlayerController>().camBounds;
            float mxd = Mathf.Max(bounds.size.x, bounds.size.y, minZoom);
            //mxd = Mathf.Max(mxd, minZoom);
            //float sz = Mathf.Lerp
            //(
            //    mainCamera.orthographicSize,
            //    zoomCoeff * mxd,
            //    Time.deltaTime * zoomT
            //);

            float sz = Mathf.MoveTowards(mainCamera.orthographicSize, zoomCoeff * mxd, Time.smoothDeltaTime * speed);
            mainCamera.orthographicSize = sz;
           // playerInterfaceCamera.orthographicSize = sz;
            


            Vector3 cent = bounds.center;
            float xl = Mathf.MoveTowards(mainCamera.transform.position.x, cent.x + xDistance, Time.smoothDeltaTime * speed);
            float yl = Mathf.MoveTowards(mainCamera.transform.position.y, cent.y + yDistance, Time.smoothDeltaTime * speed);
            mainCamera.transform.position = new Vector3
            (
                xl,
                yl,
                zDistance
            );
        }

        //Matrix4x4 p = mainCamera.projectionMatrix;
        //p.m00 *= gamma.x;
        //p.m11 *= gamma.y;

        //mainCamera.projectionMatrix = p;
    }

	
}
