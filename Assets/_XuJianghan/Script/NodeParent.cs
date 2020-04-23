using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeParent : MonoBehaviour
{
    private Transform[] m_Child;
    private float i=1;
    public GameObject NextGameObject=null;
    public int Num = 5;
    private bool UpdateRadar = true;
    private bool m_Radar=true;
    // Start is called before the first frame update
    void Start()
    {
        m_Child = GetComponentsInChildren<Transform>();
        //Debug.Log(m_Child.Length);    
        for(int j=1;j<m_Child.Length-1;)
        {
            m_Child[j].gameObject.AddComponent<Node>();
            m_Child[j].gameObject.SetActive(false);
            j = j + Num;
        }
        m_Child[1].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateRadar)
        {
            if (m_Radar)
                GameObject.Find("Radar").GetComponent<Radar>().ShowTarget(m_Child[(int)i].gameObject);
            else
                GameObject.Find("Radar").GetComponent<Radar>().ShowNoTarget();
        }
        else
            return;
    }

    public void NextNode()
    {
        m_Child[(int)i].gameObject.SetActive(false);
        i = i + Num;
        if (i >= m_Child.Length)
        {
            if(NextGameObject!=null)
                GameObject.Find("Radar").GetComponent<Radar>().ShowTarget(NextGameObject);
            else
                GameObject.Find("Radar").GetComponent<Radar>().ShowNoTarget();
            return;
        }
        else
            m_Child[(int)i].gameObject.SetActive(true);
    }

    public void UpdateRadarTrue()
    {
        UpdateRadar = true;
    }

    public void UpdateRadarFalse()
    {
        UpdateRadar = false;
    }

    public void RadarTrue()
    {
        m_Radar = true;
    }

    public void RadarFalse()
    {
        m_Radar = false;
    }
}
