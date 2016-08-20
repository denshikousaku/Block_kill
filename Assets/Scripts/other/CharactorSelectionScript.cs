using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharactorSelectionScript : MonoBehaviour {

    private string yoko1P = "yoko1P";
    private KeyCode maru1P = KeyCode.Joystick1Button1;
    private string yoko2P = "yoko2P";
    private KeyCode maru2P = KeyCode.Joystick2Button1;


    //2はプレイヤー数
    public GameObject background;
    private GameObject[] CharactorIcon = new GameObject[2];
    private GameObject[] charactorImage = new GameObject[2];
    public GameObject[] pointer = new GameObject[2];
    private int[] select = new int[2];
    private GameObject[] SelectChara = new GameObject[2];
    private GameObject battlemanager;

    // Use this for initialization
    void Start () {
        battlemanager = GameObject.Find("シーン間データ共有");
        CharactorIcon[0] = GameObject.Find("Brockman");
        CharactorIcon[1] = GameObject.Find("Hamster");
        charactorImage[0] = GameObject.Find("1Pchara");
        charactorImage[1] = GameObject.Find("2Pchara");
        pointer[0].GetComponent<RectTransform>().position = CharactorIcon[0].GetComponent<RectTransform>().position;
        pointer[1].GetComponent<RectTransform>().position = CharactorIcon[1].GetComponent<RectTransform>().position;
        select[0] = 0;
        select[1] = 1;
        //まずは画像を読み込む
    }
	
	// Update is called once per frame
	void Update () {
        //1P選択
        charaselect(yoko1P,maru1P,1);

        //2P選択
        charaselect(yoko2P,maru2P,2);

        if(SelectChara[0] != null && SelectChara[1] != null)
        {
            SceneManager.LoadScene("StageSelection");
        }
    }

    private void charaselect(string yoko,KeyCode OK,int number)
    {
        number--;
        if (Input.GetAxisRaw(yoko) > 0 && select[number] < 1)
        {
            select[number]++;
            charactorImage[number].GetComponent<Image>().sprite = CharactorIcon[select[number]].GetComponent<Image>().sprite;
            pointer[number].GetComponent<RectTransform>().position = CharactorIcon[select[number]].GetComponent<RectTransform>().position;
        }
        if (Input.GetAxisRaw(yoko) < 0 && select[number] > 0)
        {
            select[number]--;
            charactorImage[number].GetComponent<Image>().sprite = CharactorIcon[select[number]].GetComponent<Image>().sprite;
            pointer[number].GetComponent<RectTransform>().position = CharactorIcon[select[number]].GetComponent<RectTransform>().position;
        }
        if (Input.GetKeyDown(OK))
        {
            SelectChara[number] = CharactorIcon[select[number]];
            battlemanager.GetComponent<BattleSetting>().getplayer(SelectChara[number],number);
        }
    }
}
