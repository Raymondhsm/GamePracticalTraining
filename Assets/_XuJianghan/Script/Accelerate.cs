using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Accelerate : MonoBehaviour
{
    [Tooltip("加速块提供的动力大小"), SerializeField]
    private float speedUpForce = 20f;
    //[Tooltip("判断m_FPP触碰加速块各个面的精度：越小越不易触发碰撞"), SerializeField]
    //private float accuracy = 0.2f;
    // Start is called before the first frame update
    private AudioSource _audioController;
    // Start is called before the first frame update
    void Start()
    {
        _audioController = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 加速块功能实现：
    /// 1.加速块上表面提供up方向动力
    /// 2.加速块下表面提供down方向动力
    /// 3.加速块四个侧面提供斜向上的动力
    /// </summary>
    /// <param name="other">
    /// other是m_FPP的碰撞器
    /// </param>
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("发生碰撞：-------------------------------------------");

            _audioController.Play();

                other.GetComponent<Rigidbody>().AddForce(GetComponent<Transform>().up * speedUpForce * 10, ForceMode.Impulse);

        }
    }
}

