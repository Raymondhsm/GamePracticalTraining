using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePlayerDead : MonoBehaviour
{
    /// <summary>
    /// 玩家掉进了深渊，需要把玩家“弄死”
    /// 可能需要死的音效，死后怎么做也需要斟酌，是否传回原点
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "m_FPP")
        {

            other.transform.position = new Vector3(-12.679f, 0.982f, 20.105f);
        }
    }
}
