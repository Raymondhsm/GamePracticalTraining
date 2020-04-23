using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  m_target 放置触发的结果
/// </summary>
public class CheckPointController : MonoBehaviour
{
    private AudioSource m_Audios;      //音频源
    public AudioClip CheckPoint_Audio;

    public GameObject[] CheckPoints;//下一个/组要显示的关卡点
    private Transform[] m_Child;//用来暂存的

    void Start()
    {
        m_Audios = GetComponent<AudioSource>();
        m_Audios.clip = CheckPoint_Audio;

        for (int i=0;i< CheckPoints.Length;i++)
        {
            CheckPoints[i].GetComponent<MeshRenderer>().enabled = false; 
            CheckPoints[i].GetComponent<Collider>().enabled = false;

            //对子物体遍历
            m_Child = CheckPoints[i].GetComponentsInChildren<Transform>();
            foreach (Transform child in m_Child)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = false;
                if(child.gameObject.GetComponent<Collider>())
                child.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            m_Audios.PlayOneShot(CheckPoint_Audio);

            //对自己
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;

            //对子物体遍历
            m_Child = this.GetComponentsInChildren<Transform>();
            foreach (Transform child in m_Child)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            //Destroy(gameObject);


            //对下一个/组要显示的关卡点
            for (int i = 0; i < CheckPoints.Length; i++)
            {
                CheckPoints[i].GetComponent<MeshRenderer>().enabled = true;
                CheckPoints[i].GetComponent<Collider>().enabled = true;

                //对子物体遍历
                m_Child = CheckPoints[i].GetComponentsInChildren<Transform>();
                foreach (Transform child in m_Child)
                {
                    child.gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
