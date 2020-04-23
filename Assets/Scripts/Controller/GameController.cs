using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject canvasController;
    public GameObject storyCanvas;

    public GameObject[] Planet;

    public GameObject tmgo;
    public GameObject EscMenu;
    public GUIShowTexts showText;

    public AnimationController animationController;

    public string startStory;

    private Text storyText;
    private TaskManager taskManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject TM = GameObject.Find("TaskManager");
        if(!TM){
            TM = Instantiate(tmgo) as GameObject;
            TM.name = "TaskManager";
            DontDestroyOnLoad(TM);
        }



        GameObject escMenu = GameObject.Find("EscMenu");
        if (!escMenu)
        {
            escMenu = Instantiate(EscMenu) as GameObject;
            escMenu.name = "EscMenu";
            DontDestroyOnLoad(escMenu);
        }

        storyText = storyCanvas.GetComponentInChildren<Text>();

        
        taskManager = TM.GetComponent<TaskManager>();

        Debug.Log("gc yuan " + taskManager.CurrTaskIndex);
        Debug.Log("gc" + PlayerPrefs.GetInt("currTaskIndex"));
        taskManager.StartTask(PlayerPrefs.GetInt("currTaskIndex"));

        DisplayPlanetNum();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1)) taskManager.StartTask(0);
        else if (Input.GetKeyDown(KeyCode.F2)) taskManager.StartTask(2);
        else if (Input.GetKeyDown(KeyCode.F3)) taskManager.StartTask(4);
        else if (Input.GetKeyDown(KeyCode.F4)) taskManager.StartTask(6);
        else if (Input.GetKeyDown(KeyCode.F5)) taskManager.StartTask(8);
        else if (Input.GetKeyDown(KeyCode.F6)) taskManager.StartTask(9);
        else if (Input.GetKeyDown(KeyCode.F7)) taskManager.StartTask(10);
        else if (Input.GetKeyDown(KeyCode.F8)) taskManager.StartTask(12);
        else if (Input.GetKeyDown(KeyCode.F9)) taskManager.StartTask(14);
        else if (Input.GetKeyDown(KeyCode.F10)) taskManager.StartTask(16);
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            PlayerPrefs.SetInt("TrainTask", 2);
            SceneManager.LoadScene("StartMenu");
        }
        else if (Input.GetKeyDown(KeyCode.F12))
        {
            Planet[0].SetActive(false);
            Planet[1].SetActive(false);
            Planet[2].SetActive(false);
            DisplayPlanetNum();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayerPrefs.DeleteAll();
            taskManager.StartTask(0);
            SceneManager.LoadScene(0);
        }


    }

    public void DisplayPlanetNum()
    {
        //Debug.Log(PlayerPrefs.GetInt("RheaLabTask"));
        // Debug.Log(PlayerPrefs.GetInt("TrainTask"));
        if(taskManager.CurrTaskIndex > 10)
        {
            Planet[0].SetActive(true);
            Planet[1].SetActive(true);
            Planet[2].SetActive(true);
        }
        else if(taskManager.CurrTaskIndex > 5)
        {
            Planet[0].SetActive(true);
            Planet[1].SetActive(true);
        }
        else if(taskManager.CurrTaskIndex > 1)
        {
            Planet[0].SetActive(true);
            
            //设置星球位置为居中
            
            //Debug.Log(Planet[0].GetComponent<Transform>().position);
            Planet[0].GetComponent<Transform>().position = new Vector3(11f,2.9f,-0.6f);
            //Debug.Log(Planet[0].GetComponent<Transform>().position);
        }
    }

    public void ShowStory()
    {
        OpenStoryCanvas(startStory);
    }

    public void DisplayText()
    {
        string[] str;
        if(PlayerPrefs.GetInt("GoToGaeaTask") == 2)
        {
            str = new string[1];
            str[0] = "恭喜你完成游戏剧情，你还可以继续游戏";
        }
        else if(PlayerPrefs.GetInt("FindBlackWhiteLabTask") == 2)
        {
            str = new string[7];
            str[0] = "科学家：\n 还好，古人族还是给我们提供了最后的解决办法。";
            str[1] = "长官：什么办法？这两个鬼东西一旦解放，这整个星系都没好果子吃。";
            str[2] = "科学家：不用担心，他们给我们提供的方法，还是远程的。";
            str[3] = "科学家：只需要去盖亚星，找到一个控制台，";
            str[4] = "科学家：按照磁盘上的要求操作后，那座实验室会使用他们自己研发出来的重力控制系统，来加大整个实验室的防御力度，";
            str[5] = "科学家：用于抵御黑洞白洞解放后互相湮灭产生的巨大能量，再将黑洞白洞放出。";
            str[6] = "科学家：根据他们的说法，最终被毁灭的也只有瑞亚星而已，不会波及到整个星系。";

        }
        else if(PlayerPrefs.GetInt("GaeaTask") == 2)
        {
            str = new string[9];
            str[0] = "科学家：你们回来得正好。我们刚好破译了外星人文字的意思，还了解了他们机器的运作方式。现在整个实验室一片热火朝天呢。";
            str[1] = "长官：你们就热火朝天吧，我这边出大事了。";
            str[2] = "科学家：什么？";
            str[3] = "将磁盘递了过去，看着磁盘的内容，科学家眉头紧皱。";
            str[4] = "科学家：的确，他们——自称古人族——给我们带来的并不是什么好消息。";
            str[5] = "科学家：古人族高速发展的科技已经开始探索改变空间、改变重力的办法，并取得了一定成果——盖亚星和瑞亚星之间的通道。不满足的他们，将手伸向了宇宙中的神秘天体——黑洞和白洞。";
            str[6] = "科学家：黑洞和白洞的力量根本不是一般的装备能够挡得住的，自以为将黑洞和白洞“囚禁”了的他们，最后大都死在了辐射之下。";
            str[7] = "科学家：在这个磁盘的最后，它说了研究黑洞和白洞的实验室的位置是在通道中，还给出了一个坐标，说是只能在通道中使用。";
            str[8] = "我：所以这个任务，非我莫属咯。";

        }
        else if(PlayerPrefs.GetInt("FirstAtGaeaTask") == 2)
        {
            str = new string[8];
            str[0] = "长官：按照下面的汇报，似乎发现了了不起的事情了。";
            str[1] = "科学家：我们的人在其他地方也发现了在U星那个建筑里看到的文字，已经派人去解读了，U星发现的那些文字的内容，很快就能知道了。";
            str[2] = "我：汇报！我找到了那个神秘的星球了，里面的重力很大，于是我判断先回来向你们汇报。";
            str[3] = "科学家：你的判断是没错的。告诉我你在那个星球的感觉如何？我帮你调试一下你的宇航服。";
            str[4] = "…………";
            str[5] = "科研人员：报告，在新的星球里发现了新的遗迹！";
            str[6] = "科学家：你听到了吧。该你们上场了。";
            str[7] = "长官&我：走吧！";

        }
        else if(PlayerPrefs.GetInt("UranosTask") == 2)
        {
            str = new string[4];
            str[0] = "我：长官，我有件事汇报一下。";
            str[1] = "长官：怎么了？";
            str[2] = "我：好像还有另一个星球的存在，但我也只知道这个星球有和瑞亚星链接的通道，而且他们好像在干什么不为人知的事情。";
            str[3] = "长官：嗯……这个我会向上级汇报的，你先去瑞亚星探探情况吧。";
        }
        else if(PlayerPrefs.GetInt("RheaLabTask") == 2)
        {
            str = new string[3];
            str[0] = "我：我发现了一个新的星球！";
            str[1] = "科学家：好的！";
            str[2] = "科学家：……已定位坐标，已经随时都可以去那个星球了。";
        }
        else if(PlayerPrefs.GetInt("RheaClothesTask") == 2)
        {
            str = new string[4];
            str[0] = "科学家：这！！这居然是能适应重力的宇航服？";
            str[1] = "长官：能做出来吗？我们在低重力区发现一座漂浮的建筑，这对我们的探索会很有帮助。";
            str[2] = "科学家：没问题！顺带我帮你们设置一下追踪器，给你们去那座建筑的指引。";
            str[3] = "获得重力适应宇航套装";
        }
        else if(PlayerPrefs.GetInt("TrainTask") == 2)
        {   
            str = new string[1];
            str[0] = "科学家：这好像是能够在宇宙中定位的装置，修好了就给你们用吧。";
        }
        else
        {
            str = new string[3];
            str[0] = "科学家：\n 全体人员注意，前方发现大型石块，并在上方看到了类似建筑物的东西！需要派人去探索！";
            str[1] = "长官：\n\t 好像该轮到我们干活了。准备出发吧。";
            str[2] = "我：\n\t 是！长官！";
        }

        showText.Texts = str;
        showText.ShowTextsStart();
    }


    public void showMenu()
    {
        //animationController.OpenPlanet();
        StartMenu sm = canvasController.GetComponent<StartMenu>();
        sm.StartCanvas();
    }


    // public void OpenStartAnimation()
    // {
    //     StartMenu sm = canvasController.GetComponent<StartMenu>();
    //     sm.StartCanvas();
    //     if(PlayerPrefs.GetInt("TrainTask") != 2) OpenStoryCanvas(startStory);
    // }

    public void OpenStoryCanvas(string story)
    {
        animationController.OpenStoryAnimation();
        storyText.text = story;
    }

    // public void CloseStoryCanvas()
    // {
    //     animationController.CloseStoryAnimation();
    // }

}
