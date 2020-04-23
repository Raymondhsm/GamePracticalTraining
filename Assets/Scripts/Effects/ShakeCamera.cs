using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Vector3 positionShake = new Vector3(0, 0.1f, 0);//震动幅度
    public Vector3 angleShake;   //震动角度
    public float cycleTime = 0.5f;//震动周期
    public bool unscaleTime = false;//不考虑缩放时间
    public bool bothDir = true;//双向震动

    bool isShaking;
    float currentTime;
    int curCycle;
    Vector3 curPositonShake;
    Vector3 curAngleShake;
    float curFovShake;
    Vector3 startPosition;
    Vector3 startAngles;
    Transform myTransform;

    void OnEnable()
    {
        currentTime = 0f;
        curCycle = 0;
        curPositonShake = positionShake;
        curAngleShake = angleShake;
        myTransform = transform;
        startPosition = myTransform.localPosition;
        startAngles = myTransform.localEulerAngles;
    }

    void OnDisable()
    {
        myTransform.localPosition = startPosition;
        myTransform.localEulerAngles = startAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShaking)
        {
            return;
        }

        float deltaTime = unscaleTime ? Time.unscaledDeltaTime : Time.deltaTime;
        currentTime += deltaTime;
        while (currentTime >= cycleTime)
        {
            currentTime -= cycleTime;
            curCycle++;
        }

        if (isShaking)
        {
            float offsetScale = Mathf.Sin((bothDir ? 2 : 1) * Mathf.PI * currentTime / cycleTime);
            if (positionShake != Vector3.zero)
                myTransform.localPosition = startPosition + curPositonShake * offsetScale;
            if (angleShake != Vector3.zero)
                myTransform.localEulerAngles = startAngles + curAngleShake * offsetScale;
        }
    }
    //重置
    public void Restart()
    {
        if (enabled)
        {
            Debug.Log("Restart");
            currentTime = 0f;
            curCycle = 0;
            curPositonShake = positionShake;
            curAngleShake = angleShake;
            myTransform.localPosition = startPosition;
            myTransform.localEulerAngles = startAngles;
        }
        else
            enabled = true;
    }

    public void Shake(bool isshaking)
    {
        isShaking = isshaking;
    }
}
