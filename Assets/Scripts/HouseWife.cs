﻿using UnityEngine;
using System.Collections;

public class HouseWife : Charactor {

    public GameObject _Lightsaver;
    public GameObject _vacuum;
    int _SlashDirection;
    private GameObject _throwtarget;
    private bool _isthrow = false;

    public HouseWife()
    {
        RightKey = KeyCode.RightArrow;
        Leftkey = KeyCode.LeftArrow;
        JumpKey = KeyCode.Space;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	new void Update (){
        base.Update();
        var position = _charactor.transform.position;
        //Kキーを押すとライトセイバーで斬りつける
        if (Input.GetKeyDown(KeyCode.U) && _Lightsaver.activeSelf == false)
        {
            slash(position.x, position.y);
        }

        if (_Lightsaver.activeSelf == true)
        {
            //セイバーを振る範囲が決まっているので、角度で判定すべきである
            if ((_SlashDirection == 1 && _Lightsaver.transform.eulerAngles.z > 31 && _Lightsaver.transform.eulerAngles.z < 250) || (_SlashDirection == -1 && _Lightsaver.transform.eulerAngles.z < 329 && _Lightsaver.transform.eulerAngles.z > 120))
            {
                _Lightsaver.SetActive(false);
                _Lightsaver.GetComponent<Rigidbody2D>().angularVelocity = 0;
                _Lightsaver.transform.eulerAngles = new Vector3(0, 0, 0);
                _Lightsaver.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                _Lightsaver.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        //掃除機で相手を攻撃
        if(Input.GetKeyDown(KeyCode.I))
        {
            _vacuum.SetActive(true);
            if(_isLeftMove == false)
            {
                _vacuum.transform.position = position + new Vector3(3,0,0);
            }
            else
            {
                _vacuum.transform.position = position + new Vector3(-3, 0, 0);
            }
        }
        //Pキーを押すと、投げる
        //投げている途中に_throwtargetがnullになると、ハムスターが操作不能になる
        if(Input.GetKey(KeyCode.P) && _throwtarget != null)
        {
            _isthrow = true;
            _throwtarget.GetComponent<Rigidbody2D>().freezeRotation = false;
            var selfpositon = _charactor.transform.position;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _throwtarget.transform.position = new Vector3(selfpositon.x + 5,selfpositon.y,0);
                _throwtarget.GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0,0);
                _throwtarget.GetComponent<Rigidbody2D>().angularVelocity = -1000;
                
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _throwtarget.transform.position = new Vector3(selfpositon.x - 5,selfpositon.y, 0);
                _throwtarget.GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, 0);
                _throwtarget.GetComponent<Rigidbody2D>().angularVelocity = 1000;
            }
            _throwtarget.GetComponent<Hamster>().enabled = false;
            Invoke("scriptactive", 1);
        }
    }

    //投げたキャラのスクリプトを有効にする
    private void scriptactive()
    {
        _isthrow = false;
        _throwtarget.GetComponent<Hamster>().enabled = true;
        _throwtarget.GetComponent<Rigidbody2D>().freezeRotation = true;
    }

    //投げるキャラの位置をコントロールする
    private void throwing(float X, float Y)
    {
        _throwtarget.GetComponent<Rigidbody2D>().velocity = new Vector2(X,Y);
    }

    //ライトセイバーの処理
    private void slash(float CharactorPastPositionX, float CharactorPastPositionY)
    {
        _Lightsaver.SetActive(true);
        _Lightsaver.GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, -4);
        if (_isLeftMove == false)
        {
            _SlashDirection = 1;//右
        }
        else
        {
            _SlashDirection = -1;//左
        }
        _Lightsaver.transform.position = new Vector2(CharactorPastPositionX - 2 * _SlashDirection, CharactorPastPositionY + 7);
        _Lightsaver.transform.eulerAngles = new Vector3(0, 0, 30 * _SlashDirection);
        _Lightsaver.GetComponent<Rigidbody2D>().angularVelocity = -800 * _SlashDirection;
        _Lightsaver.GetComponent<Rigidbody2D>().velocity = new Vector2(15 * _SlashDirection, -23);
    }

    //投げるキャラを取得する
    private void OnTriggerEnter2D(Collider2D col)
    {
        _throwtarget = col.gameObject;
    }

    //投げるキャラを破棄
    private void OnTriggerExit2D(Collider2D col)
    {
        if(_isthrow == false)
        {
            _throwtarget = null;
        }
    }
}
