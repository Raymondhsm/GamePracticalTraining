using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//超重星geae盖亚和超轻星Uranos乌拉诺斯可用
public class GravityChange : MonoBehaviour
{
    public float GTimes = 10f;
    private float g = 9.81f;
    private Rigidbody Rid;
    // Start is called before the first frame update
    void Start()
    {
        Rid = this.GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Rid.AddForce(Vector3.down * g * (GTimes - 1) * (Rid.mass));
    }
}
