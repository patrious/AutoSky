using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Point3D
{
    public float X;
    public float Y;
    public float Z;

    public Point3D(string x, string y, string z)
    {
        X = float.Parse(x);
        Y = float.Parse(y);
        Z = float.Parse(z);

    }

    public Point3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
