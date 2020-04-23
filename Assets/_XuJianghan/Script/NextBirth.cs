using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBirth : MonoBehaviour
{
    private GameObject ReBirth;
    private int m_Next=0;
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
        if (m_Next == 0)
        {
            ReBirth.GetComponent<Rebirth>().NextBirth();
            m_Next++;
            //Debug.Log("next" + gameObject.name);
        }
        //Debug.Log("next"+ gameObject.name);
    }
}
