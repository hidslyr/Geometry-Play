using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class DataLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static List<string> Load(string fileName)
    {
        try
        {
            string line;
            List<string> data = new List<string>();
            StreamReader streamReader = new StreamReader(fileName, Encoding.Default);

            using (streamReader)
            {
                do
                {
                    line = streamReader.ReadLine();
                    if (line != null)
                    {
                        data.Add(line);
                    }        
                }
                while (line != null);
            }

            Debug.Log(fileName + " loaded");
            streamReader.Close();
            return data;
        }
        catch (System.Exception e)
        {
            Debug.Log("Data load exception " + e);
            return null;
        }
    }

    public static List<Data.Pattern> LoadPatterns(List<string> rawData)
    {
        List<Data.Pattern> ret = new List<Data.Pattern>();

        rawData.RemoveAt(0);

        foreach(string line in rawData)
        {
            //Debug.Log("pattern parsing " + line);
            string[] parsedline = line.Split(',');
            if (parsedline.Length != 5)
            {
                Debug.Log("corrupted line " + line + " " + parsedline.Length);
                continue;
            }

            string patternID = parsedline[0];
            bool isExisted = false;

            Data.Pattern parsedPattern = null;
            foreach(Data.Pattern pattern in ret)
            {
                if (pattern.m_ID.Equals(patternID))
                {
                    parsedPattern = pattern;
                    isExisted = true;
                    break;
                }
            }

            if (parsedPattern == null)
            {
                parsedPattern = new Data.Pattern();
                parsedPattern.m_ID = parsedline[0];
            }

            if (parsedPattern.m_creators == null)
            {
                parsedPattern.m_creators = new List<Data.CreatorInstance>();
            }
                   
            parsedPattern.m_duration = Int32.Parse(parsedline[1]);

            string creatorId = parsedline[2];
            int spawnTime = Int32.Parse(parsedline[4]);
            int direction = Int32.Parse(parsedline[3]) == 0 ? 1 : -1;

            parsedPattern.m_creators.Add(new Data.CreatorInstance(creatorId, spawnTime, direction));

            if (!isExisted)
            {
                ret.Add(parsedPattern);
            }
        }


        return ret;
    }

    public static List<Data.Obstacle> LoadObstacles(List<string> rawData)
    {
        List<Data.Obstacle> ret = new List<Data.Obstacle>();

        rawData.RemoveAt(0);

        foreach (string line in rawData)
        {
            Debug.Log("line " + line);
            string[] parsedline = line.Split(',');
            if (parsedline.Length != 3)
            {
                Debug.Log("corrupted line " + line + " " + parsedline.Length);
                continue;
            }

            Data.Obstacle parsedObstacle = new Data.Obstacle();


            parsedObstacle.m_ID = parsedline[0];
            parsedObstacle.m_trajectory = parsedline[2];

            ret.Add(parsedObstacle);
        }

        return ret;
    }

    public static List<Data.Creator> LoadCreators(List<string> rawData)
    {
        List<Data.Creator> ret = new List<Data.Creator>();

        rawData.RemoveAt(0);

        foreach (string line in rawData)
        {
            Debug.Log("line " + line);
            string[] parsedline = line.Split(',');
            if (parsedline.Length != 9)
            {
                Debug.Log("corrupted line " + line + " " + parsedline.Length);
                continue;
            }


            string creatorID = parsedline[0];

            Data.Creator parsedCreator = null;
            bool isExisted = false;

            foreach (Data.Creator creator in ret)
            {
                if (creator.m_ID.Equals(creatorID))
                {
                    parsedCreator = creator;
                    isExisted = true;
                    break;
                }
            }

            if (parsedCreator == null)
            {
                parsedCreator = new Data.Creator();
                parsedCreator.m_ID = parsedline[0];
            }

            
            parsedCreator.m_speed = Int32.Parse(parsedline[1]);

            Data.ObstacleInstance newInstance = new Data.ObstacleInstance();
            newInstance.m_obstacleID = parsedline[2];
            newInstance.m_timeSpawn = Int32.Parse(parsedline[3]);
            newInstance.m_direction = Int32.Parse(parsedline[4]);
            newInstance.m_speed = Int32.Parse(parsedline[5]);
            newInstance.m_acceleration = Int32.Parse(parsedline[6]);
            newInstance.m_size = Int32.Parse(parsedline[7]);
            newInstance.m_deviation = Int32.Parse(parsedline[8]);

            if (parsedCreator.m_Obstacles == null)
            {
                parsedCreator.m_Obstacles = new List<Data.ObstacleInstance>();
            }

            parsedCreator.m_Obstacles.Add(newInstance);

            if (!isExisted)
            {
                ret.Add(parsedCreator);
            }
            
        }

        return ret;
    }
}
