using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoard : MonoBehaviour
{
    private Vector3 m_position;
    private int m_Direction;
    // Number of frames
    public int NumberofFrames;
    public float Distance;
    private float m_Distance;
    private Collider m_Collider;
    public bool x;
    public bool y;
    public bool z;
    private Vector3 m_MoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        m_position = GetComponent<Transform>().position;
        m_Direction = 1;
        m_Distance = Distance;
        m_MoveDirection = Vector3.zero;
        gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (x)
        {
            m_MoveDirection = GetComponent<Transform>().right;
            //RiseX();
        }
        if (y)
        {
            m_MoveDirection = GetComponent<Transform>().up;
            //RiseY();
        }
        if (z)
        {
            m_MoveDirection = GetComponent<Transform>().forward;
            //RiseZ();
        }
        if (m_MoveDirection != Vector3.zero)
            Rise();
    }

    private void Rise()
    {
        if (Distance > 0)
        {//超过最大距离反向
            if (Vector3.Dot(GetComponent<Transform>().position,m_MoveDirection) >= Vector3.Dot(m_position, m_MoveDirection) + Distance)
                m_Direction = -1;
            //小于最小距离反向
            if (Vector3.Dot(GetComponent<Transform>().position, m_MoveDirection) <= Vector3.Dot(m_position, m_MoveDirection))
                m_Direction = 1;
        }
        else
        {
            if (Vector3.Dot(GetComponent<Transform>().position, m_MoveDirection) <= Vector3.Dot(m_position, m_MoveDirection))
                m_Direction = -1;
            //小于最小距离反向
            if (Vector3.Dot(GetComponent<Transform>().position, m_MoveDirection) >= Vector3.Dot(m_position, m_MoveDirection))
                m_Direction = 1;
        }
        GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        if (m_Collider != null)
            m_Collider.GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        //Debug.Log(m_Distance / NumberofFrames * m_MoveDirection * m_Direction);
    }

    /*private void RiseY()
    {
        if (Distance > 0)
        //超过最大距离反向
        {
            if (GetComponent<Transform>().position.y >= m_position.y + Distance)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.y <= m_position.y)
                m_Direction = 1;
        }
        else
        {
            if (GetComponent<Transform>().position.y <= m_position.y + Distance)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.y >= m_position.y)
                m_Direction = 1;
        }
        GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        if (m_Collider != null)
            m_Collider.GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        //Debug.Log(GetComponent<Transform>().position.y +"  "+ m_position.y + " "+Distance * GetComponent<Transform>().up.y);
    }

    private void RiseZ()
    {
        if (Distance > 0)
        {//超过最大距离反向
            if (GetComponent<Transform>().position.z >= m_position.z + Distance)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.z <= m_position.z)
                m_Direction = 1;
        }
        else
        {
            if (GetComponent<Transform>().position.z <= m_position.z + Distance)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.z >= m_position.z)
                m_Direction = 1;
        }
        GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        if (m_Collider != null)
            m_Collider.GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        //Debug.Log(GetComponent<Transform>().position.y +"  "+ m_position.y + " "+Distance * GetComponent<Transform>().up.y);
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collider");
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(GetComponent<Transform>().forward * 100, ForceMode.VelocityChange);
            //Debug.Log(GetComponent<Transform>().forward * 100);
        }
    }
}

