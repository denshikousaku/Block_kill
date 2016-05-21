using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Charactor : MonoBehaviour {

    protected KeyCode Leftkey;
    protected KeyCode RightKey;
    protected KeyCode JumpKey;
    public GameObject _charactor;
    public GameObject _GAMEOVER;
    public bool _isLeftMove = false;
    public List<GameObject> _colList = new List<GameObject>();
    public Animator _animator;

    // Update is called once per frame
    protected void Update()
    {
        var position = _charactor.transform.position;

        //横矢印キーで移動とアニメーション
        if (Input.GetKey(RightKey) || Input.GetKey(Leftkey))
        {
            movecharacter();
        }

        //スペースキーでジャンプ
        //スペースキーを押した瞬間
        if (Input.GetKey(JumpKey) && _colList.Count != 0)
        {
            _charactor.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
        }

        //落下死

        if (position.y < -25)
        {
            Destroy(GameObject.Find("HP"));
            _GAMEOVER.GetComponent<GAMEOVERscript>().falldead(_charactor);
        }
    }

    //キャラクターを移動させる
    public void movecharacter()
    {
        var position = _charactor.transform.position;
        if (Input.GetKey(RightKey))
        {
            position.x += 0.3f;
            _isLeftMove = false;
        }
        else if (Input.GetKey(Leftkey))
        {
            position.x -= 0.3f;
            _isLeftMove = true;
        }

        _animator.SetBool("isLeftMove", _isLeftMove);

        transform.position = position;
    }

    //prefabのインスタンス  //refの使い方がイマイチ分からない
    public void PrefabInatance(GameObject prefab, ref GameObject gameobject, float X, float Y)
    {
        var position = _charactor.transform.position;
        gameobject = Instantiate(prefab, new Vector2(position.x + X, position.y + Y), Quaternion.identity) as GameObject;
        gameobject.name = prefab.name;
    }

    //ゲームオーバーの処理
    public void GAMEOVER()
    {
        Destroy(GameObject.Find("HP"));
        _GAMEOVER.GetComponent<GAMEOVERscript>().thiunthiun(_charactor);
    }

    //当たる処理
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name != "BombBlock")
        {
            _colList.Add(col.gameObject);
        }
    }

    //離れる処理
    public void OnCollisionExit2D(Collision2D col)
    {
        _colList.Remove(col.gameObject);
    }
}
