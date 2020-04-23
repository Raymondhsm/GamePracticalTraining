using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebirth : MonoBehaviour
{
    public Transform[] BirthPosition;
    public GameObject Player;
    private int i=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextBirth()
    {
        i = i + 1;
        if (i >= BirthPosition.Length - 1)
            i = BirthPosition.Length - 1;
    }

    public void ReBirth()
    {
        Player.GetComponent<Transform>().position = BirthPosition[i].position;
    }
}
