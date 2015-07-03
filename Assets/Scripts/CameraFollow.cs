using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    float xDistance;
    float yDistance;
    float zDistance;

    void Start()
    {
        xDistance = transform.position.x;
        yDistance = transform.position.y;
        zDistance = transform.position.z;
    }

    void Update()
    {
        transform.position = new Vector3(
            target.position.x + xDistance,
            target.position.y + yDistance,
            target.position.z + zDistance
        );
    }

	
}
