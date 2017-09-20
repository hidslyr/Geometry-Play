using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorManager : MonoBehaviour {

    // Use this for initialization
    public static CreatorManager m_instance;
    public Creator m_creator;
    public Triangle m_triangle;
    public Bullet m_bulletTemplate;
    public float m_totalTimePassed = 0f;
    public const float RANGE_BETWEEN_TRIANGLE_AND_BULLET = 0.5f;

    List<Bullet> m_bulletList = new List<Bullet>();
    List<Creator> m_creatorList = new List<Creator>();

    List<Data.Obstacle> m_obstaclesData;
    List<Data.Creator> m_creatorsData;
    List<Data.Pattern> m_patternsData;

    class PendingCreator
    {
        public PendingCreator(float releaseTime, Data.Creator creatorData, int direction)
        {
            m_creatorData = creatorData;
            m_releaseTime = releaseTime;
            m_direction = direction;
        }

        public float m_releaseTime;
        public Data.Creator m_creatorData;
        public int m_direction;
    }
    List<PendingCreator> m_pendingCreator = new List<PendingCreator>();

    void Awake()
    {
        List<string> temp = DataLoader.Load("Assets/Level/Pattern.csv");
        m_patternsData = DataLoader.LoadPatterns(temp);

        temp = DataLoader.Load("Assets/Level/Creator.csv");
        m_creatorsData = DataLoader.LoadCreators(temp);

        LaunchPattern(m_patternsData[0]);

        //foreach(Data.Pattern crt in m_patternsData)
        //{
        //    Debug.Log("Obstacle " + crt.m_ID + " " + crt.m_creators.Count + " " + crt.m_creators[0]);
        //}

        //Debug.Log("test " + m_creatorsData[0].m_Obstacles[0].GetDebugInfo());
    }

    void Start() {
        m_instance = this;

        //Vector3 startPosition = new Vector3(m_triangle.m_position.x, m_triangle.m_position.y + Utils.GetScreenWidthAtWS(), m_triangle.m_position.z + 1.1f);
        //startPosition = Utils.Rotate(startPosition, new Vector3(0, 0, -30), m_triangle.m_position);
        //Instantiate<Creator>(m_creator, startPosition, new Quaternion(), this.transform);
    }	

	// Update is called once per frame
	void Update () {
        m_totalTimePassed += Time.deltaTime;

        for(int i=0; i< m_pendingCreator.Count; i++)
        {
            if (m_totalTimePassed >= m_pendingCreator[i].m_releaseTime)
            {
                ReleaseCreator(m_pendingCreator[i]);               
                Debug.Log("Release creator " + m_pendingCreator[i].m_creatorData.m_ID + " at " + m_totalTimePassed);
                m_pendingCreator.Remove(m_pendingCreator[i]);
                i--;
            }
        }
	}

    public void ReleaseBullet(Vector3 position, Data.ObstacleInstance obstacle)
    {
        bool isLeft = obstacle.m_direction == 0 ? true : false;
        Vector3 tangentPoint = Utils.GetTangentPoint(position, m_triangle.m_position, m_triangle.m_inscribedCircleRadius + (float)obstacle.m_deviation / 20, isLeft);
        Vector3 releasePos = new Vector3(position.x, position.y, m_triangle.m_position.z);

        Bullet bulllet = Instantiate<Bullet>(m_bulletTemplate, releasePos, new Quaternion(), this.transform);
        bulllet.m_velocity = Utils.findVelocityByTime(position, tangentPoint, obstacle.m_speed);
        m_bulletList.Add(bulllet);
    }

    void ReleaseCreator(PendingCreator pendingCreatorData)
    {
        Creator clone = Instantiate<Creator>(m_creator);
        clone.SetCreatorData(pendingCreatorData.m_creatorData, pendingCreatorData.m_direction);
        m_creatorList.Add(clone);
    }

    void LaunchPattern(Data.Pattern pattern)
    {
        if (m_creatorsData == null ||  m_creatorsData.Count == 0)
        {
            Debug.Log("Dont have creators Data!");
            return;
        }

        Debug.Log("Launch pattern ID " + pattern.m_ID + " "  + pattern.m_creators.Count);
        foreach(Data.CreatorInstance crtInstance in pattern.m_creators)
        {
            Data.Creator creatorData = null;
            foreach(Data.Creator crtDT in m_creatorsData)
            {
                if (crtInstance.m_ID.Equals(crtDT.m_ID))
                {
                    creatorData = crtDT;
                    break;
                }
            }

            if (creatorData == null)
            {
                Debug.Log("Creator ID " + crtInstance.m_ID + " doesn't have data!");
                break;
            }

            PendingCreator pendingCreator = new PendingCreator(m_totalTimePassed + crtInstance.m_spawnTime, creatorData, crtInstance.m_direction);
            m_pendingCreator.Add(pendingCreator);
            Debug.Log("Creator ID " + creatorData.m_ID + " pending at " + m_totalTimePassed + " with release Time : " + m_totalTimePassed + crtInstance.m_spawnTime);
        }
    }
}
