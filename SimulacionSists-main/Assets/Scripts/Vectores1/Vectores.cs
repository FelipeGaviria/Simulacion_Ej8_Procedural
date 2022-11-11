using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Vectores : MonoBehaviour
{
    [SerializeField] MyVector myFirstVector = new MyVector(2, 3);
    [SerializeField] MyVector mySecondVector = new MyVector (3, 4);
    [Range(0, 1)][SerializeField] float scalar = 0; 

    private void Update()
    {        
        #region Debug
        myFirstVector.Draw(Color.red);
        mySecondVector.Draw(Color.green);

        MyVector diff = (mySecondVector - myFirstVector) * scalar;
        diff.Draw(Color.yellow);

        MyVector final = myFirstVector + diff;

        diff.Draw(Color.yellow);
        final.Draw(mySecondVector, Color.yellow);
        diff.Draw(Color.blue);
        #endregion
    }
    //public static Vector 2 operator +(Vector2 a , Vector2 b)
}