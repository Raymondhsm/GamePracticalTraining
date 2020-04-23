using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("进场动画设置")]
    public Animator openDoorAnim;
    public Animator CameraMoveAnim;
    public GameObject door;
    public GameObject gameController;

    public Animator planetAnimator;
    public Animator storyAnimator;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(StartAnimation());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public IEnumerator StartAnimation()
    {
        AudioSource au = door.GetComponent<AudioSource>();

        //开门
        openDoorAnim.SetTrigger("doorToOpen");
        au.Play();
        CameraMoveAnim.SetTrigger("CameraForward");

        AnimatorStateInfo info = CameraMoveAnim.GetCurrentAnimatorStateInfo(0);
        while(!info.IsName("MoveEnd")){
            info = CameraMoveAnim.GetCurrentAnimatorStateInfo(0);

            yield return 0;
        }
        
        CameraMoveAnim.enabled = false;

        if(PlayerPrefs.GetInt("TrainTask") != 2)
        {
            GameController gc = gameController.GetComponent<GameController>();
            gc.ShowStory();
        }
        else
        {
            GameController gc = gameController.GetComponent<GameController>();
            gc.DisplayText();
        }
        
    }

    public void OpenPlanet()
    {
        planetAnimator.SetTrigger("planetAppear");
    }

    public void OpenStoryAnimation()
    {
        StartCoroutine(OpenStoryCanvas());
    }

    public IEnumerator OpenStoryCanvas()
    {
        storyAnimator.SetTrigger("openStory");

        yield return new WaitForSeconds(10);

        storyAnimator.SetTrigger("closeStory");

        GameController gc = gameController.GetComponent<GameController>();
        gc.DisplayText();
    }

    // public void CloseStoryAnimation()
    // {
    //     storyAnimator.SetTrigger("closeStory");
    //     StartCoroutine(CloseStoryCanvas());
    // }

    // public IEnumerator CloseStoryCanvas()
    // {
    //     AnimatorStateInfo info = storyAnimator.GetCurrentAnimatorStateInfo(0);
    //     while(!info.IsName("closeStoryCanvased")){
    //         info = storyAnimator.GetCurrentAnimatorStateInfo(0);
    //         yield return 0;
    //     }

    //     planetAnimator.SetTrigger("planetAppear");
    // }

}

