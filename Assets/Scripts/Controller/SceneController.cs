using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Slider bar;
    public float minLoadTime;

    private AsyncOperation async;
    private float time;

    public void LoadScene(int index)
    {
        time = 0f;
        Scene scene = SceneManager.GetSceneByBuildIndex(index);
        if(scene == null )return;
        else StartCoroutine(ILoadScene(scene.name));
    }

    public void LoadScene(string name)
    {
        time = 0f;
        StartCoroutine(ILoadScene(name));
    }

    IEnumerator ILoadScene(string name)
    {
        async = SceneManager.LoadSceneAsync(name);
        async.allowSceneActivation = false;

        // Continue until the installation is completed
        while (async.isDone == false)
        {
            time += Time.deltaTime;

            bar.value = async.progress;
            //  if (loadingText != null)
            //      loadingText.text = "%" + (100 * bar.transform.localScale.x).ToString("####");

            if (async.progress >= 0.9f && time >= minLoadTime * 0.9f)
            {
                bar.value = 1;
                if(time >= minLoadTime)async.allowSceneActivation = true;
            }
            yield return null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
