using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBirth : MonoBehaviour
{
    private GameObject ReBirth;
    public GameObject[] ResetGameObject;
    // Start is called before the first frame update
    void Start()
    {
        ReBirth = GameObject.Find("ReBirth");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ReBirth.GetComponent<Rebirth>().ReBirth();
        for(int i=0;i<ResetGameObject.Length;i++)
        {
            ResetGameObject[i].GetComponent<RiseRock>().Reset();
        }
    }
}
