using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DiskEventMainController))]
public class MakeCameraLookAtDiskController : MonoBehaviour
{
    [Header("玩家")]

    [Tooltip("玩家GameObject")]
    public GameObject playerGameObject;

    [Tooltip("玩家相机")]
    public Camera playerCamera;


    [Header("设置")]

    [Tooltip("视角转动时间")]
    public float rotateTime = 3f;

    
    private DiskEventMainController diskEventMainController;//总控制脚本，用于返回转动相机动作完毕的消息

    private bool is_rotating = false; // 标记正在转动视角
    private bool is_firstFrame = true; // 标记第一帧旋转

    private float m_angleY; // Y轴胶囊体旋转角度
    private float m_angleX; // X轴玩家相机旋转角度

    private float rotateSpeedY; // 计算出来的Y轴旋转速度
    private float rotateSpeedX; // 计算出来的X轴旋转速度

    private Vector3 savePlayerLocalEulerAngles; // 保存当前算出来的玩家旋转角度，用于固定玩家视角
    private Vector3 saveCameraLocalEulerAngles; // 保存当前算出来的相机旋转角度，用于固定玩家视角

    private void Start()
    {
        // 初始化私有变量
        diskEventMainController = GetComponent<DiskEventMainController>();
    }

    private void Update()
    {
        if (is_rotating)
        {
            // 分别旋转胶囊体和相机

            // 0.为防止玩家乱动鼠标，先设置当前旋转
            if(!is_firstFrame)
            {
                playerGameObject.transform.localEulerAngles = savePlayerLocalEulerAngles;
                playerCamera.transform.localEulerAngles = saveCameraLocalEulerAngles;
            }

            // 1.旋转胶囊体(Y轴)

            // 获取旋转轴
            Vector3 normalizeY = Vector3.Cross(playerGameObject.transform.forward, new Vector3(transform.position.x, playerGameObject.transform.position.y,transform.position.z) - playerGameObject.transform.position);
            m_angleY = Vector3.Angle(-playerGameObject.transform.forward, playerGameObject.transform.position - new Vector3(transform.position.x,playerGameObject.transform.position.y,transform.position.z));

            if(is_firstFrame)
            {
                rotateSpeedY = m_angleY / rotateTime; //Debug.Log("rotateSpeedY: " + rotateSpeedY);
            }

            if(m_angleY > 1f)
            {
                // 胶囊体按旋转轴转动
                playerGameObject.transform.Rotate(normalizeY, Time.deltaTime * rotateSpeedY, Space.World);
            }
            



            // 2.旋转相机（X轴）

            // 暂存胶囊体正对磁盘的状态
            Vector3 tempLocalEulerAngles = playerGameObject.transform.localEulerAngles; // 保存胶囊体当前旋转，在调整结束后恢复
            playerGameObject.transform.Rotate(normalizeY, m_angleY, Space.Self);        // 将胶囊体正对磁盘，方便计算相机角度

            // 计算相机旋转轴
            float dotValue = Vector3.Dot(playerCamera.transform.up, transform.position - playerCamera.transform.position); // 点乘小于0时disk在下面
            Vector3 normalizeX = dotValue > 0 ? -playerCamera.transform.right : playerCamera.transform.right;
            m_angleX = Vector3.Angle(-playerCamera.transform.forward, playerCamera.transform.position - transform.position);

            if(is_firstFrame)
            {
                rotateSpeedX = m_angleX / rotateTime; //Debug.Log("rotateSpeedX: " + rotateSpeedX); 
                is_firstFrame = false;
            }

            if(m_angleX > 1f)
            {
                // 玩家相机按旋转轴转动
                playerCamera.transform.Rotate(normalizeX, Time.deltaTime * rotateSpeedX, Space.World);
            }

            // 恢复胶囊体旋转
            playerGameObject.transform.localEulerAngles = tempLocalEulerAngles;

            // 3.记录当前旋转状态
            savePlayerLocalEulerAngles = playerGameObject.transform.localEulerAngles;
            saveCameraLocalEulerAngles = playerCamera.transform.localEulerAngles;

            // 转动视角完成
            if (m_angleY < 1f && m_angleX < 1f)
            {
                // 恢复标记
                is_rotating = false;

                // // 发送完成的消息
                //Debug.Log("转动视角完成");
                diskEventMainController.OnRotateCameraEnd();

                // 关闭此脚本
                this.enabled = false;
            }

        }
    }

    private void LateUpdate()
    {
        if(is_rotating)
        {
            playerGameObject.transform.localEulerAngles = savePlayerLocalEulerAngles;
            playerCamera.transform.localEulerAngles = saveCameraLocalEulerAngles;
        }
    }

    public void LookAtDisk()
    {
        is_rotating = true;
    }

    //private void OnGUI()
    //{
    //    GUI.Label(new Rect(100f, 100f, 100f, 50f), "Y轴角度" + m_angleY.ToString());
    //    GUI.Label(new Rect(100f, 150f, 100f, 50f), "X轴角度" + m_angleX.ToString());
    //}
}
