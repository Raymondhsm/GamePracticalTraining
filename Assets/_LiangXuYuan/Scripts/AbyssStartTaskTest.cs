using UnityEngine;


/// <summary>
/// !!!仅用于Abyss星球测试任务用，必要时删除
/// </summary>
public class AbyssStartTaskTest : MonoBehaviour
{
    // 获取任务脚本
    public Task task1;
    public Task task2;

    // 标记任务开始
    private bool is_taskStarted = false;

    private void Update()
    {
        if (!is_taskStarted)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                task1.StartTask();
                is_taskStarted = true;
            }
            else if(Input.GetKeyDown(KeyCode.O))
            {
                task2.StartTask();
                is_taskStarted = true;
            }
        }
    }
}
