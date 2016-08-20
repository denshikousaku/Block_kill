using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Hamster : MonoBehaviour
{
    private string yoko = "yoko2P";
    private KeyCode sannkaku = KeyCode.Joystick2Button0;
    private KeyCode maru = KeyCode.Joystick2Button1;//突進のボタン
    private KeyCode batu = KeyCode.Joystick2Button2;
    private KeyCode shikaku = KeyCode.Joystick2Button3;

    private GameObject _hamster;
    public GameObject _camera;
    public GameObject _gost;

    public GameObject _GAMEOVER;

    private bool _Right = false;
    private bool _fallattack = false;

    private List<GameObject> _colList = new List<GameObject>();

    private Animator _animator;

    // Use this for initialization
    void Start()
    {
        _hamster = gameObject;
        _hamster.name = "Hamster";
        _animator = _hamster.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = _hamster.transform.position;
        //カメラをキャラクターに追従させる
        //_camera.transform.position = new Vector3(position.x,position.y,-10);

        //横矢印キーで移動とアニメーション
        if (Input.GetAxisRaw(yoko) > 0 || Input.GetAxisRaw(yoko) < 0)
        {
            movecharacter();
        }

        //スペースキーでジャンプ
        if ((Input.GetKeyDown(batu)) && _colList.Count != 0)
        {
            _hamster.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
        }

        //Sボタンで突進
        if(Input.GetKeyDown(maru) && _gost.activeSelf == false && _colList.Count != 0)
        {
            if(Input.GetAxisRaw(yoko) > 0 || _Right == true)
            {
                _hamster.GetComponent<Rigidbody2D>().velocity = new Vector2(50,0);
            }
            else if(Input.GetAxisRaw(yoko) < 0 || _Right == false)
            {
                _hamster.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
            }
            _gost.SetActive(true);
            Invoke("rashend", 0.5f);
        }

        //ジャンプ中にDを押すと、落下攻撃
        if((Input.GetKeyDown(KeyCode.D)) && _colList.Count == 0 && _gost.activeSelf == false)
        {
            _hamster.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -60);
            _gost.SetActive(true);
            _fallattack = true;
        }

        //落下死
        if (position.y < -25)
        {
            GAMEOVER();
        }
    }

    //キャラクターを動かす
    private void movecharacter()
    {
        var position = _hamster.transform.position;
        if (Input.GetAxisRaw(yoko) > 0)//右か左か
        {
            position.x += 0.5f;
            _Right = true;
        }
        else if (Input.GetAxisRaw(yoko) < 0)
        {
            position.x -= 0.5f;
            _Right = false;
        }

        _animator.SetBool("Right", _Right);

        transform.position = position;
    }

    //突進攻撃を終了する
    private void rashend()
    {
        _hamster.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        _gost.SetActive(false);
    }

    //prefabのインスタンス  //refの使い方がイマイチ分からない
    private void PrefabInstance(GameObject prefab, ref GameObject gameobject, float X, float Y)
    {
        var position = _hamster.transform.position;
        gameobject = Instantiate(prefab, new Vector2(position.x + X, position.y + Y), Quaternion.identity) as GameObject;
        gameobject.name = prefab.name;
    }

    //ゲームオーバーの処理
    private void GAMEOVER()
    {
        Destroy(GameObject.Find("HP_enemy"));
        var position = _hamster.transform.position;
        var gameover = Instantiate(_GAMEOVER, new Vector2(position.x, position.y), Quaternion.identity);
        gameover.name = _GAMEOVER.name;
    }

    //当たる処理
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "BombBlock")
        {
            _colList.Add(col.gameObject);
        }

        if(_fallattack == true)
        {
            _gost.SetActive(false);
            _fallattack = false;
        }
        //ダメージに関する処理は、ダメージを与えた側がメッセージを送ると良い
    }

    //離れる処理
    private void OnCollisionExit2D(Collision2D col)
    {
        _colList.Remove(col.gameObject);
    }
}
