using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public static test m_instance;
	// Use this for initialization
	void Awake () {
        m_instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
