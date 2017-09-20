using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public class Pattern
    {
        public string m_ID;
        public int m_duration;
        public List<CreatorInstance> m_creators = null;
    }

    public class CreatorInstance
    {
        public CreatorInstance(string ID, int spawnTime, int direction)
        {
            m_ID = ID;
            m_spawnTime = spawnTime;
            m_direction = direction;
        }

        public string m_ID;
        public int m_spawnTime;
        public int m_direction;
    }

    public class Creator
    {
        public string m_ID;
        public float m_speed;
        public List<ObstacleInstance> m_Obstacles = null;
    }

    public class ObstacleInstance
    {
        public ObstacleInstance()
        {

        }

        public ObstacleInstance(string obstacleID, int timeSpawn, int direction, int speed, int acceleration, int size, int deviation)
        {
            m_obstacleID = obstacleID;
            m_timeSpawn = timeSpawn;
            m_direction = direction;
            m_speed = speed;
            m_acceleration = acceleration;
            m_size = size;
            m_deviation = deviation;
        }

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

        ObstacleInstance Clone()
        {
            return new ObstacleInstance(m_obstacleID, m_timeSpawn, m_direction, m_speed, m_acceleration, m_size, m_deviation);
        }
    }

    public class Obstacle
    {
        public string m_ID;
        public string m_trajectory;
    }
}
