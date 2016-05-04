using UnityEngine;
using System.Collections;

public class Hamster_gostManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        var ColObjPosition = col.gameObject.transform.position;
        var gostPosition = gameObject.transform.position;
        if (ColObjPosition.x < gostPosition.x)
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 9000f);
        }
        else
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1) * 9000f);
        }
        col.gameObject.GetComponent<HP>().AddDamage(1);
    }
}
