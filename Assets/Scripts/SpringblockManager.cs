using UnityEngine;
using System.Collections;

public class SpringblockManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name != BlockNames.EraserBrockName && col.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            var springposition = gameObject.transform.position;
            var colposition = col.gameObject.transform.position;
            Vector2 coldirection = new Vector2(colposition.x - springposition.x,colposition.y - springposition.y);
            float kakudo = Vector2.Angle(Vector2.right, coldirection);//180度以下の値を返す
            //ベクトルのy成分が負の値の場合、角度にマイナス符号を付ける
            if(colposition.y - springposition.y < 0)
            {
                kakudo = -1 * kakudo;
            }
            //-45 <= kakudo < 45
            int force = 3000;
            if ((-45 <= kakudo && kakudo < 0) || (0 <= kakudo && kakudo < 45))
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
            }
            else if (45 <= kakudo && kakudo < 135)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * force);
            }
            else if (135 <= kakudo && kakudo < 180 || -180 <= kakudo && kakudo < -135)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * force);
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * force);
            }
            else if (-135 <= kakudo && kakudo < -45)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down * force);
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
            }
        }
    }
}
