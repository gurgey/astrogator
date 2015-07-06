using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewtonGravity : MonoBehaviour {
    public float G;
    public float R0;
    public float realG;
    public float realR0;
    public float whatParam;
    public Transform planets;

    Transform closest;
    
    void Start()
    {
        closest = planets.GetChild(FindClosest(transform.position));
        
        //px = 0;
        //py = 3;
    }

    int FindClosest(Vector3 pos)
    {
        int best = -1;
        float shortest = int.MaxValue;

        for (int p = 0; p < planets.childCount; ++p)
        {
            if (!planets.GetChild(p).gameObject.activeSelf)
                continue;

            float px = planets.GetChild(p).position.x;
            float py = planets.GetChild(p).position.y;
            float distX = (pos.x - px);
            float distY = (pos.y - py);

           // UpdateArrows(distX, distY);
           // if (distX*distX + disy)
            float r = distX * distX + distY * distY;
            if (r < shortest)
            {
                shortest = r;
                best = p;
            }
            

            
        }
        return best;
    }

    void FixedUpdate()
    {
        closest = planets.GetChild(FindClosest(transform.position));
        //closest = planets.GetChild(FindClosest());

        //float distX = (closest.position.x - thisRB.position.x);
        //float distY = (closest.position.y - thisRB.position.y);
        ////distX = (distX < R0) ? distX : distX + R0;

        //float r = Mathf.Sqrt(distX * distX + distY * distY + R0 * R0);
        //float a = G / r / r;
        //Vector3 F = new Vector3(distX, distY, 0);
        //F.Normalize();
        //F.x *= a ;
        //F.y *= a;
        //GetComponent<PlayerController>().ApplyForce(F);

    }

    public Vector3 GravForce(Vector3 pos)
    {
        Transform fakeClosest = planets.GetChild(FindClosest(pos));


        //int count = 0;
        //foreach (Transform p in planets)
        //{
        //    ++count;
        //    if (count > 2)
        //    {
        //        break;
        //    }
        float distX = whatParam*(fakeClosest.position.x - pos.x);
        float distY = whatParam*(fakeClosest.position.y - pos.y);
        //distX = (distX < R0) ? distX : distX + R0;

        
        float r = Mathf.Sqrt(distX * distX + distY * distY + R0 * R0 * whatParam * whatParam);
        float a = G / r / r / r;
        Vector3 F = new Vector3(a * distX, a * distY);

        //}
        //F.Normalize();
        //F.x *= a;
        //F.y *= a;
        //GetComponent<PlayerController>().ApplyForce(F);
        return F;
    }


    public Vector3 RealGravForce(Vector3 pos)
    {
        Vector3 F = new Vector3(0, 0);
        foreach (Transform p in planets)
        {

            float distX = (p.position.x - pos.x);
            float distY = (p.position.y - pos.y);
            distX *= whatParam;
            distY *= whatParam;

            float r = Mathf.Sqrt(distX * distX + distY * distY + realR0 * realR0 * whatParam * whatParam);
            float a = realG / r / r / r;
            F.x += a * distX;
            F.y += a * distY;

        }

        return F;
    }
    public Vector3 OrbitVelocity(float mass)
    {
        // v^2 /r = a_g
        // v = sqrt(r a_g)
        //FixedUpdate();
        Vector3 acceleration = GravForce(transform.position) / mass;
        closest = planets.GetChild(FindClosest(transform.position));
        Vector3 r = closest.position - transform.position;
        //float distX = (closest.position.x - thisRB.position.x);
        //float distY = (closest.position.y - thisRB.position.y);
        //distX = (distX < R0) ? distX : distX + R0;

        //float r = Mathf.Sqrt(distX * distX + distY * distY + R0 * R0);
        //float a = G / r / r ;
        //Vector3 F = new Vector3(distX, distY, 0);
        //F.Normalize();
        //F.x *= a;
        //F.y *= a;
        
        //GetComponent<PlayerController>().ApplyForce(F);

        float vMag = Mathf.Sqrt(acceleration.magnitude * r.magnitude) / 3f;

        return new Vector3(vMag * r.y, vMag * r.x, 0);


    }

}
