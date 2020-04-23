using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DustGameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToStartMenu()
    {
        NextSceneData.getInstance().NextSceneName = "StartMenu";
        SceneManager.LoadSceneAsync("LoadScene");
    }
}
