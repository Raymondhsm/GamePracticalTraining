using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        transform.Find("Plane").gameObject.SetActive(true);
    }

    public void setRadarTarget()
    {
        GameObject.Find("Radar").GetComponent<Radar>().ShowTarget(gameObject);
    }
}
