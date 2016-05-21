using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("GameScene001");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("GameScene001");
        }
    }
}
