using UnityEngine;
using System.Collections;

public class BombBlock : MonoBehaviour {

    public GameObject _BombBlock;
    public GameObject _burnP;
    private GameObject _burnO;

	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name != "eraserblock")
        {
            //爆発の底辺をボムブロックの底辺に合わせている
            var posi = _BombBlock.transform.position;
            float burnheight = _burnP.GetComponent<SpriteRenderer>().bounds.size.y;
            float Bombheight = _BombBlock.GetComponent<SpriteRenderer>().bounds.size.y;
            float substruct = (burnheight / 2)-(Bombheight / 2);
            /*Debug.Log(burnheight);
            Debug.Log(Bombheight);*/

            _burnO = Instantiate(_burnP, new Vector2(posi.x, posi.y + substruct - 0.05f*substruct   ), Quaternion.identity) as GameObject;
            _burnO.name = _burnP.name;

            //爆発で吹っ飛ぶ
            if(col.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                var ColObjectPosition = col.gameObject.transform.position;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(ColObjectPosition.x - posi.x,ColObjectPosition.y - posi.y) * 1000);
            }
            Destroy(_burnO, 1);
            Destroy(_BombBlock);
            //キャラクターがBombブロックに触れているときに爆発すると、新しいブロックを出せなくなる
        }
    }
}
