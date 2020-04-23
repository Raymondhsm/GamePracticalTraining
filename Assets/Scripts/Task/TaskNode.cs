
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TaskNode : MonoBehaviour
{
    public bool canOriginVisible = false;
    public bool canFinishedVisble  = false;
    public string taskNodeName;
    public int layer;
    public UnityEvent finishEvent;

    private GameObject sceneGameController; 

    private bool isEnable;
    public bool IsEnable
    {
        set { isEnable = value; }
        get { return isEnable; }
    }

    private bool isFinished;
    public bool IsFinished
    {
        set { isFinished = value; }
        get { return isFinished; }
    }

    
    public string TaskNodeName
    {
        set { taskNodeName = value; }
        get { return taskNodeName; }
    }

    private Task parent;
    public Task Parent
    {
        set { parent = value; }
        get { return parent; }
    }


    void Start()
    {
        // Debug.Log(taskNodeName + " tn Start");
        //初始化
        parent = gameObject.GetComponentInParent<Task>();
        isEnable = false;
        isFinished = false;
        parent.addTaskNode(this);

        //设置名字默认值；
        if(taskNodeName == "")taskNodeName = gameObject.name;

        if(!canOriginVisible) gameObject.SetActive(false);
    }

    public void setVisible()
    {
        gameObject.SetActive(true);
    }

    //任务节点触发函数
    void OnTriggerEnter(Collider collider){
        //Debug.Log(isEnable);
        if(!isEnable) return;
        if(collider.CompareTag("Player")){
            NotifyTask();
        }
    }

    virtual protected void NotifyTask(){
        //Debug.Log(taskNodeName + " notifyTask");

        finishEvent.Invoke();

        bool result = parent.RespondTaskNode(layer, taskNodeName);

        if(result){
            isEnable = false;
            if(!canFinishedVisble)gameObject.SetActive(false);

            //其他完成该任务节点的操作
            //。。。
        }
    }

}
