using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GUIShowTexts : MonoBehaviour
{
    [Header("GUI组件")]
    [Tooltip("用于显示文本的Canvas")]
    public Canvas canvas;

    [Tooltip("用于显示文本的EventSystem")]
    public EventSystem eventSystem;

    [Tooltip("将画布中显示文字的Text组件拖到这里")]
    public Text textComponent;

    [Header("文本")]
    [Tooltip("将要显示的文本，输入数量和文本")]
    public string[] Texts;

    [Header("播放设置")]
    [Tooltip("文本更新时间间隔")]
    public float waitTime = 3f;

    public UnityEvent finishEvent;

    private int m_currShowIndex = 0;// 当前显示文本的index

    void Awake()
    {
        // 初始化时将画布隐藏起来
        canvas.enabled = false;
        eventSystem.enabled = false;
        //Debug.Log("false");
    }

    /// <summary>
    /// 外部调用，开始显示文本
    /// </summary>
    public void ShowTextsStart()
    {
        // 开始播放时将画布显示出来
        canvas.enabled = true;
        eventSystem.enabled = true;
        // 开始协程函数，播放文本
        StartCoroutine(ShowNextTexts());
        //Debug.Log("true");
    }

    /// <summary>
    /// 协程调用函数，每过一段时间，显示下一条文本
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowNextTexts()
    {
        //Debug.Log("GUI1");
        while (m_currShowIndex < Texts.Length)
        {
            textComponent.text = Texts[m_currShowIndex];
            m_currShowIndex = m_currShowIndex + 1;
            yield return new WaitForSeconds(waitTime);
            //Debug.Log("GUI2");
        }

        // 执行到这里表示文本播放结束并调用结束事件
        finishEvent.Invoke();

        // 执行完把画布隐藏起来
        canvas.enabled = false;
        eventSystem.enabled = false;

        // 关闭此脚本
        this.enabled = false;
        //Debug.Log("GUI3");
    }
}
