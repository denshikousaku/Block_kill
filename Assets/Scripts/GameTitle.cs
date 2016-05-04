using UnityEngine;
using System.Collections;

public class GameTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (Input.GetKeyDown("space"))
        {
            Application.LoadLevel("GameScene001");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            Application.LoadLevel("GameScene001");
        }
    }
}
