using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour
{

    private float speed;
    private int bTime;
    private Rigidbody rb;
    private string[] hit = {"Fence1","Fence2","Fence3","Fence4","Boss(Clone)","Player"};

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bTime = 0;
    }

    void Update()
    {
        rb.velocity = speed*(rb.velocity.normalized);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (bTime>10){
            Destroy(gameObject);
        }
        foreach (string x in hit){
            if (x.Equals (col.gameObject.name)){
                bTime = bTime + 1;
            }
        }
    }

    public int getbTime(){
        return bTime;
    }

    public void setSpeed(float s){
        speed = s;
    }
    
}
