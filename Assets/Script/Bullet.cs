using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public Vector3 m_velocity = new Vector3(0,0,0);

	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 changes = m_velocity * Time.deltaTime;
        this.transform.position += changes;
    }
}
