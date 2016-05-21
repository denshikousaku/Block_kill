using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Brockman : MonoBehaviour
{
    //キー設定
    string yoko = "yoko1P";
    private KeyCode _jamp = KeyCode.JoystickButton2;
    private KeyCode _blockbutton = KeyCode.JoystickButton1;
    private KeyCode _saverbutton = KeyCode.JoystickButton3;

    public GameObject _camera;
    public GameObject _charactor;

    public int _blockselect = 1;
    private GameObject _blockPrefab;
    private GameObject _RiftingBlock;   //持ち上げているブロックを格納する
    private List<GameObject> _colblockList = new List<GameObject>();

    public GameObject _blockPrefab2;
    public GameObject _blockPrefab3;
    public GameObject _blockPrefab4;

    public GameObject _baseballprefab;

    public GameObject _Lightsaver;
    int _SlashDirection;

    public GameObject _GAMEOVER;

    private bool _isLeftMove = false;

    private List<GameObject> _colList = new List<GameObject>();

    public Animator _animator;

    // Use this for initialization
    void Start()
    {
        switch (_blockselect)
        {
            case 1:
                _blockPrefab = (GameObject)Resources.Load(BlockNames.DefaultName);
                break;
            case 2:
                _blockPrefab = (GameObject)Resources.Load(BlockNames.BombBlockName);
                break;
            case 3:
                _blockPrefab = (GameObject)Resources.Load(BlockNames.EraserBrockName);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var position = _charactor.transform.position;
        //カメラをキャラクターに追従させる
        //_camera.transform.position = new Vector3(position.x,position.y,-10);

        //横矢印キーで移動とアニメーション
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw(yoko) > 0 || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw(yoko) < 0)
        {
            movecharacter();
        }

        //スペースキーでジャンプ
        //スペースキーを押した瞬間
        if ((Input.GetKey(KeyCode.Space) || Input.GetKeyDown(_jamp)) && _colList.Count != 0)
        {
            _charactor.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
        }

        //Bボタンを押すと、ブロックに対して処理をする
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(_blockbutton))
        {
            BlockControl(_blockPrefab2,ref _RiftingBlock);
        }

        //Nボタンを押すと、ブロックに対して処理をする
        if (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(_blockbutton))
        {
            BlockControl(_blockPrefab3,ref _RiftingBlock);
        }

        //Mボタンを押すと、ブロックに対して処理をする
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(_blockbutton))
        {
            BlockControl(_blockPrefab,ref _RiftingBlock);
        }
        //Gボタンを押すと、ブロックに対して処理をする
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(_blockbutton))
        {
            BlockControl(_blockPrefab4, ref _RiftingBlock);
        }

        //キャラクターの頭上にブロックを追従させる
        if (_RiftingBlock != null)
        {
            blockfollow(0, 5f);
        }

        //Hボタンを押すと、野球ボールに対して処理をする
        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(_blockbutton))
        {
            BlockControl(_baseballprefab, ref _RiftingBlock);
        }

        //Kキーを押すとライトセイバーで斬りつける
        if ((Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(_saverbutton)) && _Lightsaver.activeSelf == false)
        {
            slash(position.x,position.y);
        }

        if(_Lightsaver.activeSelf == true)
        {
            //セイバーを振る範囲が決まっているので、角度で判定すべきである
            if ((_SlashDirection == 1 && _Lightsaver.transform.eulerAngles.z > 31  && _Lightsaver.transform.eulerAngles.z < 250) || (_SlashDirection == -1 && _Lightsaver.transform.eulerAngles.z < 329 && _Lightsaver.transform.eulerAngles.z > 120))
            {
                _Lightsaver.SetActive(false);
                _Lightsaver.GetComponent<Rigidbody2D>().angularVelocity = 0;
                _Lightsaver.transform.eulerAngles = new Vector3(0, 0, 0);
                _Lightsaver.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                _Lightsaver.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        //落下死
        if (position.y < -25)
        {
            GAMEOVER();
        }
    }

    //キャラクターを移動させる
    private void movecharacter()
    {
        var position = _charactor.transform.position;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw(yoko) > 0)
        {
            position.x += 0.3f;
            _isLeftMove = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw(yoko) < 0)
        {
            position.x -= 0.3f;
            _isLeftMove = true;
        }

        _animator.SetBool("isLeftMove", _isLeftMove);

        transform.position = position;
    }

    //ブロックに対して条件に応じた処理を行う関数
    private void BlockControl(GameObject _blockPrefab,ref GameObject _RiftingBlock)
    {
        //ブロックを持つ
        if (_RiftingBlock == null)
        {
            //Bキーでブロックを出す   //色々な種類のブロックを出せるようにしたい
            if (_colblockList.Count == 0)
            {
                PrefabInatance(_blockPrefab, ref _RiftingBlock, 0, 4.5f);
            }
            //Bキーで隣接するブロックを持ち上げる
            else
            {
                _RiftingBlock = _colblockList[_colblockList.Count - 1];
                _colList.Remove(_RiftingBlock);
                _colblockList.Remove(_RiftingBlock);
            }
            _RiftingBlock.GetComponent<Collider2D>().enabled = false;
        }
        //ブロックを手離す
        else
        {
            _RiftingBlock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw(yoko) > 0 || Input.GetAxisRaw(yoko) < 0) //投げるか、置くか
            {
                float ThrowVelocity = 20;
                if (_isLeftMove == false) //右に投げる
                {
                    blockfollow(5, 3);
                    _RiftingBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowVelocity, ThrowVelocity);   //.AddForce((Vector2.right + Vector2.up) * 3000f);
                }
                else//左に投げる
                {
                    blockfollow(-5, 3);
                    _RiftingBlock.GetComponent<Rigidbody2D>().velocity = new Vector2(-ThrowVelocity, ThrowVelocity);
                }
            }
            else
            {
                if (_isLeftMove == false)  //右に置く
                {
                    blockfollow(5, -0.5f);
                }
                else//左に置く
                {
                    blockfollow(-5, -0.5f);
                }
            }
            _RiftingBlock.GetComponent<Collider2D>().enabled = true;
            _RiftingBlock = null;
        }
    }

    //ブロックの位置をキャラクターの位置を基準に変更する
    private void blockfollow(float X, float Y)
    {
        _RiftingBlock.transform.position = _charactor.transform.position + new Vector3(X, Y, 0);
    }

    //prefabのインスタンス  //refの使い方がイマイチ分からない
    private void PrefabInatance(GameObject prefab, ref GameObject gameobject, float X, float Y)
    {
        var position = _charactor.transform.position;
        gameobject = Instantiate(prefab, new Vector2(position.x + X, position.y + Y), Quaternion.identity) as GameObject;
        gameobject.name = prefab.name;
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
        if (_RiftingBlock.name == "baseball")
        {
            _RiftingBlock.transform.position += new Vector3(_SlashDirection * 3,3,0);
            _RiftingBlock.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _RiftingBlock.GetComponent<Collider2D>().enabled = true;
            _RiftingBlock = null;
        }
    }

    //ゲームオーバーの処理
    private void GAMEOVER()
    {
        Destroy(GameObject.Find("HP"));
        var position = _charactor.transform.position;
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

        if (col.gameObject.name == "block" || col.gameObject.name == "throwblock")
        {
            _colblockList.Add(col.gameObject);
        }
    }

    //離れる処理
    private void OnCollisionExit2D(Collision2D col)
    {
        _colList.Remove(col.gameObject);

        if (col.gameObject.name == "block" || col.gameObject.name == "throwblock")
        {
            _colblockList.Remove(col.gameObject);
        }
    }
}

public class BlockNames
{
    static private string defaultName = "block";
    static public string DefaultName
    {
        get
        {
            return defaultName;
        }
    }
    static private string bombBlockName = "BombBlock";
    static public string BombBlockName
    {
        get
        {
            return bombBlockName;
        }
    }
    static private string eraserbrockName = "eraserblock";
    static public string EraserBrockName
    {
        get
        {
            return eraserbrockName;
        }
    }
}

/*
ブロックを持っていないときにbを押すと、新しいブロックを作って持ち上げる
ブロックを持っているときに、bを押すと、ブロック置くまたは投げる
キャラクターがブロックに隣接しているときにbを押すと、そのブロックを持ち上げる

ブロックを持っている    _RiftingBlock != null
ブロックに触れている    _colblockList.Count != 0
bを押す                 _Input.GetKeyDown("b")
*/
