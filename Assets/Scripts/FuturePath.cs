using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using DotNetMatrix;
public class FuturePath : MonoBehaviour 
{
    public GameObject player;
	// Use this for initialization
	void Start () 
    {
	
	}

    //KeyValuePair<Func<float, float>, Func<float, float>> Parameterize(List<float> poly)
    //{
    //    float e = 1.0f; //param I get to tune
    //    float a = poly[0]*e;
    //    float b = poly[1]*e;
    //    float c = poly[2]*e;
    //    float d = poly[3]*e;
    //    float f = poly[4]*e;
    //    //if ()
    //}

    //public void Redraw()
    //{
    //    // conic equation looks like:
    //    // ax^2 + bxy + cy^2 + dx + ey + f = 0
    //    // given 5 points on the spaceship's path, find the
    //    // time-paramaterized version of this conic.
    //    // i.e. x(t), y(t)

    //    List<Vector3> points = player.GetComponent<PlayerController>().Next5Points();

    //    //Matri
    //    double[][] mat = new double[5][];
    //    double[][] aux = new double[5][];
    //    for (int i = 0; i < 5; ++i )
    //    {
    //        mat[i] = new double[5];
    //        aux[i] = new double[1];
    //    }
    //    //GeneralMatrix mat = new GeneralMatrix(6, 6);
    //    // Matrix fmat = new Matrix(6, 1);
    //    for (int i = 0; i < 5; ++i)
    //    {
    //        float x = points[i].x;
    //        float y = points[i].y;
    //        mat[i][0] = x * x;
    //        mat[i][1] = x * y;
    //        mat[i][2] = y * y;
    //        mat[i][3] = x;
    //        //mat[i][4] = y;
    //        mat[i][4] = 1;

    //        aux[i][0] = -y;
    //    }

    //    GeneralMatrix a = new GeneralMatrix(mat);
    //    GeneralMatrix b = new GeneralMatrix(aux);
    //    GeneralMatrix cap = a.Solve(b);

    //    List<float> poly = new List<float>();
    //    for (int i = 0; i < 5; ++i)
    //    {
    //        poly.Add((float)cap.GetElement(i, 0));
    //    }

    //    //var wow = Parameterize(poly);
    //    //Matrix sol = mat.SolveWith(fmat);
    //    //mat.
    //    int rrr = 5;
    //}

	// Update is called once per frame
	void Update () 
    {
        List<Vector3> somuch = player.GetComponent<PlayerController>().NextPoints();
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(somuch.Count);
        for (int i = 0; i < somuch.Count; ++i)
            lr.SetPosition(i, somuch[i]);
	}
}
