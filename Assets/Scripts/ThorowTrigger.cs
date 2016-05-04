using UnityEngine;
using System.Collections;

public class ThorowTrigger : MonoBehaviour {

    public GameObject _housewife;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        _housewife.GetComponent<HouseWife>().setThrowObject(col.gameObject);

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _housewife.GetComponent<HouseWife>().setThrowObject(null);
    }
}
