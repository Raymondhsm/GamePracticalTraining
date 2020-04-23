using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SildeWayChild : MonoBehaviour
{
    //[Tooltip("Destination"), SerializeField]
    private GameObject m_nextObject;
    //[Tooltip("结束时会跃出多少距离"), SerializeField]
    private float overDistance;
    [Tooltip("多少帧会达到下一个平面"), SerializeField]
    private float m_Time;
    private float m_i = 0;
    private float m_Correction;
    private Collider m_Collider;
    private Vector3 m_Displacement;
    private GameObject m_Parent;
    public Transform[] SildeWayDirection;
    private Vector3 m_Speed;
    private Vector3 m_NormalVector;
    private float m_j = 0;
    private Vector3 m_SpeedZero;
    private float endForce;
    // Start is called before the first frame update
    void Start()
    {

        m_Speed = new Vector3(0, 0, 0);
        m_Parent = this.gameObject.transform.parent.gameObject;
        SildeWayDirection = m_Parent.GetComponentsInChildren<Transform>();
        m_NormalVector = GetComponent<Transform>().right;
        foreach (Transform child in SildeWayDirection)
        {
            //Debug.Log(child.name);
            if (child == GetComponent<Transform>())
                break;
            else
                m_j++;
        }
        m_Time = m_Parent.GetComponent<SildeWayParent>().GetTime((int)m_j);
        m_i = m_Time + overDistance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (m_Collider == null)
            return;

        if (m_i == m_Time+1)
        {
            if (m_Collider != null)
            {
                //if (m_j == 2 && m_Parent.GetComponent<SildeWayParent>().GetDirection() == false)
                    m_Collider.GetComponent<Rigidbody>().isKinematic = false;
                //if (m_j == SildeWayDirection.Length - 2 && m_Parent.GetComponent<SildeWayParent>().GetDirection() == true)
                   //m_Collider.GetComponent<Rigidbody>().isKinematic = false;
            }
            //Debug.Log(m_Collider.GetComponent<Rigidbody>().isKinematic + "111 " + m_Parent.GetComponent<SildeWayParent>().GetDirection());
            //m_Collider.GetComponent<FPPController>()._addForce(m_Displacement.normalized*endForce*1000);
            m_Collider.GetComponent<Rigidbody>().AddForce(m_Displacement.normalized * endForce * 1000);
            //Debug.Log("force" + m_Displacement.normalized + "  " + endForce);
            endForce = 0;
            m_Collider = null;
            //SildeWayDirection[1].GetComponent<SildeWayChild>().enabled = true;
            //SildeWayDirection[SildeWayDirection.Length - 1].GetComponent<SildeWayChild>().enabled = true;
            //Debug.Log("time" + time);
            return;
        }

        //Debug.Log("displacement.x="+displacement.x+"  time " +i);
        if (m_Collider != null)
            m_Collider.GetComponent<Transform>().Translate(m_Displacement, Space.World);

        m_i++;
        /*if(m_i == m_Time)
        {
            if (m_Collider != null)
            {
                if (m_j == 2 && m_Parent.GetComponent<SildeWayParent>().GetDirection() == false)
                    m_Collider.GetComponent<Rigidbody>().isKinematic = false;
                if (m_j == SildeWayDirection.Length - 2 && m_Parent.GetComponent<SildeWayParent>().GetDirection() == true)
                    m_Collider.GetComponent<Rigidbody>().isKinematic = false;
            }
        }*/
    }

    void OnTriggerEnter(Collider collider)
    {
        m_Speed = collider.GetComponent<Rigidbody>().velocity;
        //Debug.Log(m_j+" "+SildeWayDirection.Length);
        /*if (m_j == SildeWayDirection.Length-1 && m_Speed != m_SpeedZero)
        {
            m_Parent.GetComponent<SildeWayParent>().SetDirection(false);
            //Debug.Log("反向");
        }
        if (m_j == 1 && m_Speed != m_SpeedZero)
        {
            m_Parent.GetComponent<SildeWayParent>().SetDirection(true);
            //Debug.Log("正向");
        }*/
        if (Vector3.Dot(transform.position - collider.GetComponent<Transform>().position, transform.up) > 0 && m_Speed != Vector3.zero && m_j==1)
            m_Parent.GetComponent<SildeWayParent>().SetDirection(true);
        if (Vector3.Dot(transform.position - collider.GetComponent<Transform>().position, transform.up) < 0 && m_Speed != Vector3.zero&&m_j==SildeWayDirection.Length-1)
            m_Parent.GetComponent<SildeWayParent>().SetDirection(false);
        //Debug.Log(transform.position - collider.GetComponent<Transform>().position +" "+ transform.up);
        if (m_Parent.GetComponent<SildeWayParent>().GetDirection())
        {
            if(m_j== SildeWayDirection.Length - 1)
            {
                m_i = m_Time + 1;
                endForce = 0;
                return;
            }
            //SildeWayDirection[SildeWayDirection.Length - 1].GetComponent<SildeWayChild>().enabled = false;
            collider.GetComponent<Rigidbody>().isKinematic = true;
            /*if (m_j == 1)
                collider.GetComponent<Rigidbody>().isKinematic = true;*/
            //Debug.Log(m_j);
            if (m_j == SildeWayDirection.Length - 2)
            {
                endForce = m_Parent.GetComponent<SildeWayParent>().GetendForce();
                //Debug.Log(m_Displacement.normalized * endForce + "force1");
            }
            else
                endForce = 0;
            //Debug.Log(m_Displacement.normalized * endForce + "force2");
            if (m_j >= SildeWayDirection.Length - 1)
            {
                m_i = m_Time;
                return;
            }
            else
                m_nextObject = SildeWayDirection[(int)m_j + 1].gameObject;
            if (m_j == SildeWayDirection.Length - 2)
            {
                overDistance = m_Parent.GetComponent<SildeWayParent>().GetoverDistance();
            }
            else
            {
                overDistance = 0;
            }
            //Debug.Log(m_nextObject.name+"time"+m_Time+"m_i"+m_i);
        }
        else
        {
            //SildeWayDirection[1].GetComponent<SildeWayChild>().enabled = false;
            if (m_j == 1)
            {
                m_i = m_Time + 1;
                endForce = 0;
                return;
            }
            collider.GetComponent<Rigidbody>().isKinematic = true;
            /*if (m_j == SildeWayDirection.Length - 1)
                collider.GetComponent<Rigidbody>().isKinematic = true;*/
            if (m_j == 2)
            {
                endForce = m_Parent.GetComponent<SildeWayParent>().GetendForce();
                //Debug.Log("m_Dis" + m_Displacement);
            }
            else
                endForce = 0;
            //Debug.Log(m_j);
            if (m_j <= 1)
            {
                m_i = m_Time;
                return;
            }
            else
                m_nextObject = SildeWayDirection[(int)m_j - 1].gameObject;
            if (m_j == 2)
            {
                overDistance = m_Parent.GetComponent<SildeWayParent>().GetoverDistance();
            }
            else
            {
                overDistance = 0;
            }
            //Debug.Log(m_nextObject.name);
            if (!m_Parent.GetComponent<SildeWayParent>().GetReturn())
            {
                m_i = m_Time;
                collider.GetComponent<Rigidbody>().isKinematic = false;
                return;
            }
        }
        //Debug.Log(m_nextObject.name);
        var position = GetComponent<Transform>().position;
        var destination = m_nextObject.transform.position;
        m_Displacement = destination - position;
        //Debug.Log(destination);
        m_Displacement = m_Displacement.normalized * overDistance + m_Displacement;
        //m_Displacement = m_Displacement.normalized * 0.9f + m_Displacement;
        m_Displacement.x = m_Displacement.x / m_Time;
        m_Displacement.y = m_Displacement.y / m_Time;
        m_Displacement.z = m_Displacement.z / m_Time;
        m_Collider = collider;
        m_i = 0;
        //collider.GetComponent<Transform>().Translate(displacement);
    }
}
