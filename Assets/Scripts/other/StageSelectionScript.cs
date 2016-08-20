using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageSelectionScript : MonoBehaviour {

    private string tate1P = "tate1P";
    private KeyCode maru1P = KeyCode.Joystick1Button1;

    private GameObject[] StageIcon = new GameObject[2];
    private GameObject pointer;
    private GameObject StageImage;
    private int select = 0;
    private GameObject SelectStage;
    private GameObject battleConfiguration;
    // Use this for initialization
    void Start () {
        pointer = GameObject.Find("pointer");
        StageIcon[0] = GameObject.Find("GameScene001");
        StageIcon[1] = GameObject.Find("NewScene");
        StageImage = GameObject.Find("StageImage");
        pointer.GetComponent<RectTransform>().position = StageIcon[0].GetComponent<RectTransform>().position;
        battleConfiguration = GameObject.Find("シーン間データ共有");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw(tate1P) > 0 && select < 1)
        {
            select++;
            StageImage.GetComponent<Image>().sprite = StageIcon[select].GetComponent<Image>().sprite;
            pointer.GetComponent<RectTransform>().position = StageIcon[select].GetComponent<RectTransform>().position;
        }
        if (Input.GetAxisRaw(tate1P) < 0 && select > 0)
        {
            select--;
            StageImage.GetComponent<Image>().sprite = StageIcon[select].GetComponent<Image>().sprite;
            pointer.GetComponent<RectTransform>().position = StageIcon[select].GetComponent<RectTransform>().position;
        }
        if (Input.GetKeyDown(maru1P))
        {
            SelectStage = StageIcon[select];
            Debug.Log(SelectStage.name);
            battleConfiguration.GetComponent<BattleSetting>().GetStage(SelectStage.name);
        }
    }
}
