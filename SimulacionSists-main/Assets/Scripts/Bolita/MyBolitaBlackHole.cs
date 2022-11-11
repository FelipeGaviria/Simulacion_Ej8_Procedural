using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBolitaBlackHole : MonoBehaviour
{
    private MyVector position;
    private MyVector acceleration;
    [SerializeField] private MyVector velocity;

    [Header("World")]

    [SerializeField] Camera camera;
    [SerializeField] private Transform BlackHole;

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
        velocity.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);

        MyVector MyPosition = new MyVector(transform.position.x, transform.position.y);
        MyVector BlackHolePosition = new MyVector(BlackHole.position.x, BlackHole.position.y);
        acceleration = BlackHolePosition - MyPosition;
    }
    private void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;
        transform.position = new Vector3(position.x, position.y);
    }
}

