using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class P1 : MonoBehaviour
{
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI win;
    public TextMeshProUGUI Bb;
    public GameObject bulletPrefab;
    public Transform bulletSpawner;
    //public GameObject HP;

    private Rigidbody rb;
    private float startTime = 0f;
    private float holdTime = 5.0f;
    private float timeDiff = 0f;
    private int hp = 10;
    private int hit = 0;
    private bool pressed = false;
    private float cooldown = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Bb.text = Random.Range(0,6).ToString();
        cooldown = Time.time;
        //Hp = GameObject.Find("/Canvas/HP").GetComponent<TextMeshProUGUI>();
        //Bb = GameObject.Find("/Canvas/HP").GetComponent<TextMeshProUGUI>();
        //win = GameObject.Find("/Canvas/GameOver").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - cooldown >= 20.0f){
            Bb.text = Random.Range(0,6).ToString();
            cooldown = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.Space)&(!pressed)&((Time.time - startTime)>=0.5f))
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
             
        float moveHorizontal = Input.GetAxis("P1_horizontal");
        float moveVertical = Input.GetAxis("P1_vertical");
        
        transform.Rotate(0,moveHorizontal*Time.deltaTime*150.0f,0);
        transform.Translate(0,0,moveVertical*Time.deltaTime*3.0f);
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
        if(col.gameObject.name == "Bullet(Clone)")
        {
            if((col.gameObject.GetComponent<BounceBullet>().getbTime()).ToString()==Bb.text){
                hp = hp - 1;
                Hp.text = hp.ToString();
                Bb.text = Random.Range(0,6).ToString();
                cooldown = Time.time;
                hit = 0;
                if(hp<=0){
                    Destroy(gameObject);
                    Destroy(col.gameObject);
                    win.text = "P2 WIN !!!";
                }
            }else{
                hit = hit+1;
                if (hit >= 25){
                    hp = hp - 1;
                    Hp.text = hp.ToString();
                    hit = 0;
                }
            }
        }
    }

}
