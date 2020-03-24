using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawner;

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Bullet(Clone)")
        {
            //print(col.gameObject.GetComponent<BounceBullet>().getbTime());
            if(col.gameObject.GetComponent<BounceBullet>().getbTime()==1){
                GameObject boss = (GameObject)Instantiate(bossPrefab,bossSpawner.position,bossSpawner.rotation);
                Destroy(gameObject);
                Destroy(col.gameObject);
            }
        }
    }
}
