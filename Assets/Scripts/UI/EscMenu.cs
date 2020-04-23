using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    // esc按键菜单，可返回中转站
    public GameObject EscMenuCanvas;

    // 玩家控制脚本，可在显示菜单时禁用
    private GameObject m_player;
    private FPPController m_controller;

    void Start()
    {
        // 游戏开始时，隐藏界面
        EscMenuCanvas.SetActive(false);
    }

    void Update()
    {
        // 按下Esc键时显示菜单
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "StartMenu")
        {
            // 初始化私有变量
            m_player = GameObject.Find("m_FPP");
            m_controller = m_player.GetComponent<FPPController>();

            EscMenuCanvas.SetActive(true);

            m_controller.enabled = false; // 禁用玩家脚本，使鼠标移动不影响视角

            Cursor.lockState = CursorLockMode.None; // 设置鼠标状态：不锁定、可见
            Cursor.visible = true;
        }
    }

    /// <summary>
    /// 点击了返回中转站按钮
    /// </summary>
    public void OnReturnBtnClick()
    {
        // 隐藏界面
        EscMenuCanvas.SetActive(false);

        Physics.gravity = new Vector3(0, -9.81f, 0);

        NextSceneData.getInstance().NextSceneName = "StartMenu";
        SceneManager.LoadSceneAsync("LoadScene");
    }

    /// <summary>
    /// 点击了继续游戏按钮
    /// </summary>
    public void OnCancelBtnClick()
    {
        // 隐藏界面
        EscMenuCanvas.SetActive(false);

        // 恢复玩家控制脚本
        m_controller.enabled = true;

        // 恢复鼠标状态
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
