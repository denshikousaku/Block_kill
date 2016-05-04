using UnityEngine;
using System.Collections;

public class LightSaverManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        col.gameObject.GetComponent<HP>().AddDamage(1);
    }
}
