using UnityEngine;
using System.Collections;

public class HouseWife : CharactorClass {

    public GameObject _Lightsaver;
    public GameObject _vacuum;
    int _SlashDirection;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        var position = _charactor.transform.position;
        //Kキーを押すとライトセイバーで斬りつける
        if (Input.GetKeyDown(KeyCode.U) && _Lightsaver.activeSelf == false)
        {
            slash(position.x, position.y);
        }

        if (_Lightsaver.activeSelf == true)
        {
            //セイバーを振る範囲が決まっているので、角度で判定すべきである
            if ((_SlashDirection == 1 && _Lightsaver.transform.eulerAngles.z > 31 && _Lightsaver.transform.eulerAngles.z < 250) || (_SlashDirection == -1 && _Lightsaver.transform.eulerAngles.z < 329 && _Lightsaver.transform.eulerAngles.z > 120))
            {
                _Lightsaver.SetActive(false);
                _Lightsaver.GetComponent<Rigidbody2D>().angularVelocity = 0;
                _Lightsaver.transform.eulerAngles = new Vector3(0, 0, 0);
                _Lightsaver.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                _Lightsaver.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        //掃除機で相手を攻撃
        if(Input.GetKeyDown(KeyCode.I))
        {
            _vacuum.SetActive(true);
            if(_isLeftMove == false)
            {
                _vacuum.transform.position = position + new Vector3(3,0,0);
            }
            else
            {
                _vacuum.transform.position = position + new Vector3(-3, 0, 0);
            }
        }
    }

    //ライトセイバーの処理
    private void slash(float CharactorPastPositionX, float CharactorPastPositionY)
    {
        _Lightsaver.SetActive(true);
        _Lightsaver.GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, -4);
        if (_isLeftMove == false)
        {
            _SlashDirection = 1;//右
        }
        else
        {
            _SlashDirection = -1;//左
        }
        _Lightsaver.transform.position = new Vector2(CharactorPastPositionX - 2 * _SlashDirection, CharactorPastPositionY + 7);
        _Lightsaver.transform.eulerAngles = new Vector3(0, 0, 30 * _SlashDirection);
        _Lightsaver.GetComponent<Rigidbody2D>().angularVelocity = -800 * _SlashDirection;
        _Lightsaver.GetComponent<Rigidbody2D>().velocity = new Vector2(15 * _SlashDirection, -23);
    }
}
