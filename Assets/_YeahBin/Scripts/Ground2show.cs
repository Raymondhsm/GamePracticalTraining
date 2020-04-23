using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground2show : MonoBehaviour
{
    public GameObject Ground;

    public float speed = 10.0f;
    public Vector3 endPoint = Vector3.zero;
    public bool pingpong = true;

    private Vector3 curEndPoint = Vector3.zero;
    private Vector3 startPoint = Vector3.zero;
    private bool showGround;

    // Start is called before the first frame update
    void Start()
    {
        showGround = false;
        startPoint = Ground.transform.position;
        curEndPoint = endPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (showGround)
        {
            Ground.transform.position = Vector3.Slerp(Ground.transform.position, curEndPoint, Time.deltaTime * speed);
            if (pingpong)
            {
                if (Vector3.Distance(Ground.transform.position, curEndPoint) < 0.1f)
                {
                    //Debug.Log("curEndPoint.y"+ curEndPoint.y);
                    curEndPoint = Vector3.Distance(curEndPoint, endPoint) < Vector3.Distance(curEndPoint, startPoint) ? startPoint : endPoint;
                }
            }
        }
           
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showGround = true;
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
