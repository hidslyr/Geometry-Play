using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width/2, 0));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0));

        float m_screenWidthAtWS = Vector3.Distance(leftEdge, rightEdge);

        Vector3 topEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.height / 2, 0));
        Vector3 bottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, 0));

        float m_screenHeightAtWS = Vector3.Distance(topEdge, bottomEdge);

        //Debug.Log("m_screenWidthAtWS " + m_screenWidthAtWS);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        Vector3 scale = new Vector3(m_screenWidthAtWS / sr.size.x, m_screenHeightAtWS / sr.size.y, 1);

        this.transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
