using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweens : MonoBehaviour
{
    //Serialized
    [SerializeField]
    private Transform targetTransform;
    [Header("Tween related")]
    [SerializeField, Range(0, 1)]
    private float normalizedTime;
    [SerializeField]
    private float duration = 5;
    [Header("Parameters")]
    [SerializeField]
    private Color initialColor;
    [SerializeField]
    private Color finalColor;
    [SerializeField]
    private AnimationCurve curve;

    //Internals
    private float currentTime = 0f;
    private Vector3 initialPosition;
    private Vector3 finalPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartTween();
    }

    private void Update()
    {
        //Calculate Normalized Time
        normalizedTime = currentTime / duration;

        // Interpolate Position and Color
        transform.position = Vector3.Lerp(initialPosition, finalPosition, EaseInQuad(normalizedTime));
        spriteRenderer.color = Color.Lerp(initialColor, finalColor, EaseInQuad(normalizedTime));
        
        //Increase Time Every Frame
        currentTime += Time.deltaTime;

        if (normalizedTime >=1)
        {
            Debug.Log("Completed");
        }
        if (Input.GetKeyDown(KeyCode.Space)) StartTween();
    }

    private void StartTween()
    {
        currentTime = 0f;
        initialPosition = transform.position;
        finalPosition = targetTransform.position;
    }

    private float EaseInQuad(float x)
    {
        return x * x;
    }
}
