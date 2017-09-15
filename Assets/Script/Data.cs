using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public struct Pattern
    {
        string m_ID;
        float m_duration;
        List<Dictionary<string, Creator>> m_creators;
    }

    public struct Creator
    {
        string m_ID;
        float m_speed;
        List<Dictionary<string, Obstacle>> m_Obstacles;
    }

    public struct ObstacleInstance
    {
        Obstacle m_ObstaclePattern;
        float m_timeSpawn;
        float m_direction;
        float m_speed;
        float m_acceleration;
        float m_size;
        float m_deviation;
    }

    public struct Obstacle
    {
        string m_ID;
        string m_trajectory;
    }
}
