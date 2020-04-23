using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ShowTextsController)), RequireComponent(typeof(PickUpDiskController))]
public class DiskEventMainController : MonoBehaviour
{
    public Transform player;
    public Transform camera;

    // 让玩家缓慢看向磁盘的脚本
    MakeCameraLookAtDiskController makeCameraLookAtDiskController;

    // 在GUI上显示文本的脚本
    private ShowTextsController showTextsController;

    // 玩家捡起磁盘动作的脚本
    private PickUpDiskController pickUpDiskController;

    // 当前状态
    private bool is_happened = false;
    private bool is_triggerEnter = false;
    private bool is_rotateCameraEnd = false;
    private bool is_showTextEnd = false;
    private bool is_pickDiskEnd = false;
    private bool is_playerCanMove = true;
    private bool is_playerCanRotateCamera = true;

    // 固定玩家状态
    private Vector3 playerStopPosition;
    private Vector3 playerStopLocalEulerAngles;
    private Vector3 cameraStopLocalEulerAngles;

    void Start()
    {
        // 初始化私有变量
        makeCameraLookAtDiskController = GetComponent<MakeCameraLookAtDiskController>();
        showTextsController = GetComponent<ShowTextsController>();
        pickUpDiskController = GetComponent<PickUpDiskController>();
    }

    /// <summary>
    /// 根据当前状态（到哪一步），继续执行过程
    /// </summary>
    void Update()
    {
        // 1.转动相机
        if(is_happened && is_triggerEnter)
        {
            // 设置为false，仅执行一次
            is_triggerEnter = false;

            // 使玩家位置固定
            is_playerCanMove = false;
            playerStopPosition = player.position;

            // 转动相机，使相机看着磁盘
            makeCameraLookAtDiskController.LookAtDisk();

        }

        // 2.播放文本
        if(is_happened && is_rotateCameraEnd)
        {
            // 相机旋转完成，固定视角
            is_playerCanRotateCamera = false;
            playerStopLocalEulerAngles = player.localEulerAngles;
            cameraStopLocalEulerAngles = camera.localEulerAngles;

            // 设置为false，仅执行一次
            is_rotateCameraEnd = false;

            // 播放文本
            showTextsController.ShowTextsStart();
        }

        // 3.捡起磁盘动作
        if(is_happened && is_showTextEnd)
        {
            // 设置为false，仅执行一次
            is_showTextEnd = false;

            // 调用捡起磁盘动作
            pickUpDiskController.PickUpDisk();
        }

        // 4.全部完成，恢复玩家状态
        if(is_happened && is_pickDiskEnd)
        {
            // 设置为false，仅执行一次
            is_pickDiskEnd = false;

            //恢复玩家状态
            is_playerCanMove = true;
            is_playerCanRotateCamera = true;

            // 设置雷达状态，避免空目标
            GameObject.Find("Radar").GetComponent<Radar>().ShowTarget(GameObject.Find("FunctionPortal"));

            // 剧情：回到中转站
            NextSceneData.getInstance().NextSceneName = "StartMenu";
            SceneManager.LoadSceneAsync("LoadScene");
        }

    }

    private void LateUpdate()
    {
        if(!is_playerCanMove)
        {
            player.position = playerStopPosition;
        }
        if(!is_playerCanRotateCamera)
        {
            player.localEulerAngles = playerStopLocalEulerAngles;
            camera.localEulerAngles = cameraStopLocalEulerAngles;
        }
    }

    /// <summary>
    /// 当玩家进入碰撞体，标记触碰事件已发生，开始顺序执行
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(is_happened == false)
        {
            is_happened = true;
            is_triggerEnter = true;
        }
        
    }

    public void OnRotateCameraEnd()
    {
        is_rotateCameraEnd = true;
    }

    public void OnShowTextEnd()
    {
        is_showTextEnd = true;
    }

    public void OnPickUpDiskEnd()
    {
        is_pickDiskEnd = true;
    }
}
