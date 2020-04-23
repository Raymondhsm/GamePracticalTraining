 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPLife : MonoBehaviour
{
    private int timer;//计数器
    private bool _isOn;//生命开关
    private float m_gravity;
    private Vector3 m_Pos1;//第一个关卡点
    private Vector3 m_Pos2;//第二个关卡点
    private Vector3 m_Pos3;//第三个关卡点
    private Vector3 m_Pos4;//第四个关卡点
    private Vector3 m_Pos5;//第五个关卡点
    private AudioController m_audios;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        _isOn = false;
        m_Pos1 = new Vector3(106.06f, 103.3f, -222.65f);
        m_Pos2 = new Vector3(145.513f, 236.34f, -104.0f);
        m_Pos3 = new Vector3(146.59f, 305.62f, -32.27f);
        m_Pos4 = new Vector3(7.2f, 280.99f, 79.25f);
        m_Pos5 = new Vector3(-260f, 386.2f, 124.1f);
        m_gravity = -9.81f;
        HighGravity();
        m_audios = GetComponent<AudioController>();
    }


    // Update is called once per frame
    void Update()
    {

        //调试
        if (Input.GetKey(KeyCode.F1))
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos1;
        if (Input.GetKey(KeyCode.F2))
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos2;
        if (Input.GetKey(KeyCode.F3))
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos3;
        if (Input.GetKey(KeyCode.F4))
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos4;
        if (Input.GetKey(KeyCode.F5))
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos5;
    }

    public void HighGravity()
    {
        m_gravity = -35.0f;
        Physics.gravity = new Vector3(0, m_gravity, 0);
    }

    public void LowGravity()
    {
        m_gravity = -9.81f;
        Physics.gravity = new Vector3(0, m_gravity, 0);
    }

    /// <summary>
    /// 判断m_FPP目前处于关卡的哪个地段
    /// </summary>
    /// <param name="collision"></param>
     void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Light")
        {
            timer++;
            Debug.Log("timer : " + timer);
        }
        if (collision.gameObject.tag == "LifeRock")
        {
            _isOn = true;
            Debug.Log("Life is on!");
        }
        //第一个关卡点：回到起点
        if(_isOn&&timer==0&&collision.gameObject.tag=="Terrain")
        {
            Debug.Log("回到起点");
            m_audios._CloseLoop();
            m_audios.DeadSound();
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos1;
            _isOn = false;
        }
        //第二个关卡点，回到第二个光柱处
        if( (timer==2&&collision.gameObject.tag=="Terrain" )
          ||(timer==2&&collision.gameObject.tag=="Mountain"))
        {
            Debug.Log("触发第二个关卡点");
            m_audios._CloseLoop();
            m_audios.DeadSound();
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos2;
        }
        //第三个关卡点，回到第三个光柱处
        if ((timer == 3 && collision.gameObject.tag == "Terrain")
          ||(timer == 3 && collision.gameObject.tag == "Mountain"))
        {
            Debug.Log("触发第三个关卡点");
            m_audios._CloseLoop();
            m_audios.DeadSound();
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos3;
        }
        //第四个关卡点，回到第四个光柱
        if (timer==4)
        {
            if (collision.gameObject.tag == "Terrain" ||
                collision.gameObject.tag == "Mountain" ||
                collision.gameObject.tag == "LifeRock")
            {
                Debug.Log("触发第四个关卡点");
                m_audios._CloseLoop();
                m_audios.DeadSound();
                this.gameObject.GetComponent<Rigidbody>().position = m_Pos4;
            }
        }
        //第五个关卡点，回到第五个光柱
        if(timer==5&&collision.gameObject.tag=="LifeRock")
        {
            Debug.Log("触发第五个关卡点");
            m_audios._CloseLoop();
            m_audios.DeadSound();
            this.gameObject.GetComponent<Rigidbody>().position = m_Pos5;
        }
    }
}
