using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

//ゲームオーバー
public class GAMEOVERscript : MonoBehaviour {

    //変数
    string _tate = "tate1P";

    public GameObject _GAMEOVER;

    public Text _gameend;

    private GameObject _thiunthiunP;
    private GameObject _thiunthiunO;

    private bool _windowopen = false;

    private GameObject _retry;
    private GameObject _title;
    private GameObject _pointer;

    int _RETRY = 0;

    // Use this for initialization
    void Start()
    {
    }

    public void thiunthiun(GameObject deadobject) {
        //一連の処理のための準備
        var deadposition = deadobject.transform.position;
        _GAMEOVER.transform.position = deadposition;
        //_charactor.GetComponent<charactor>().enabled = false;

        //キャラクターのHP0
        Destroy(deadobject);
        _thiunthiunP = (GameObject)Resources.Load("thiunthiun");
        _gameend.text = "ハムスターの勝ち";
        _gameend.color = Color.red;

        _GAMEOVER.GetComponent<AudioSource>().Play();
        //ティウンティウンのエフェクト
        //+ 十字方向　 * 斜め方向　 P 位置   F 力　 大文字 外側　 小文字 内側
        //数字を変数  +P +F   +p  +f  *P   *F   *p   *f
        float CROSSP = 3, CROSSF = 500, crossp = 1.8f, crossf = 300;
        float NANAMEP = 2.4f, NANAMEF = 300, nanamep = 1.44f, nanamef = 180;

        _create(0, CROSSP, Vector2.up * CROSSF);//上
        _create(0, crossp, Vector2.up * crossf);

        _create(NANAMEP, NANAMEP, Vector2.up * NANAMEF);//右上
        _move(Vector2.right * NANAMEF);
        _create(nanamep, nanamep, Vector2.up * nanamef);//右上
        _move(Vector2.right * nanamef);

        _create(CROSSP, 0, Vector2.right * CROSSF);
        _create(crossp, 0, Vector2.right * crossf);

        _create(NANAMEP, -NANAMEP, Vector2.right * NANAMEF);
        _move(Vector2.down * NANAMEF);
        _create(nanamep, -nanamep, Vector2.right * nanamef);
        _move(Vector2.down * nanamef);

        _create(0, -CROSSP, Vector2.down * CROSSF);
        _create(0, -crossp, Vector2.down * crossf);

        _create(-NANAMEP, -NANAMEP, Vector2.down * NANAMEF);
        _move(Vector2.left * NANAMEF);
        _create(-nanamep, -nanamep, Vector2.down * nanamef);
        _move(Vector2.left * nanamef);

        _create(-CROSSP, 0, Vector2.left * CROSSF);
        _create(-crossp, 0, Vector2.left * crossf);

        _create(-NANAMEP, NANAMEP, Vector2.left * NANAMEF);
        _move(Vector2.up * NANAMEF);
        _create(-nanamep, nanamep, Vector2.left * nanamef);
        _move(Vector2.up * nanamef);
        //Tagを使ってthiunthiunオブジェクトをまとめて削除したい

        //ウィンドウを表示し、その上に「もう一度遊ぶ」・「タイトルに戻る」のテキストと、選択状態を示すポインタを表示する
        Invoke("windowopen",1);
    }

    public void falldead(GameObject deadobject)
    {
        //キャラクターの落下死
        _GAMEOVER.GetComponent<AudioSource>().Play();
        Destroy(deadobject);
        _gameend.text = "ハムスターの勝ち";
        _gameend.color = Color.red;

        //ウィンドウを表示し、その上に「もう一度遊ぶ」・「タイトルに戻る」のテキストと、選択状態を示すポインタを表示する
        Invoke("windowopen", 1);
    }

    // Update is called once per frame
    void Update () {

        //もう一度遊ぶか、タイトルに戻るかを選択する
        if (_windowopen == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw(_tate) < 0)
            {
                select(_retry, 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw(_tate) > 0)
            {
                select(_title, 2);
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                SceneLoad();
            }
        }
    }

    private void _create(float posix,float posiy,Vector2 force)
    {
        var posiGAMEOVER = _GAMEOVER.transform.position;
        _thiunthiunO = Instantiate(_thiunthiunP, new Vector2(posiGAMEOVER.x + posix,posiGAMEOVER.y + posiy), Quaternion.identity) as GameObject;
        _thiunthiunO.name = _thiunthiunP.name;
        _thiunthiunO.GetComponent<Rigidbody2D>().AddForce(force);
    }

    private void _move(Vector2 force)
    {
        _thiunthiunO.GetComponent<Rigidbody2D>().AddForce(force);
    }

    private void windowopen()
    {
        Image _window = GameObject.Find("window").GetComponent<Image>();
        _window.enabled = true;

        _retry = GameObject.Find("retry");
        _retry.GetComponent<Text>().text = "もう一度遊ぶ";

        _title = GameObject.Find("title");
        _title.GetComponent<Text>().text = "タイトルに戻る";

        _pointer = GameObject.Find("pointer");
        _pointer.GetComponent<Image>().enabled = true;

        _windowopen = true;
    }

    private void select(GameObject gameobject,int jyoutai)
    {
        var posipointer = _pointer.GetComponent<RectTransform>().position;
        posipointer.y = gameobject.GetComponent<RectTransform>().position.y;
        _pointer.GetComponent<RectTransform>().position = posipointer;
        _RETRY = jyoutai;
    }

    private void SceneLoad()
    {
        if (_RETRY == 1)
        {
            SceneManager.LoadScene("GameScene001");
        }
        else if (_RETRY == 2)
        {
            SceneManager.LoadScene("title_scene");
        }
    }

}
