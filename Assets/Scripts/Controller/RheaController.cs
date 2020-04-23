using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  m_target 放置触发的结果
/// </summary>
public class RheaController : MonoBehaviour
{
    public GameObject[] CheckPoints;
    public Radar radar;
    public GameObject target;
    void Start()
    {
        for (int i = 0; i < CheckPoints.Length; i++)
        {
            CheckPoints[i].GetComponent<MeshRenderer>().enabled = false;
            CheckPoints[i].GetComponent<Collider>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name == "Cylinder")
        {
            if (!PlayerPrefs.HasKey("RheaClothesTask"))
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.gameObject.GetComponent<Collider>().enabled = false;
            }
            if((PlayerPrefs.GetInt("RheaClothesTask") == 2) && (PlayerPrefs.GetInt("RheaLabTask") == 2))
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < CheckPoints.Length; i++)
            {
                CheckPoints[i].GetComponent<MeshRenderer>().enabled = true;
                CheckPoints[i].GetComponent<Collider>().enabled = true;
            }
            Destroy(gameObject);
            if (this.gameObject.tag == "Light")
            {
                this.GetComponent<MeshRenderer>().enabled = false;
                this.GetComponent<Collider>().enabled = false;
            }
            radar.ShowTarget(target);

        }
    }
}
