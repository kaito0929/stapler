using UnityEngine;
using System.Collections;

public class PandaMove : MonoBehaviour {

    //===============================================================
    //フロア2に到達したかのフラグを受け取って
    //パンダを左方向へと進行させるスクリプト
    //===============================================================

    // 変数宣言----------------------------------------------------------------------
    private Vector3 pos;

    //フロア2に到達したかのフラグを持つオブジェクト（アリス）
    public GameObject Alice;
    //アリスがフロア2に到達したかのフラグ
    private bool AliceReachingFlag;

    //別のスクリプトのフラグを受け取る変数
    public bool GetFlag;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        //オブジェクトにひっついたかのフラグを取得
        GimmickParent touch = gameObject.GetComponent<GimmickParent>();
        GetFlag = touch.GetTapFlag();

        //アリスがフロア2に到達したかのフラグを取得
        AliceMove alice = Alice.GetComponent<AliceMove>();
        AliceReachingFlag = alice.GetReachingFlag();


        if (AliceReachingFlag == true)
        {
            //パンダがタップされて時計に止められていなければ前進
            //そうでなければ当たり判定を消して動きを止める
            if (GetFlag == false)
            {
                pos = transform.position;
                pos.x -= 0.01f;
                transform.position = pos;
            }
            if (GetFlag == true)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }



    }
}
