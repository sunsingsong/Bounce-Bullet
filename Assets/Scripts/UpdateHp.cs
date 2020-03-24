using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHp : MonoBehaviour
{
    public TextMeshProUGUI over;
    private TextMeshProUGUI textMesH;
    private int hp=10;

    // Start is called before the first frame update
    void Start()
    {
        textMesH = GetComponent<TextMeshProUGUI>();
        hp = 10;
    }

    public void changeHp(int i){
        hp = i;
        textMesH.text = hp.ToString();
        if (hp<=0){
            over.text = "Game Over";
        }
    }
}