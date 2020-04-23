using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseRock : MonoBehaviour
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
        m_Distance = 0;
        m_MoveDirection = GetComponent<Transform>().up;
        gameObject.AddComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (x)
        {
            m_MoveDirection = GetComponent<Transform>().right;
            RiseX();
        }
        if (y)
        {
            m_MoveDirection = GetComponent<Transform>().up;
            RiseY();
        }
        if (z)
        {
            m_MoveDirection = GetComponent<Transform>().forward;
            RiseZ();
        }
    }

    private void RiseX()
    {
        if (Distance > 0)
        {//超过最大距离反向
            if (GetComponent<Transform>().position.x >= m_position.x + Distance * m_MoveDirection.x)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.x <= m_position.x)
                m_Direction = 1;
        }
        else
        {
            if (GetComponent<Transform>().position.x <= m_position.x + Distance * m_MoveDirection.x)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.x >= m_position.x)
                m_Direction = 1;
        }
        GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        if (m_Collider != null)
            m_Collider.GetComponent<Transform>().Translate(m_Distance / NumberofFrames * m_MoveDirection * m_Direction, Space.World);
        //Debug.Log(m_Distance / NumberofFrames * m_MoveDirection * m_Direction);
    }

    private void RiseY()
    {
        if (Distance > 0)
        //超过最大距离反向
        {
            if (GetComponent<Transform>().position.y >= m_position.y + Distance * m_MoveDirection.y)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.y <= m_position.y)
                m_Direction = 1;
        }
        else
        {
            if (GetComponent<Transform>().position.y <= m_position.y + Distance * m_MoveDirection.y)
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
            if (GetComponent<Transform>().position.z >= m_position.z + Distance * m_MoveDirection.z)
                m_Direction = -1;
            //小于最小距离反向
            if (GetComponent<Transform>().position.z <= m_position.z)
                m_Direction = 1;
        }
        else
        {
            if (GetComponent<Transform>().position.z <= m_position.z + Distance * m_MoveDirection.z)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_Distance = Distance;
            m_Collider = other;
        }
        //Debug.Log(GetComponent<Transform>().position.x + "  " + m_position.x + " " + Distance +" "+ GetComponent<Transform>().right.x);    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_Distance = 0;
            m_Collider = null;
        }
    }

    public void Reset()
    {
        transform.position = m_position;
    }
}
