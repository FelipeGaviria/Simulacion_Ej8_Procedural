using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingToBall : MonoBehaviour
{
    private enum MovementMode
    {
        ConstantVelocity = 0,
        Acceleration
    }

    private Vector3 acceleration;
    private Vector3 velocity;

    [SerializeField] private MovementMode movementMode;
    [SerializeField] private float speed;

    private void Update()
    {
        UpdateMovementVectors();
        //Integrae Vel and Acc
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        RotateZ(Mathf.Atan2(velocity.y, velocity.x) - Mathf.PI / 2f);
    }

    private void UpdateMovementVectors()
    {
        //Calculate Vel
        if (movementMode == MovementMode.ConstantVelocity)
        {
            velocity = GetWorldMousePosition() - transform.position;
            velocity.Normalize();
            velocity *= speed;
            acceleration *= 0;
        }
        else if (movementMode == MovementMode.Acceleration)
        {
            acceleration = GetWorldMousePosition() - transform.position;
        }
    }

    private Vector3 GetWorldMousePosition()
    {
        Camera camera = Camera.main;
        Vector3 screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);
        return worldPos;
    }

    private void RotateZ(float radians)
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, radians * Mathf.Rad2Deg);
    }
}
