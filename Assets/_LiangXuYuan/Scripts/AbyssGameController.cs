using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssGameController : MonoBehaviour
{
    public Radar radar;

    [Header("任务15：去盖亚星传送门")]
    public GameObject potal;

    [Header("任务20：探索黑洞深处")]
    public GameObject disk;
    public GUIShowTexts showText0;
    public GUIShowTexts showText1;
    public GUIShowTexts showText2;
    public GUIShowTexts showText3;
    public GUIShowTexts showText4;
    public GUIShowTexts showText5;

    public void StartLocateGaeaTask()
    {
        Debug.Log("前往盖亚星任务开始");

        // 雷达目标设置为传送门
        radar.ShowTarget(potal);

        // 说话
        showText0.ShowTextsStart();
    }

    public void StartFindBlackWhiteLabTask()
    {
        Debug.Log("探索黑洞白洞实验室任务开始");

        // 标志乱飞
        radar.ShowRandom();

        // 说话
        showText1.ShowTextsStart();
    }

    public void FinishFindRadarStablePlaceTask()
    {
        Debug.Log("找到雷达稳定位置任务完成");

        // 雷达显示目标
        radar.ShowTarget(disk);

        // 说话
        showText2.ShowTextsStart();
    }

    public void FinishFindEntryOfLabTask()
    {
        Debug.Log("找到实验室入口任务完成");

        showText3.ShowTextsStart();
    }
    public void FinishEnterLabTask()
    {
        Debug.Log("进入实验室任务完成");

        showText4.ShowTextsStart();
    }

    public void FinishEnterLab2Task()
    {
        Debug.Log("进入实验室2任务完成");

        showText5.ShowTextsStart();
    }
    public void FinishDiskTask()
    {
        Debug.Log("找到并捡起磁盘任务完成");
    }
}
