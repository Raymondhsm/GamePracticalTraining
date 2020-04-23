using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 在教程关卡中名为“textPlane”的平面或游戏物体为放置脚本的物体
/// 然后将要触发的3D Text在面板中初始化即可
/// </summary>
public class TextController : MonoBehaviour
{
    static private int currLayer = 0;

    public Animator textAnimator;
    public GameObject texts;//教程关卡的指导字幕
    public string content;//内容存储

    public int layer = -1;


    /// <summary>
    /// 玩家通过触碰到特定地点来引发指导字幕
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter(Collision other)
    {
        if(layer != currLayer && layer != -1 && currLayer != 0)return;
        if(content == texts.GetComponent<Text>().text)return;

        if(other.gameObject.tag=="Player")
        {
            textAnimator.SetTrigger("TextAppear");
            StartCoroutine(setContent());
            if(layer == -1)currLayer = 0;
            else currLayer = layer + 1;
        }
    }

    IEnumerator setContent()
    {
        AnimatorStateInfo info = textAnimator.GetCurrentAnimatorStateInfo(0);

        while(!info.IsName("TextAppear")){
            info = textAnimator.GetCurrentAnimatorStateInfo(0);
            yield return 0;
        }

        texts.GetComponent<Text>().text = content;
    }

}
