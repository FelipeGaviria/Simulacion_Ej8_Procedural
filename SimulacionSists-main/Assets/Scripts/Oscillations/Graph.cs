using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] GameObject m_prefab;
    [SerializeField] int m_totalPoints = 10;
    [SerializeField] float m_distanceFactor = 1;
    [SerializeField] float m_amplitude = 1;

    private GameObject[] m_allPoints;

    private void Start()
    {
        m_allPoints = new GameObject[m_totalPoints];
        for(int i = 0; i < m_totalPoints; ++i)
        {
            m_allPoints[i] = Instantiate(m_prefab, transform);
        }
    }

    private void Update()
    {
        for (int i = 0; i < m_allPoints.Length; i++)
        {
            float x = i * m_distanceFactor;
            float y = F(x);

            m_allPoints[i].transform.localPosition = new Vector3(x, y, 0);
        }
        float F(float x) => m_amplitude * Mathf.Sin(x + Time.time);// + Mathf.Sin(2f * x);
    }
}
