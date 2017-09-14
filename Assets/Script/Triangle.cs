using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {
    public static Triangle m_instance;
    static float m_lineRange;
    static float m_scale = 0.3f;
    public float m_altitudes;
    public float m_inscribedCircleRadius;
    public Vector3 m_position = Vector3.zero;

    public Bullet m_bullet;
    public List<Bullet> m_bulletList = new List<Bullet>();

    MeshFilter m_meshFilter;
    MeshCollider m_meshCollider;

    // Use this for initialization

    LineRenderer m_line;
    int m_segments = 60;
    float m_radius;

    void Awake()
    {
        m_instance = this;
        Vector3 leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.width / 2, 0));
        Vector3 rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0));
        float screenWidthAtWS = Vector3.Distance(leftEdge, rightEdge);

        Vector3 topEdge = Camera.main.ScreenToWorldPoint(new Vector3(-Screen.height / 2, 0));
        Vector3 bottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.height / 2, 0));

        float m_screenHeightAtWS = Vector3.Distance(topEdge, bottomEdge);


        m_lineRange = m_scale * screenWidthAtWS;
        m_position = new Vector3(0, m_screenHeightAtWS / 6, 0);

        m_meshFilter = GetComponent<MeshFilter>();

        InitTriangle(m_meshFilter.mesh);

        m_meshCollider = GetComponent<MeshCollider>();

        m_meshCollider.sharedMesh = m_meshFilter.mesh;
    }

    void Start () {
        m_line = GetComponent<LineRenderer>();

        m_line.positionCount = m_segments + 1;

        float height = m_altitudes - (m_lineRange * Mathf.Sqrt(3) / 6);
        m_radius = height - m_altitudes;
        CreatePoints();
    }
    
    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate()
    {
        if (UnityEngine.Input.GetMouseButton(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //Debug.Log("mouseDown " + pos);

            if (pos.x < Screen.width / 2)
            {
                //rotate left
                //Debug.Log("rotate left");
                Vector3 center = m_position;
                Vector3[] vertices = m_meshFilter.mesh.vertices;
                Vector3 angle = new Vector3(0, 0, 5);

                m_meshFilter.mesh.vertices = RotateTriangle(vertices, angle, center);
            }
            else
            {
                //rotate right
                //Debug.Log("mouseDown " + pos);

                Vector3 center = m_position;
                Vector3[] vertices = m_meshFilter.mesh.vertices;
                Vector3 angle = new Vector3(0, 0, -5);

                m_meshFilter.mesh.vertices = RotateTriangle(vertices, angle, center);
            }
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.M))
        {
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldpos.z += 0.5f;
            Bullet bulllet = Instantiate<Bullet>(m_bullet, worldpos, new Quaternion(),this.transform);
            bulllet.m_velocity = new Vector3(1, -1, 0);
            m_bulletList.Add(bulllet);
            Debug.Log("create bullet " + worldpos);
        }

        m_meshCollider.sharedMesh = m_meshFilter.mesh;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
    }


    Vector3[] RotateTriangle(Vector3[] vertices, Vector3 angle, Vector3 center)
    {

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = angle;

        for (int i = 0; i < vertices.Length; i++)
        {
            //vertices being the array of vertices of your mesh
            vertices[i] = rotation * (vertices[i] - center) + center;

            Debug.Log("after rotate " + vertices[i]);
        }

        return vertices;
    }

    void InitTriangle(Mesh mesh)
    {
        float height;
        float width;

        m_altitudes = m_lineRange * Mathf.Sin(Mathf.PI / 3);

        Debug.Log("Altitude " + m_altitudes);

        height = m_altitudes - (m_lineRange * Mathf.Sqrt(3) / 6);

        width = m_lineRange / 2;

        m_inscribedCircleRadius = m_altitudes - height;

        Debug.Log("Start Triangle " + width + " " + height + " " + Mathf.Sin(3.14f / 3));

        Vector3[] vertices = new Vector3[3]
        {
            new Vector3(-width,-m_altitudes + height) + m_position,
            new Vector3(0,height) + m_position,
            new Vector3(width,-m_altitudes + height) + m_position
        };

        Debug.Log("Vertices " + vertices[0] + " " + vertices[1] + " " + vertices[2]);

        int[] tri = new int[3];
        tri[0] = 0;
        tri[1] = 1;
        tri[2] = 2;

        Vector3[] norm = new Vector3[3];
        norm[0] = Vector3.forward;
        norm[1] = Vector3.forward;
        norm[2] = Vector3.forward;


        Vector2[] uv = new Vector2[3];
        uv[0] = new Vector2(1, 1);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(0, 0);

        mesh.vertices = vertices;
        mesh.triangles = tri;
        mesh.normals = norm;
        mesh.uv = uv;
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z = m_position.z - 0.1f;

        float angle = 20f;

        for (int i = 0; i < (m_segments + 1); i++)
        {
            x = m_position.x + Mathf.Sin(Mathf.Deg2Rad * angle) * m_radius;
            y = m_position.y + Mathf.Cos(Mathf.Deg2Rad * angle) * m_radius;

            m_line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / m_segments);
        }
    }
}
