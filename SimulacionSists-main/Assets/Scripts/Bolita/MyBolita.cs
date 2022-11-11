//#define USE_HARD_MODE
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBolita : MonoBehaviour
{
    private MyVector position;
    //[SerializeField] private MyVector displacement;
    private MyVector displacement;
    [SerializeField] private MyVector velocity;
    [SerializeField] private MyVector acceleration;
    [Range(0f, 1f)][SerializeField] private float dampingFactor = 0.9f;

    [Header("World")]

    [SerializeField] Camera camera;

    private int currentAccelerationCounter = 0;
    private readonly MyVector[] directions = new MyVector[4]
    {
        new MyVector(0, -9.8f),
        new MyVector(9.8f, 0f),
        new MyVector(0, 9.8f),
        new MyVector(-9.8f, 0f)
    };

    private void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        position.Draw(Color.blue);
        displacement.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Change the acc
            acceleration = directions[(++currentAccelerationCounter) % directions.Length];
            velocity *= 0;
        }

    }
    private void Move()
    {
        //Integral de acc es v y la int de vel es pos
        //Euler Integrator
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        #region Check Bound Easy to read Mode

        //Check Horizontal Bounds
        if (position.x > camera.orthographicSize) //CheckRight
        {
            velocity.x *= -1;
            position.x = camera.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        else if (position.x < -camera.orthographicSize) //CheckRight
        {
            velocity.x *= -1;
            position.x = -camera.orthographicSize;
            velocity *= dampingFactor; //Damping

        }
        //Check Vertical Bounds
        if (position.y > camera.orthographicSize) //CheckUp
        {
            velocity.y *= -1;
            position.y = camera.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        else if (position.y < -camera.orthographicSize) //CheckDown
        {
            velocity.y *= -1;
            position.y = -camera.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        #endregion
        #region Check Bound Hard to read Mode
#if USE_HARD_MODE

        Debug.Log("Moving");

        CheckBounds(ref position.x, ref displacement.x, camera.orthographicSize);
        CheckBounds(ref position.y, ref displacement.y, camera.orthographicSize);
#endif
        #endregion

        transform.position = new Vector3(position.x, position.y);
    }
    #region HardMode
#if USE_HARD_MODE
    private void CheckBounds(ref float x, ref float displacementX, float halfWidth)
    {
        if (Mathf.Abs(x) > halfWidth)
        {
            displacementX = displacementX * -1;
            x = Mathf.Sign(x) * camera.orthographicSize;
        }
        //Check if ball is visible by camera (horizontally) and invert X
        //Check if ball is visible by camera (vertically) and invert X
    }
#endif
    #endregion
}

