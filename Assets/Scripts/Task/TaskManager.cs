using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public struct TaskInfo{
        public string name; 
        public string sceneName;
        public string message;
    }

    public List<TaskInfo> taskList;
    public Text taskTitleText;
    
    private Dictionary<string,Task> currTaskList;         //当前进行中的任务
    private List<string> finishedTaskList;
    private List<string> justFinishedTaskList;

    private int currTaskIndex;
    public int CurrTaskIndex
    {
        set { currTaskIndex = value; }
        get { return currTaskIndex; }
    }


    void Awake(){
        //为数据结构申请内存
        //taskList = new List<TaskInfo>();
        currTaskList = new Dictionary<string,Task>();
        finishedTaskList = new List<string>();
        justFinishedTaskList = new List<string>();

        currTaskIndex = PlayerPrefs.HasKey("currTaskIndex") ? PlayerPrefs.GetInt("currTaskIndex") : 0;

        StartTask(currTaskIndex);
    }

    ~TaskManager()
    {
        PlayerPrefs.SetInt("currTaskIndex",currTaskIndex);
    }

    public void StartTask(string name, string sceneName)
    {
        currTaskIndex = FindTaskIndex(name);
        taskTitleText.text = taskList[currTaskIndex].message;
        StopAllCoroutines();
        PlayerPrefs.SetInt("currTaskIndex",currTaskIndex);
        StartCoroutine(canStart(name, sceneName));
    }

    public void StartTask(int index)
    {
        if(index >= taskList.Count)return;

        TaskInfo ti = taskList[index];
        currTaskIndex = index;
        taskTitleText.text = taskList[currTaskIndex].message;
        StopAllCoroutines();
        PlayerPrefs.SetInt("currTaskIndex", currTaskIndex);
        StartCoroutine(canStart(ti.name, ti.sceneName));
    }

    private IEnumerator canStart(string name, string sceneName)
    {
        while(sceneName != SceneManager.GetActiveScene().name)
        {
            yield return 0;
        }

        StartTask(name);
    }

    //开始任务
    private bool StartTask(string name)
    {
        //if(isTaskActive(name))return true;

        //实例化任务
        Task task = initializeTask(name);

        //添加开始的任务
        addOnGoingTask(task);

        //调用任务开始函数
        task.StartTask();

        return true;
    }
    

    public void NextTask()
    {
        // Debug.Log("NextTask");
        TaskInfo ti = taskList[++currTaskIndex];
        taskTitleText.text = taskList[currTaskIndex].message;
        PlayerPrefs.SetInt("currTaskIndex",currTaskIndex);
        StartCoroutine(canStart(ti.name, ti.sceneName));
    }

    //实例化任务
    private Task initializeTask(string name)
    {
        if(GameObject.Find(name) != null)
            return GameObject.Find(name).GetComponent<Task>();

        // Debug.Log("initial");

        //从prefab中load任务
        Object prefab = Resources.Load("TaskPrefabs/" + name) as Object;
        if (prefab == null) {
            // Debug.LogError("not found task prefab");
            return null;
        }
        GameObject taskObject = Instantiate(prefab) as GameObject;

        //读取任务进度
        readHistory(taskObject.GetComponent<Task>());
        
        return taskObject.GetComponent<Task>();
    }

    //  读取任务进度
    private void readHistory(Task task)
    {
        if(!PlayerPrefs.HasKey(task.taskName))return;
        task.CurrTaskNodeIndex = PlayerPrefs.GetInt(task.taskName + ".taskNodeIndex");
        task.CurrTaskNodeNum = PlayerPrefs.GetInt(task.taskName + ".taskNodeNum");
        task.CurrLayer = PlayerPrefs.GetInt(task.taskName + ".taskNodeLayer");

        // if(!taskSchedule.ContainsKey(task.TaskName))return;
        // OnGoingTask t = taskSchedule[task.TaskName] as OnGoingTask;
        // task.CurrTaskNodeIndex = t.taskNodeIndex;
        // task.CurrTaskNodeNum = t.taskNodeNum;
    }

    //  添加任务列表
    public void addOnGoingTask(Task task){
        task.TManager = this;
        if (!currTaskList.ContainsKey(task.taskName)) currTaskList.Add(task.TaskName,task);
    }

    //判断任务是否存在
    private int FindTaskIndex(string name)
    {
        int i=0;
        foreach(TaskInfo na in taskList)
        {
            if(na.name == name)return i;
            ++i;
        }
        return -1;
    }

    //判断任务是否为进行中
    private bool isTaskActive(string name)
    {
        return currTaskList.ContainsKey(name);
    }

    public Task getOnGoingTaskByName(string name){
        bool index = currTaskList.ContainsKey(name);
        if(index)
            return currTaskList[name];
        else return null;
    }

    public List<string> getJustFinishedTask(){
        return justFinishedTaskList;
    }

   
    //相应任务完成的函数
    public void RespondTask(string name)
    {
        currTaskList.Remove(name);
        finishedTaskList.Add(name);
        justFinishedTaskList.Add(name);

        //开启下一个任务
        NextTask();
    }

    //任务完成的事件函数
    public void FinishedTaskEvent()
    {
        foreach(string task in justFinishedTaskList){
            //任务完成后的操作
            justFinishedTaskList.Remove(task);
        }
    } 
}