using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    public GameObject[] target;
    public TextMeshProUGUI win;
    public TextMeshProUGUI bHp;
    public TextMeshProUGUI bB;
    public float speed;
    private Rigidbody rb;
    private Random rnd = new Random();
    private int hp=10;
    private int current=0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = 10;
        target[0] = GameObject.Find("/Target1");
        target[1] = GameObject.Find("/Target2");
        target[2] = GameObject.Find("/Target3");
        target[3] = GameObject.Find("/Target4");
        print(GameObject.Find("/Canvas/GameOver").GetComponent<TextMeshProUGUI>());
        win = GameObject.Find("/Canvas/GameOver").GetComponent<TextMeshProUGUI>();
        bHp = GameObject.Find("/Canvas/BossHP").GetComponent<TextMeshProUGUI>();
        bB = GameObject.Find("/Canvas/BossBounce").GetComponent<TextMeshProUGUI>();
        bB.text = Random.Range(0,6).ToString();
        bHp.text = hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 4){
            if (transform.position != target[current].transform.position){
                Vector3 pos = Vector3.MoveTowards(transform.position, target[current].transform.position, speed*Time.deltaTime);
                rb.MovePosition(pos);
            }else{
                current = (current + 1) % target.Length;
            }
        }

    }

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Bullet(Clone)")
        {
            //print(col.gameObject.GetComponent<BounceBullet>().getbTime());
            print(col.gameObject.GetComponent<BounceBullet>().getbTime());
            //if((col.gameObject.GetComponent<BounceBullet>().getbTime()-1).ToString()==bB.text){
            if((col.gameObject.GetComponent<BounceBullet>().getbTime()).ToString()==bB.text){
                hp = hp - 1;
                bHp.text = hp.ToString();
                bB.text = Random.Range(0,6).ToString();    
                if(hp<=0){
                    Destroy(gameObject);
                    Destroy(col.gameObject);
                    win.text = "WIN !!!";
                }
            }
        }
    }
}
