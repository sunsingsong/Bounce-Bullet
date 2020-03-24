using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI Hp;
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    public TextMeshProUGUI win;
    //public GameObject HP;

    private Rigidbody rb;
    private float startTime = 0f;
    private float holdTime = 5.0f;
    private float timeDiff = 0f;
    private int hp = 10;
    private bool pressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Hp = GameObject.Find("/Canvas/HP").GetComponent<TextMeshProUGUI>();
        win = GameObject.Find("/Canvas/GameOver").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&(!pressed))
        {
            startTime = Time.time;
            pressed = true;
        }
        if(Input.GetKeyUp(KeyCode.Space)&pressed)
        {
            timeDiff = Time.time - startTime;
            Debug.Log(timeDiff);
            if(timeDiff <= 0.3f){
                Shooting(6.0f);
            }else if(timeDiff <= 0.6f){
                Shooting(9.0f);
            }else if(timeDiff <= 0.9f){
                Shooting(12.0f);
            }else if(timeDiff < 1.5f){
                Shooting(15.0f);
            }else if(timeDiff >= 1.5f){
                Shooting(18.0f);
            }
            pressed = false;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        transform.Rotate(0,moveHorizontal*Time.deltaTime*180.0f,0);
        transform.Translate(0,0,moveVertical*Time.deltaTime*3.5f);
    }

    public void Shooting(float speed)
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab,bulletSpawner.position,bulletSpawner.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*speed;
        bullet.GetComponent<BounceBullet>().setSpeed(speed);
        //Destroy(bullet,2);
    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Bullet(Clone)"){
            hp = hp-1;
            Hp.text = hp.ToString();
            if (hp<=0){
                win.text = "Game Over";
            }
            //HP.GetComponent<UpdateHp>().changeHp(hp);
        }
    }

}
