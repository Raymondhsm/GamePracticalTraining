using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globe
{
    public static string nextSceneName;
}

public class LoadScene : MonoBehaviour
{
    public Camera _camera;
    private BrightnessSaturationAndContrast _BrightnessSaturationAndContrast;

    public float MinTime = 3f;
    private float StartTime;

    private float curProgressValue;
    private AsyncOperation operation;
    private string NextSceneName;

    // Use this for initialization
    void Start()
    {
        //最小运行时间
        StartTime = Time.time;

        _BrightnessSaturationAndContrast = _camera.GetComponent<BrightnessSaturationAndContrast>();

        //if (SceneManager.GetActiveScene().name == "L2")
        {
            //获取需要加载场景的名字
            NextSceneName = NextSceneData.getInstance().NextSceneName;
            Debug.Log(NextSceneName);
            //启动协程
            StartCoroutine(AsyncLoading());
        }

        curProgressValue = 0;
    }

    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(NextSceneName);//Globe.nextSceneName
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;

        yield return operation;
    }

    private void Update()
    {
        UpdateAsync();

        //Debug.Log(Num);
    }

    // Update is called once per frame
    void UpdateAsync()
    {

        int progressValue = 90;

        curProgressValue = operation.progress * 100;

        if (curProgressValue < progressValue)
        {
            //curProgressValue++;
            _BrightnessSaturationAndContrast.brightness = 1 + curProgressValue / progressValue + (Time.time - StartTime)/ MinTime;
            _BrightnessSaturationAndContrast.saturation = 1 + curProgressValue / progressValue + (Time.time - StartTime)/ MinTime;
        }


        //Debug.Log(curProgressValue);

        if (operation.progress == 0.9f&& Time.time - StartTime > MinTime )
        {
            operation.allowSceneActivation = true;//启用自动加载场景  
        }
    }

}
