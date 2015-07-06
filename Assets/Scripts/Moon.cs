using UnityEngine;
using System.Collections;


class EllipticalCoord 
{
    public Vector3 CartesianPos()
    {
        Vector3 k = new Vector3(a * Mathf.Cos(t), b * Mathf.Sin(t));
        return k + center;
    }

    float a;
    float b;
    float t;

    Vector3 center;
    public EllipticalCoord(Vector3 _initialPos, float _eccentricity)
    {
        float periapsis = _initialPos.magnitude;
        float apoapsis = (1 + _eccentricity) / (1 - _eccentricity) * periapsis;
        a = (periapsis + apoapsis) / 2;

        //float distToFocus = _initialPos.magnitude - ;
        // F^2 = a^2 - b^2
        // periapsis = A - F = A - sqrt(a^2 - b^2)
        // a^2 - b^2 = (A - periapsis)^2
        // b = sqrt( a^2 - (A - periapsis)^2)
        b = Mathf.Sqrt(a * a - (a - periapsis) * (a - periapsis));

        center = _initialPos - a * _initialPos.normalized;

        t = 0f;
    }

    public void AddTimestep(float dt)
    {
        t += dt;
    }
}

public class Moon : MonoBehaviour 
{
    public float speed;
	// Use this for initialization
    float radius;
    EllipticalCoord pos;

	void Start () 
    {
        radius = transform.localPosition.magnitude;
        pos = new EllipticalCoord(transform.localPosition, 0f);

	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        pos.AddTimestep(speed * Time.fixedDeltaTime);
	}

    void Update()
    {
        transform.localPosition = pos.CartesianPos();
    }
}
