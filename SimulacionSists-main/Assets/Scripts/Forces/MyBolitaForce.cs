using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBolitaForce : MonoBehaviour
{
    public enum BolitaRunMode
    {
        Friction, 
        FluidFriction, 
        Gravity
    }

    public float Mass => mass;

    private MyVector position;
    [SerializeField] private BolitaRunMode runMode;
    [SerializeField] private MyVector velocity;
    [SerializeField] private MyVector acceleration;
    [SerializeField] public float mass = 1f;

    [Header("Forces")]
    [SerializeField] private MyVector wind;
    [SerializeField] private MyVector gravity;
    //private MyVector netForce;

    [Header("Others")]
    [SerializeField] Camera cameraRef;
    [SerializeField] private MyBolitaForce otherBolita;
    [Range(0f, 1f)][SerializeField] private float dampingFactor = 0.9f;
    [Range(0f, 1f)][SerializeField] private float frictionCoeff = 0.9f;

    private void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }

    private void FixedUpdate()
    {
        Vector2 a = Vector2.one;
        float magnitude = a.magnitude;

        //Reset Acc
        acceleration *= 0f;

        //Apply Weight if we're not simulating the newton Grav
        if (runMode != BolitaRunMode.Gravity)
        {
            MyVector weight = gravity * mass;
            ApplyForce(weight);
        }
        // some frictions
        if (runMode == BolitaRunMode.FluidFriction)
        {
            //Fluid Friction
            ApplyFluidFriction();
        }
        else if (runMode == BolitaRunMode.Friction)
        {
            // Friction
            ApplyFriction();
        }
        else if (runMode == BolitaRunMode.Gravity)
        {
            MyVector diff = otherBolita.position - position;
            float distance = diff.magnitude;
            float scalarPart = (mass * otherBolita.mass / (distance * distance));
            MyVector gravity = scalarPart * diff.normalized;
            ApplyForce(gravity);
        }

        //Wind
        //ApplyForce(wind);

        //Integrate Acc and Vel
        Move();
    }

    private void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
    }

    private void Move()
    {
        //Euler Integrator
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        //World is a box only if not simulating gravity
        if (runMode == BolitaRunMode.Gravity)
        {
            CheckLimitSpeed();
        }
        else
        {
            CheckWorldBoxBounds();
        }
        //Tell Unity new Obj's Position
        transform.position = new Vector3(position.x, position.y);
    }
    private void ApplyForce(MyVector force)
    {
        acceleration += force / mass;
    }
    private void ApplyFriction()
    {
        //Friction
        float N = -mass * gravity.y;
        MyVector friction = -frictionCoeff * N * velocity.normalized;
        ApplyForce(friction);
        friction.Draw(position, Color.blue);
    }
    private void ApplyFluidFriction()
    {
        //ApplyFluid Friction
        if (transform.localPosition.y <= 0)
        {
            //Input Variables
            float frontalArea = transform.localScale.x;
            float rho = 1; //densidad
            float fluidDragCoeff = 1;
            float velocityMagnitude = velocity.magnitude;

            //Calc The Force
            float scalarPart = -0.5f * rho * velocityMagnitude * velocityMagnitude * frontalArea * fluidDragCoeff;
            MyVector friction = scalarPart * velocity.normalized;
            ApplyForce(friction);
        }
    }

    private void CheckLimitSpeed (float maxSpeed = 10)
    {
        if (velocity.magnitude > 10)
        {
            velocity = 10 * velocity.normalized;
        }
    }

    private void CheckWorldBoxBounds()
    {
        //Check Horizontal Bounds
        if (position.x > cameraRef.orthographicSize) //CheckRight
        {
            velocity.x *= -1;
            position.x = cameraRef.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        else if (position.x < -cameraRef.orthographicSize) //CheckRight
        {
            velocity.x *= -1;
            position.x = -cameraRef.orthographicSize;
            velocity *= dampingFactor; //Damping

        }

        //Check Vertical Bounds
        if (position.y > cameraRef.orthographicSize) //CheckUp
        {
            velocity.y *= -1;
            position.y = cameraRef.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
        else if (position.y < -cameraRef.orthographicSize) //CheckDown
        {
            velocity.y *= -1;
            position.y = -cameraRef.orthographicSize;
            velocity *= dampingFactor; //Damping
        }
    }
}
