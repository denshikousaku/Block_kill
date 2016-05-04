using UnityEngine;
using System.Collections;

public class EraserblockManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 12)
        {
            Destroy(col.gameObject);
        }
    }
}
