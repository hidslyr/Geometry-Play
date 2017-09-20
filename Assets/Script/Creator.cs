using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {
    float m_moveSpeed = 1;
    int m_direction = 1;
    Data.Creator m_data;
    List<Data.ObstacleInstance> m_obstacleList;
    float m_totalTimePassed = 0;

	void Start () {
        Vector3 startPosition = new Vector3(Triangle.m_instance.m_position.x, Triangle.m_instance.m_position.y + Utils.GetScreenWidthAtWS(), Triangle.m_instance.m_position.z + 1.1f);

        transform.position = startPosition;
        m_moveSpeed = 360 / m_data.m_speed;
    }
	
	// Update is called once per frame
	void Update () {
        m_totalTimePassed += Time.deltaTime;

        transform.position = Utils.Rotate(transform.position, new Vector3(0,0,m_moveSpeed * Time.deltaTime * m_direction), Triangle.m_instance.m_position);

        if (UnityEngine.Input.GetKeyDown(KeyCode.N))
        {
            //CreatorManager.m_instance.ReleaseBullet(transform.position, 1, false);
        }
        
        for (int i = 0; i < m_obstacleList.Count; i++)
        {
            if (m_totalTimePassed > m_obstacleList[i].m_timeSpawn)
            {
                CreatorManager.m_instance.ReleaseBullet(transform.position, m_obstacleList[i]);
                Debug.Log("Creator ID " + m_data.m_ID + " release bullet ID " + m_obstacleList[i].m_obstacleID + " at " + m_obstacleList[i].m_timeSpawn);

                m_obstacleList.RemoveAt(i);
                i--;
            }
        }
    }



    public void SetCreatorData(Data.Creator data, int direction)
    {
        m_data = data;
        m_obstacleList = new List<Data.ObstacleInstance>(m_data.m_Obstacles);

        m_direction = direction;
    }
}
