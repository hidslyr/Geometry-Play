using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {
    float m_moveSpeed = 1;

	void Start () {
        Debug.Log("creator start ");
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Utils.Rotate(transform.position, new Vector3(0,0,m_moveSpeed), Triangle.m_instance.m_position);

        if (UnityEngine.Input.GetKeyDown(KeyCode.N))
        {
            CreatorManager.m_instance.ReleaseBullet(transform.position, 1, false);
        }
    }
}
