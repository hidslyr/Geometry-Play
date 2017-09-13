using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatorCircle : MonoBehaviour {
    public Triangle m_triangle;
    public int m_segments = 60;
    public float m_radius;
    LineRenderer m_line;
    Vector3 m_position;

    // Use this for initialization
    void Start () {
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width / 2, 0));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0));
        float screenWidthAtWS = Vector3.Distance(leftEdge, rightEdge);

        m_line = GetComponent<LineRenderer>();
        
        m_line.positionCount = m_segments + 1;
        m_position = m_triangle.m_position;
        m_radius = screenWidthAtWS;
        CreatePoints();

        Debug.Log("circle " + m_position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreatePoints()
    {
        float x;
        float y;
        float z = m_position.z + 1.1f;

        float angle = 20f;

        for (int i = 0; i < (m_segments + 1); i++)
        {
            x = m_position.x +  Mathf.Sin(Mathf.Deg2Rad * angle) * m_radius;
            y = m_position.y + Mathf.Cos(Mathf.Deg2Rad * angle) * m_radius;

            m_line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / m_segments);
        }
    }
}
