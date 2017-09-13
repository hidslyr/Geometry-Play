using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorManager : MonoBehaviour {

    // Use this for initialization
    public static CreatorManager m_instance;
    public Creator m_creator;
    public Triangle m_triangle;
    public Bullet m_bulletTemplate;

    float m_speed;

    List<Bullet> m_bulletList = new List<Bullet>();

    void Start() {
        m_instance = this;

        Vector3 startPosition = new Vector3(m_triangle.m_position.x, m_triangle.m_position.y + Utils.GetScreenWidthAtWS(), m_triangle.m_position.z + 1.1f);
        startPosition = Utils.Rotate(startPosition, new Vector3(0, 0, -30), m_triangle.m_position);
        Debug.Log("hello2 " + startPosition);

        Creator a = Instantiate<Creator>(m_creator, startPosition, new Quaternion(), transform);

        LineRenderer temp = test.m_instance.GetComponent<LineRenderer>();



        //Vector3 M = startPosition;
        //Vector3 O = Triangle.m_instance.m_position;

        //float OM = Vector3.Distance(M, O);
        //float OX = Triangle.m_instance.m_inscribedCircleRadius;
        //Vector3 OdegPos = new Vector3(m_triangle.m_position.x, m_triangle.m_position.y + m_triangle.m_inscribedCircleRadius, m_triangle.m_position.z - 0.1f);
        //Debug.Log("OX " + OX + " OM " + OM);

        ////calculate x1;
        //float alpha2;
        //float cosalpha2 = OX / OM;

        //alpha2 = Mathf.Rad2Deg * Mathf.Acos(cosalpha2);
        //Debug.Log("alpha2 " + alpha2);

        //float OM2 = startPosition.y - m_triangle.m_position.y;

        //float alpha;
        //float cosalpha = OM2 / OM;
        //alpha = Mathf.Rad2Deg * Mathf.Acos(cosalpha);

        //Debug.Log("alpha " + alpha);

        //Vector3 X;
        //Vector3 X2;

        //if (M.x < O.x)
        //{
        //    X = Utils.Rotate(OdegPos, new Vector3(0, 0, alpha + alpha2), O);
        //    X2 = Utils.Rotate(OdegPos, new Vector3(0, 0, alpha - alpha2), O);
        //}
        //else
        //{
        //    X = Utils.Rotate(OdegPos, new Vector3(0, 0, alpha2 - alpha ), O);
        //    X2 = Utils.Rotate(OdegPos, new Vector3(0, 0, -alpha2 - alpha), O);
        //}



        //calculate x2;
        //float alpha3;
        //float OM3 = O.x -M.x;
        //Debug.Log("OM3 " + OM3);
        //float cosalpha3 = OM3 / OM;
        //alpha3 = Mathf.Rad2Deg * Mathf.Acos(cosalpha3);

        //Debug.Log("alpha3 " + alpha3);

        //Vector3 X2 = Utils.Rotate(OdegPos, new Vector3(0, 0, 90 - alpha2 -  alpha3), O);
        Vector3[] tangentPoints = Utils.GetTangentPoints(startPosition, m_triangle.m_position, m_triangle.m_inscribedCircleRadius);
        Vector3 X = tangentPoints[0];
        Vector3 X2 = tangentPoints[1];
        Vector3 tempPos = new Vector3(startPosition.x, startPosition.y, m_triangle.m_position.z - 0.1f);
        temp.positionCount = 3;
        temp.material = new Material(Shader.Find("Particles/Additive"));
        temp.startColor = Color.red;
        temp.endColor = Color.blue;

        temp.SetPosition(0, X);
        temp.SetPosition(1, tempPos);
        temp.SetPosition(2, X2);
    }	

	// Update is called once per frame
	void Update () {
		
	}

    public void ReleaseBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bulllet = Instantiate<Bullet>(m_bulletTemplate, position, new Quaternion(), this.transform);
        bulllet.m_velocity = velocity;
        m_bulletList.Add(bulllet);
    }
}
