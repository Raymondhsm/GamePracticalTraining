using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RheaGameController : MonoBehaviour
{
    public Text text;
    public Animator textAnimator;
    public float textShowTime;
    public GUIShowTexts m_text;
    public Radar m_radar;
    public GameObject m_target;
    public GameObject m_target1;
    private string[] strs1 = new string[1];
    private string[] strs2 = new string[1];
    private string[] strs3 = new string[1];
    private string[] strs4 = new string[1];
    private string[] strs5 = new string[1];
    private string[] strs6 = new string[1];
    private string[] strs7 = new string[1];

    // Start is called before the first frame update
    void Start()
    {
        /*if ((!PlayerPrefs.HasKey("RheaClothesTask")) && (PlayerPrefs.GetInt("RheaClothesTask") != 2))
            StartText();
        else
        {
            Debug.Log("没有执行");
            Debug.Log(PlayerPrefs.HasKey("RheaClothesTask"));
            Debug.Log(PlayerPrefs.GetInt("RheaClothesTask"));
        }*/
        if (!PlayerPrefs.HasKey("RheaClothesTask"))
            m_radar.HideRadar();
        if ((PlayerPrefs.GetInt("RheaClothesTask") == 2) && (!PlayerPrefs.HasKey("RheaLabTask")))
            m_radar.ShowTarget(m_target1);
        if ((PlayerPrefs.GetInt("RheaClothesTask") == 2) && (PlayerPrefs.GetInt("RheaLabTask") == 2) && (!PlayerPrefs.HasKey("FindRheaSecretTask")))
        {
            m_radar.ShowRandom();
            StartCoroutine(WaitForRadarStable());
        }
    }

    public void StartText()
    {
        Debug.Log("ok");
        strs1[0] = "这里就是瑞亚星……这里的环境和地球好像……可是为什么……？";
        m_text.Texts = strs1;
        m_text.ShowTextsStart();
    }

    public void RespondFindGravity()
    {
        strs2[0] = "这里的重力，好像比刚才的地方要低，呆久了可能有生命危险，虽然能看到远方天上有一个好像实验室的建筑，但是还是回去吧";
        m_text.Texts = strs2;
        m_text.ShowTextsStart();
        m_radar.ShowTarget(m_target);
    }

    public void RespondFindClothes()
    {
        strs3[0] = "这好像是一件宇航服的残骸？为什么和人类的那么像？带回去给科学家们看看吧。";
        m_text.Texts = strs3;
        m_text.ShowTextsStart();
    }

    public void RespondAvoidClimb()
    {
        strs4[0] = "目前也只是探索，没必要去山上的地方。";
        m_text.Texts = strs4;
        m_text.ShowTextsStart();
    }

    public void RespondAvoidAbyss()
    {
        strs5[0] = "这里看起来很危险，还是不要贸然深入比较好";
        m_text.Texts = strs5;
        m_text.ShowTextsStart();
    }

    public void RespondFindComputer()
    {
        strs6[0] = "这似乎是附近的另一个星球，先记录下来坐标吧。";
        m_text.Texts = strs6;
        m_text.ShowTextsStart();
    }

    public void RespondEnterAbyss()
    {
        strs7[0] = "这里好像有什么秘密，下去看看吧。";
        m_text.Texts = strs7;
        m_text.ShowTextsStart();
    }

    public void BackStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    void ShowText(string showText)
    {
        text.text = showText;
        textAnimator.SetTrigger("TextAppear");

        StartCoroutine(CloseText());
    }

    IEnumerator CloseText()
    {
        float time = 0f;

        while(time <= textShowTime)
        {
            time += Time.deltaTime;
            yield return 0;
        }
        textAnimator.SetTrigger("TextDisappear");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if((!PlayerPrefs.HasKey("RheaClothesTask")) &&PlayerPrefs.GetInt("RheaClothesTask") !=2&&this.gameObject.name!="SuperComputer")
            {
                RespondAvoidClimb();
            }
            if(this.gameObject.name=="SuperComputer")
            {
                RespondFindComputer();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F8))SceneManager.LoadScene("StartMenu");
    }

    private IEnumerator WaitForRadarStable()
    {
        yield return new WaitForSeconds(5);
        m_radar.ShowTarget(GameObject.Find("AbyssEnter"));
    }
}
