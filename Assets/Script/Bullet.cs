using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public Vector3 m_velocity = new Vector3(0,0,0);

    BoxCollider m_boxCollider;
	void Start () {
        m_boxCollider = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 changes = m_velocity * Time.deltaTime;
        this.transform.position += changes;
    }
}
