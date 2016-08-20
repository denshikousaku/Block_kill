using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleSetting : MonoBehaviour {
    private GameObject[] Player = new GameObject[2];
    private GameObject[] charactor = new GameObject[2];
    private GameObject[] instance = new GameObject[2];

    public void getplayer(GameObject selectchara,int number)
    {
        Player[number] = selectchara;
        charactor[number] = (GameObject)Resources.Load(Player[number].name);
        Debug.Log(charactor[number].name);
    }

    public void GetStage(string stagename)
    {
        SceneManager.LoadScene(stagename);
        Invoke("time",0.5f);
    }

    private void time()
    {
        instance[0] = Instantiate(charactor[0],new Vector3(-10,0,0),Quaternion.identity) as GameObject;
        instance[1] = Instantiate(charactor[1],new Vector3(10, 0, 0), Quaternion.identity) as GameObject;
        //C#ファイルのディレクトリを変更したためにキャラのスクリプトがおかしくなった。
    }

    // Use this for initialization
    void Start () {
        Object.DontDestroyOnLoad(gameObject);
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update () {
    }
}
