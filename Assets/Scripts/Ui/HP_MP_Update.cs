using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class HP_MP_Update : MonoBehaviour
{
    public Slider hp_slider;
    public Slider mp_slider;
    //public Slider exp_slider;
    public TextMeshProUGUI hp_text;
    public TextMeshProUGUI mp_text;
    //public Text exp_text;
    public Camera cam;
    //public Stats stats;
    public int maxHp = 100;
    public int maxMp = 100;    
    public int hp = 70;
    public int mp = 30;

    void Start()
    {
        cam = Camera.main;
        //stats = cam.GetComponent<Camera_Work>().player.GetComponent<Stats>(); 
        // �÷��̾��� ���ݽ�ũ��Ʈ���� ���� �ҷ���.
        hp_slider = GameObject.Find("hp_slider").GetComponent<Slider>();
        mp_slider = GameObject.Find("mp_slider").GetComponent<Slider>();
        hp_slider.minValue = 0;
        mp_slider.minValue = 0;
    }

    void Update()
    {
        //hp_slider.maxValue = stats.maxHp;//�����̴��� �ִ밪�� ������ �ִ�ü������ ����
        //mp_slider.maxValue = stats.maxMp;//�����̴��� �ִ밪�� ������ �ִ븶���� ����
        //hp_slider.value = stats.hp;
        //mp_slider.value = stats.mp;
        //hp_text.text = (stats.hp.ToString() + "/" + stats.maxHp.ToString());
        //mp_text.text = (stats.mp.ToString() + "/" + stats.maxMp.ToString());
        
        hp_slider.maxValue = maxHp;
        mp_slider.maxValue = maxMp;
        hp_slider.value = hp;
        mp_slider.value = mp;
        hp_text.text = (hp.ToString() + "/" + maxHp.ToString());
        mp_text.text = (mp.ToString() + "/" + maxMp.ToString());


    }
}
