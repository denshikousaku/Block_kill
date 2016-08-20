﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HP : MonoBehaviour{

    public int MaxHP;
    private int NowHP;
    private GameObject HPtext;
    private GameObject _GAMEOVER;

	// Use this for initialization
	void Start () {
        NowHP = MaxHP;
        HPtext = GameObject.Find(gameObject.name + "HP");
        HPtext.GetComponent<Text>().text = "HP " + NowHP.ToString();
        _GAMEOVER = GameObject.Find("GAMEOVER");
    }

    //最大HPを増減させる
    public void MaxHPmanage()
    {

    }

    //現在のHPを増減させる //ダメージと回復は分けなくてよいか
    public void AddDamage(int damage)
    {
        //他のオブジェクトの防御力の値によって、ダメージ量を変えることもできる
        NowHP -= damage;
        if(NowHP > 0)
        {
            HPtext.GetComponent<Text>().text = "HP " + NowHP.ToString();
        }
        else if(NowHP <= 0)
        {
            NowHP = 0;
            HPtext.GetComponent<Text>().text = "HP " + NowHP.ToString();
            _GAMEOVER.GetComponent<GAMEOVERscript>().thiunthiun(gameObject);
        }
    }
}
