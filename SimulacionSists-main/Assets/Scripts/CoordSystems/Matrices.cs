using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrices : MonoBehaviour
{
    [SerializeField] Vector4 location;

    void Start()
    {
        
    }

    void Update()
    {
        //Construct a Matrix
        Matrix4x4 matrix = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 1, 0), location);
        
        //Calculate global point from local point (Matrix by sector)

        transform.localPosition = matrix * new Vector4(0, 0, 0, 1);
    }
}
