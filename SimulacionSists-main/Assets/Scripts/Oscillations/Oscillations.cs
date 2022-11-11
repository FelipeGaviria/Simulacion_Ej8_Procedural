using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillations : MonoBehaviour
{
    //[SerializeField] private float amplitude = 1;
    [SerializeField] private float period = 1;

    public enum OscillationMode
    {
        Horiz,
        Diag
    }
    [SerializeField] private OscillationMode Mode;

    Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Mode == OscillationMode.Horiz)
        {
            float x = Mathf.Sin(Time.time) * period;
            transform.position = initialPosition + new Vector3(x, 0, 0);
        }

        if (Mode == OscillationMode.Diag)
        {
            float x = Mathf.Sin(5f * Time.time) + Mathf.Cos(Time.time / 3f) + Mathf.Sin(Time.time / 13f);
            transform.position = initialPosition + new Vector3(x, x, 0);
        }
    }
}
