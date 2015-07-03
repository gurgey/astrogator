using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
    public float speed;
    public float tilt;
    public float mass;
    public float enginesOnInterval;
    public float energyRechargeTime;
    public Boundary boundary;
    public GameObject gameOverCanvas;
    public GameObject gameWinCanvas;
    public RectTransform battery;
    public FuturePath fp;
    public int energy 
    {
        get
        {
            return (int)battery.FindChild("Slider").GetComponent<Slider>().value;
        }
        set
        {
            Slider s = battery.FindChild("Slider").GetComponent<Slider>();
            if (value <= s.maxValue && value >= s.minValue)
                battery.FindChild("Slider").GetComponent<Slider>().value = value;
        }
    }

    public Vector3 acceleration { get; private set; }
    public Vector3 velocity { get; private set; }

    float originalMass;

    public void ApplyForce(Vector3 direction)
    {
        acceleration = direction / mass;
    }

    void Start()
    {
       // Rigidbody r = (Rigidbody)GetComponent<Rigidbody>();
        energy = (int)battery.FindChild("Slider").GetComponent<Slider>().maxValue;
        originalMass = mass;
        acceleration = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
        transform.FindChild("Engines").gameObject.SetActive(false);
        //velocity = new Vector3(0, 0, 0);
        //r.velocity = GetComponent<NewtonGravity>().OrbitVelocity();
       // int i = 5;
        velocity = GetComponent<NewtonGravity>().OrbitVelocity(mass);
        gameOverCanvas.SetActive(false);
        gameWinCanvas.SetActive(false);

    }
    void Awake()
    {
        
    }


    float enginesInt = 0.0f;
    float rechargeInt = 0.0f;
    void FixedUpdate()
    {
        enginesInt += Time.fixedDeltaTime;
        rechargeInt += Time.fixedDeltaTime;

        //velocity verlet method.


        acceleration = GetComponent<NewtonGravity>().GravForce(transform.position) / mass;
        transform.position += Time.fixedDeltaTime * (velocity + Time.fixedDeltaTime * acceleration / 2f);
        Vector3 newAcceleration = GetComponent<NewtonGravity>().GravForce(transform.position) / mass;
        velocity += Time.fixedDeltaTime * (acceleration + newAcceleration) / 2f;

        if (Input.GetMouseButton(0) && energy > 0)
        {
            
            enginesInt = 0.0f;
            transform.FindChild("Engines").gameObject.SetActive(true);
            //Rigidbody r = (Rigidbody)GetComponent<Rigidbody>();

            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float moveHorizontal = pz.x - transform.position.x;
            float moveVertical = pz.y - transform.position.y;

            float norm = 1f / Mathf.Sqrt(moveHorizontal * moveHorizontal + moveVertical * moveVertical);

            float mh = moveHorizontal * speed * norm;
            float mv = moveVertical * speed * norm;
            velocity += new Vector3(mh, mv, 0f) / mass;

            //Quaternion quot = Quaternion.LookRotation(pz - transform.position, new Vector3(0, 0, -1));

           // transform.rotation = quot;
            //transform.FindChild("Engines").rotation = quot;

            rechargeInt = 0.0f;
            --energy;
        }
        else
        {
            transform.FindChild("Engines").gameObject.SetActive(false);
            if (rechargeInt > energyRechargeTime)
            {
                rechargeInt = 0.0f;
                energy += 100;
            }
        }

        

        

    }

    public List<Vector3> NextPoints()
    {
        int minNum = 10;
        int maxNum = 200;
        float minDist = 0.01f;
        List<Vector3> ret = new List<Vector3>();

        Vector3 fakeAcceleration = new Vector3(acceleration.x, acceleration.y);
        Vector3 fakeVelocity = new Vector3(velocity.x, velocity.y);
        Vector3 fakePosition = new Vector3(transform.position.x, transform.position.y);

        for (int i = 0; i < maxNum; ++i)
        {
            ret.Add(fakePosition);
            
            fakeAcceleration = GetComponent<NewtonGravity>().GravForce(fakePosition) / mass;
            fakePosition += Time.fixedDeltaTime * (fakeVelocity + Time.fixedDeltaTime * fakeAcceleration / 2f);
            Vector3 newAcceleration = GetComponent<NewtonGravity>().GravForce(fakePosition) / mass;
            fakeVelocity += Time.fixedDeltaTime * (fakeAcceleration + newAcceleration) / 2f;
            
            if (i > minNum && (fakePosition - transform.position).sqrMagnitude < minDist)
            {
                break;
            }

        }

        return ret;
    }

    //Vector3 planetPos = new Vector3(0, 3, 0);
    //Vector3 Gf()
    //{
    //    float G = 5.0f;
    //    //Vector3 planetPos = new Vector3(0, 3, 0);

    //    Vector3 rv = planetPos - transform.position;

    //    float r = rv.magnitude;
    //    //distX = (distX < R0) ? distX : distX + R0;

    //    float a = G / r / r;
    //    //Vector3 F = new Vector3(a , , 0);
    //    rv.Normalize();
    //    rv *= a;
    //    //F.Normalize();
    //    //F.x *= a;
    //    //F.y *= a;
    //    //GetComponent<PlayerController>().ApplyForce(F);
    //    return rv;

    //}

    //Vector3 swag()
    //{
    //    Vector3 a = Gf() / mass;
    //    //closest = planets.GetChild(FindClosest());
    //    Vector3 r = planetPos - transform.position;
    //    //float distX = (closest.position.x - thisRB.position.x);
    //    //float distY = (closest.position.y - thisRB.position.y);
    //    //distX = (distX < R0) ? distX : distX + R0;

    //    //float r = Mathf.Sqrt(distX * distX + distY * distY + R0 * R0);
    //    //float a = G / r / r ;
    //    //Vector3 F = new Vector3(distX, distY, 0);
    //    //F.Normalize();
    //    //F.x *= a;
    //    //F.y *= a;

    //    //GetComponent<PlayerController>().ApplyForce(F);

    //    float vMag = 0.3f*Mathf.Sqrt(a.magnitude * r.magnitude);

    //    return new Vector3(vMag * r.y, vMag * r.x, 0);
    //}


    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        gameObject.SetActive(false);
    }


    public void GameWin()
    {
        mass = float.MaxValue;
        velocity = Vector3.zero;
        gameWinCanvas.SetActive(true);
    }

    public void Restart()
    {
        gameObject.SetActive(true);
        //transform.position = new Vector3(0, 0, 0);
        //Rigidbody r = (Rigidbody)GetComponent<Rigidbody>();
        //r.position = new Vector3(0, 0, 0);
       // r.velocity = GetComponent<NewtonGravity>().OrbitVelocity();
        //gameOverCanvas.SetActive(false);
        mass = originalMass;
        Start();
    }
}
