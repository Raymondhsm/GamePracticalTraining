using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UranosGameController : MonoBehaviour
{
    public GUIShowTexts GUIshowText;

    public Radar radar;

    // Start is called before the first frame update
    void Start()
    {
        radar.HideRadar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void ShowText1()
    {
        string []s = { "这是怎么回事？！",
            "好像是指引我去什么地方……去看看吧。"
         };
        Debug.Log(s[0]);
        GUIshowText.Texts = s;
        GUIshowText.ShowTextsStart();

        radar.ShowTarget();
    }

    public void ShowText2()
    {
        string[] s = { "……这是！",
            "赶紧回去告诉他们这个消息吧。"
         };
        Debug.Log(s[0]);
        GUIshowText.Texts = s;
        GUIshowText.ShowTextsStart();
    }

    public void NextScene()
    {
        NextSceneData.getInstance().NextSceneName = "StartMenu";
        SceneManager.LoadSceneAsync("LoadScene");
    }

}
