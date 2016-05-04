using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VacuumScript : MonoBehaviour {

    private List<GameObject> suckList = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var position = gameObject.transform.position;
        int i;
        for(i=0;i<suckList.Count;i++)
        {
            var suckposition = suckList[i].transform.position;
            Vector3 suckdirection = position - suckposition;
            suckList[i].GetComponent<Rigidbody2D>().velocity = suckdirection;
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        suckList.Add(col.gameObject);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        suckList.Remove(col.gameObject);
    }
}
