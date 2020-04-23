using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource m_Audios;      //音频源
    public AudioClip m_WalkingSound;   //走路声
    public AudioClip m_RunSound;       //跑步声
    public AudioClip m_JumpSound;      //跳跃声
    public AudioClip m_GlideSound;     //滑翔声
    public AudioClip m_BackGround;     //背景音乐声
    public AudioClip m_WallJumpSound;  //蹭墙声
    public AudioClip m_DeadSound;      //死亡声

    // Start is called before the first frame update
    void Start()
    {
        _Init();
        m_Audios.Pause();
    }

    private void _Init()
    {
        m_Audios = GetComponent<AudioSource>();
    }

    public bool  _isPlaying()
    {
        return m_Audios.isPlaying;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 暂停
    /// </summary>
    public void _Pause()
    {
        m_Audios.Pause();
    }

    /// <summary>
    /// 蹭墙声
    /// </summary>
    public void WallJumpSound()
    {
        m_Audios.clip = m_WallJumpSound;
        m_Audios.PlayOneShot(m_WallJumpSound);
    }

    /// <summary>
    /// 播放走路声
    /// </summary>
    public void WalkSound()
    {
        m_Audios.clip = m_WalkingSound;
        m_Audios.PlayOneShot(m_WalkingSound);
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    public void PlayBackGroud()
    {
        m_Audios.clip = m_BackGround;
        m_Audios.Play();
    }

    /// <summary>
    /// 播放跑步声
    /// </summary>
   public void RunSound()
    {
        m_Audios.clip = m_RunSound;
        m_Audios.PlayOneShot(m_RunSound);
    }

    /// <summary>
    /// 播放跳跃声
    /// </summary>
    public void JumpSound()
    {
        m_Audios.clip = m_JumpSound;
        m_Audios.PlayOneShot(m_JumpSound);
    }

    /// <summary>
    /// 播放滑翔声
    /// </summary>
    public void GlideSound()
    {
        m_Audios.clip = m_GlideSound;
        m_Audios.PlayOneShot(m_GlideSound);
    }

    /// <summary>
    /// 播放死亡声
    /// </summary>
    public void DeadSound()
    {
        m_Audios.clip = m_DeadSound;
        m_Audios.Play();
    }

    /// <summary>
    /// 停止
    /// </summary>
    public void _Stop()
    {
        m_Audios.Stop();
    }

    public void _CloseLoop()
    {
        m_Audios.loop = false;
    }

    public void _OpenLoop()
    {
        m_Audios.loop = true;
    }
}
