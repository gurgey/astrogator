using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//class FakeFloat
//{
//    uint value;
//    FakeFloat(uint _value)
//    {

//    }
//}



class ColliderCoord //note: for a capsule collider
{
    public Vector3 CartesianPos(Vector3 parentPos)
    {

        if (position <= topRight)
        {
            return new Vector3(zeroVec.x, zeroVec.y + (position - zero).ToFloat(p), 0) + parentPos;
        }
        else if (position <= topLeft)
        {
            float angle = (position - topRight).ToFloat(p) / radius.ToFloat(p);
            return new Vector3(radius.ToFloat(p) * Mathf.Cos(angle), topRightVec.y + radius.ToFloat(p) * Mathf.Sin(angle), 0) + parentPos;
        }
        else if (position <= bottomLeft)
        {
            return new Vector3(topLeftVec.x, topLeftVec.y - (position - topLeft).ToFloat(p), 0) + parentPos;
        }
        else
        {
            float angle = (position - bottomLeft).ToFloat(p) / radius.ToFloat(p);
            return new Vector3(-radius.ToFloat(p) * Mathf.Cos(angle), bottomLeftVec.y - radius.ToFloat(p) * Mathf.Sin(angle), 0) + parentPos;
        }
    }

    ModInt radius;
    ModInt height;

    //0 is at the bottom of the right length.
    ModInt zero;
    Vector3 zeroVec;

    //height is the top of the right length
    ModInt topRight;
    Vector3 topRightVec;

    //height + 1/2 circleper is top of the left length
    ModInt topLeft;
    Vector3 topLeftVec;

    //2*height + 1/2 circleper is the bottom of the right leng
    ModInt bottomLeft;
    Vector3 bottomLeftVec;

    ModInt position; //really a fraction
    float p;

    public ColliderCoord(float _radius, float _height, float _fraction)
    {
        p = 2 * _height + 2 * Mathf.PI * _radius;

        height = new ModInt(_height, p);
        radius = new ModInt(_radius, p);

        position = new ModInt(_fraction * p, p);


        zeroVec = new Vector3(_radius, -_height / 2f, 0);
        zero = new ModInt(0f, p);

        topRightVec = new Vector3(_radius, _height / 2f, 0);
        topRight = new ModInt(_height, p);

        topLeftVec = new Vector3(-_radius, _height / 2f, 0);
        topLeft = new ModInt(topRight.ToFloat(p) + Mathf.PI * _radius, p);

        bottomLeftVec = new Vector3(-_radius, -_height / 2f, 0);
        bottomLeft = new ModInt(topLeft.ToFloat(p) + _height, p);
    }

    public void AddTimestep(uint speed)
    {
        position += speed;
    }
}

public class Asteroid : MonoBehaviour 
{

    public GameObject originalAsteroid;
    public int numAsteroids;
    public uint speed;
    List<GameObject> asteroids;
    List<ColliderCoord> asteroidCoords;


	void Start () 
    {
        originalAsteroid.SetActive(true);
        asteroids = new List<GameObject>();
        asteroidCoords = new List<ColliderCoord>();
        CapsuleCollider mc = GetComponent<CapsuleCollider>();

        //float p = 2 * Mathf.PI * mc.radius + 2 * mc.height;
        float space = 1f / numAsteroids;
        for (int i = 0; i < numAsteroids; ++i)
        {
            ColliderCoord ac = new ColliderCoord(mc.radius, mc.height - 2 * mc.radius, space * i);
            GameObject newAst = Instantiate(originalAsteroid);

            newAst.transform.parent = transform;
            newAst.transform.position = ac.CartesianPos(transform.position);

            newAst.name = i.ToString();
            newAst.GetComponent<Rigidbody>().angularVelocity = Random.rotation.eulerAngles;

            asteroids.Add(newAst);
            asteroidCoords.Add(ac);
        }
        originalAsteroid.SetActive(false);
	}

    void OnTriggerExit(Collider other)
    { 
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().GameOver();
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < numAsteroids; ++i)
        {
            asteroidCoords[i].AddTimestep(speed);
        }
    }

	void Update () 
    {
	    for (int i = 0; i < numAsteroids; ++i)
        {
            asteroids[i].transform.position = asteroidCoords[i].CartesianPos(transform.position);
        }
	}
}
