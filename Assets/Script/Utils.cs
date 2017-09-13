using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {
    public static Utils m_instance;
    static float m_screenWidthAtWS;
    static float m_screenHeightAtWS;


    void Start () {
        m_instance = this;

        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width / 2, 0));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0));
        m_screenWidthAtWS = Vector3.Distance(leftEdge, rightEdge);

        Vector3 topEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.height / 2, 0));
        Vector3 bottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, 0));

        m_screenHeightAtWS = Vector3.Distance(topEdge, bottomEdge);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector3 Rotate(Vector3 position, Vector3 angle, Vector3 center)
    {
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = angle;

        position = rotation * (position - center) + center;

        return position;
    }

    public static float GetScreenWidthAtWS()
    {
        return m_screenWidthAtWS;
    }

    public static float GetScreenHeightAtWS()
    {
        return m_screenHeightAtWS;
    }

    public static Vector3[] GetTangentPoints(Vector3 outsidePoint, Vector3 circleCenter, float radius)
    {
        Vector3[] tangentPoints = new Vector3[2];

        Vector3 M = outsidePoint;
        Vector3 O = circleCenter;

        float OM = Vector3.Distance(M, O);
        float OX = Triangle.m_instance.m_inscribedCircleRadius;
        Vector3 OdegPos = new Vector3(O.x, O.y + radius, O.z - 0.1f);
        Debug.Log("OX " + OX + " OM " + OM);

        //calculate x1;
        float alpha2;
        float cosalpha2 = OX / OM;

        alpha2 = Mathf.Rad2Deg * Mathf.Acos(cosalpha2);
        Debug.Log("alpha2 " + alpha2);

        float OM2 = M.y - O.y;

        float alpha;
        float cosalpha = OM2 / OM;
        alpha = Mathf.Rad2Deg * Mathf.Acos(cosalpha);

        Debug.Log("alpha " + alpha);

        Vector3 X;
        Vector3 X2;

        if (M.x < O.x)
        {
            X = Utils.Rotate(OdegPos, new Vector3(0, 0, alpha + alpha2), O);
            X2 = Utils.Rotate(OdegPos, new Vector3(0, 0, alpha - alpha2), O);
        }
        else
        {
            X = Utils.Rotate(OdegPos, new Vector3(0, 0, alpha2 - alpha), O);
            X2 = Utils.Rotate(OdegPos, new Vector3(0, 0, -alpha2 - alpha), O);
        }

        tangentPoints[0] = X;
        tangentPoints[1] = X2;

        return tangentPoints;
    }
}
