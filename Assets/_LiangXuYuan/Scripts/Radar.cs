using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    [Header("跟踪目标")]
    [Tooltip("玩家对象")]
    public GameObject player;
    [Tooltip("跟踪的目标")]
    public GameObject target;

    [Header("跟踪器属性")]
    [Tooltip("显示/隐藏跟踪器")]
    public bool is_showRadar = true;
    [Tooltip("显示/隐藏红点")]
    public bool is_showTarget = true;
    [Tooltip("目标红点“乱飞”")]
    public bool is_random = false;
    [Tooltip("跟踪器指针旋转的速度")]
    public float rotateSpeed = 100f;
    [Tooltip("跟踪器的扫描范围（半径）")]
    public float scanRadius = 30f;
    [Tooltip("随机目标刷新间隔时间")]
    public float randomTargetRefreshTime = 0.2f;

    // 跟踪器的背景、指针、红点
    private GameObject m_background;
    private GameObject m_pointer;
    private GameObject m_point;

    // 保存一个随机位置
    private Vector3 m_randomDirection = Vector3.zero;
    // 标记是否开启了协程
    private bool is_startedCoroutine = false;

    void Start()
    {
        m_background = GameObject.Find("RadarBase");
        m_pointer = GameObject.Find("RadarPointer");
        m_point = GameObject.Find("RadarPoint");
    }

    void Update()
    {
        // 显示隐藏雷达
        if(is_showRadar)
        {
            m_background.SetActive(true);
            m_pointer.SetActive(true);
            m_point.SetActive(is_showTarget ? true : false); // 显示隐藏红点
        }
        else
        {
            m_background.SetActive(false);
            m_pointer.SetActive(false);
            m_point.SetActive(false);
        }
    }

    /// <summary>
    /// 绘制雷达跟踪器
    /// 状态1：显示雷达，指针旋转，不显示目标
    /// 状态2：正常显示雷达和目标
    /// 状态3：显示雷达，但目标“乱飞”
    /// </summary>
    void OnGUI()
    {
        if(is_showRadar)
        {
            // 指针旋转
            m_pointer.GetComponent<RectTransform>().Rotate(new Vector3(0f, 0f, 1f) * -rotateSpeed * Time.deltaTime);

            // 显示目标
            if(is_showTarget)
            {
                if(!is_random)
                {
                    // 这段代码为正常显示目标状态

                    // 1排除异常
                    if (player == null || target == null)
                    {
                        Debug.LogError("雷达错误：玩家或目标为空");
                        return;
                    }
                    
                    // 2更新目标红点的位置
                    Vector3 direction = target.transform.position - player.transform.position; // 世界坐标系下，从玩家指向目标物体的向量
                    direction = player.transform.InverseTransformDirection(direction); // 将世界坐标系转换成玩家的局部坐标系
                    direction = direction / scanRadius * m_background.GetComponent<RectTransform>().rect.width / 2; //将向量的长度标准化，使之符合跟踪器扫描范围
                    float radius = m_background.GetComponent<RectTransform>().rect.width / 2 * 0.95f; // 获取UI界面上跟踪器的宽度
                    if (direction.sqrMagnitude > radius * radius) // 限制红点的范围，不超出跟踪器背景
                    {
                        direction = direction.normalized * radius;
                    }

                    m_point.GetComponent<RectTransform>().localPosition = new Vector3(direction.x, direction.z, 0f); // 设置红点的位置
                }
                else
                {
                    // 这段代码为目标红点“乱飞”状态

                    // 开启协程（单次调用），用来修改随机目标坐标
                    if (!is_startedCoroutine)
                    {
                        StartCoroutine(ChangeRandomDirection());
                        is_startedCoroutine = true;
                    }
                    m_point.GetComponent<RectTransform>().localPosition = m_randomDirection; // 设置红点的位置
                }
                
            }
        }   
    }

    /// <summary>
    /// 外部调用，隐藏雷达
    /// </summary>
    public void HideRadar()
    {
        is_showRadar = false;
    }

    /// <summary>
    /// 外部调用，显示雷达，但不显示目标红点
    /// </summary>
    public void ShowNoTarget()
    {
        is_showRadar = true;
        is_showTarget = false;
    }

    /// <summary>
    /// 外部调用，显示雷达，显示随机目标
    /// </summary>
    public void ShowRandom()
    {
        is_showRadar = true;
        is_showTarget = true;
        is_random = true;
    }

    /// <summary>
    /// 外部调用，显示目标，使用当前目标
    /// </summary>
    public void ShowTarget()
    {
        is_showRadar = true;
        is_showTarget = true;
        is_random = false;
    }

    /// <summary>
    /// 外部调用，显示目标，使用参数的目标
    /// </summary>
    /// <param name="target">目标GameObject</param>
    public void ShowTarget(GameObject target)
    {
        is_showRadar = true;
        is_showTarget = true;
        is_random = false;
        this.target = target;
    }

    /// <summary>
    /// 协程函数，定期更改随机目标的坐标
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeRandomDirection()
    {
        while(is_random)
        {
            float angle = Random.Range(0f, 2 * Mathf.PI); // 随机角度
            float length = Random.Range(0f, m_background.GetComponent<RectTransform>().rect.width / 2 * 0.95f); // 随机距离
            m_randomDirection = new Vector3(length * Mathf.Cos(angle), length * Mathf.Sin(angle), 0f); // 圆形表盘上随机位置，更新m_randomDirection
            yield return new WaitForSeconds(randomTargetRefreshTime);
        }
        is_startedCoroutine = false;
        yield break;
    }

    /// <summary>
    /// 调用样例（注释掉）
    /// </summary>
    //public void sample()
    //{
    //    Radar radar = GetComponent<Radar>();
    //    radar.HideRadar();
    //    radar.ShowNoTarget();
    //    radar.ShowRandom();
    //    radar.ShowTarget();
    //    radar.ShowTarget(gameObject);
    //}
}
