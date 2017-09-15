using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

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
                    //Debug.Log("string " + line);
                    data.Add(line);
                }
                while (line != null);
            }

            foreach(string dataline in data)
            {
                Debug.Log("data " + dataline);
            }

            streamReader.Close();
            return data;
        }
        catch (System.Exception e)
        {
            Debug.Log("Data load exception " + e);
            return null;
        }
    }
}
