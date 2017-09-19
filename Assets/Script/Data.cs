using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public class Pattern
    {
        public string m_ID;
        public float m_duration;
        public List<Creator> m_creators;
    }

    public class Creator
    {
        public string m_ID;
        public float m_speed;
        public List<ObstacleInstance> m_Obstacles = null;
    }

    public class ObstacleInstance
    {
        public string m_obstacleID;
        public int m_timeSpawn;
        public int m_direction;
        public int m_speed;
        public int m_acceleration;
        public int m_size;
        public int m_deviation;

        public string GetDebugInfo()
        {
            return m_obstacleID + " " + m_timeSpawn + " " + m_direction + " " + m_speed + " " + m_acceleration + " " + m_size + " " + m_deviation;
        }
    }

    public class Obstacle
    {
        public string m_ID;
        public string m_trajectory;
    }
}
