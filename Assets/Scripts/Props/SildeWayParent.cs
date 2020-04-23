using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SildeWayParent : MonoBehaviour
{
    //每个间隔的时间
    public int[] m_Time;
    //最后退出时超过的距离
    private float overDistance = 0;
    public bool isReturn;
    // Update is called once per frame
    private bool Direction;
    private Transform[] m_Child;
    public float endForce = 0;
    /*private void Awake()
    {
        int number = transform.childCount;
        m_Time = new int[number];
    }*/
    private void Start()
    {
        Direction = true;
        m_Child = GetComponentsInChildren<Transform>();
        foreach (Transform child in m_Child)
        {
            //Debug.Log(child.name);
            if (transform == child)
                continue;
            else
            {
                if (child.gameObject.GetComponent<SildeWayChild>() == null)
                    child.gameObject.AddComponent<SildeWayChild>();
            }

        }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    public int GetTime(int i)
    {
        return m_Time[i];
    }
    public float GetoverDistance()
    {
        return overDistance;
    }
    public void SetDirection(bool i)
    {
        Direction = i;
    }
    public bool GetDirection()
    {
        return Direction;
    }
    public bool GetReturn()
    {
        return isReturn;
    }
    public float GetendForce()
    {
        return endForce;
    }
}
