using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorManager : MonoBehaviour {

    // Use this for initialization
    public static CreatorManager m_instance;
    public Creator m_creator;
    public Triangle m_triangle;
    public Bullet m_bulletTemplate;
    public const float RANGE_BETWEEN_TRIANGLE_AND_BULLET = 0.5f;

    float m_speed;

    List<Bullet> m_bulletList = new List<Bullet>();

    void Awake()
    {
        DataLoader.Load("Assets/Level/Creator.csv");
    }

    void Start() {
        m_instance = this;

        Vector3 startPosition = new Vector3(m_triangle.m_position.x, m_triangle.m_position.y + Utils.GetScreenWidthAtWS(), m_triangle.m_position.z + 1.1f);
        startPosition = Utils.Rotate(startPosition, new Vector3(0, 0, -30), m_triangle.m_position);
        Debug.Log("hello2 " + startPosition);
        Instantiate<Creator>(m_creator, startPosition, new Quaternion(), this.transform);
    }	

	// Update is called once per frame
	void Update () {
		
	}

    public void ReleaseBullet(Vector3 position, float speed, bool isLeft)
    {
        Vector3 tangentPoint = Utils.GetTangentPoint(position, m_triangle.m_position, m_triangle.m_inscribedCircleRadius + RANGE_BETWEEN_TRIANGLE_AND_BULLET, isLeft);
        Vector3 releasePos = new Vector3(position.x, position.y, m_triangle.m_position.z);
        Bullet bulllet = Instantiate<Bullet>(m_bulletTemplate, releasePos, new Quaternion(), this.transform);
        bulllet.m_velocity = Utils.findVelocity(position, tangentPoint, speed);
        m_bulletList.Add(bulllet);
    }
}
