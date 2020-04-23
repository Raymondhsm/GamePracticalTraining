using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : MonoBehaviour
{
    public float m_force=80.0f;
    private bool _isEnter;
    private Rigidbody m_FPP;
    // Start is called before the first frame update
    void Start()
    {
        _isEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isEnter&&!PlayerPrefs.HasKey("RheaLabTask"))
            m_FPP.AddForce(Vector3.up * m_force, ForceMode.Force);
    }

     void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            //Debug.Log("进入低重力区");
            _isEnter = true;
            m_FPP = collision.gameObject.GetComponent<Rigidbody>();
        }
    }
}
