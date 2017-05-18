using UnityEngine;
using System.Collections;

public class BallMove : MonoBehaviour {

    //===========================================================================
    //ボールを移動させるためのスクリプト
    //画面外に出たならば初期位置に戻すようにもする
    //===========================================================================

    // 変数宣言----------------------------------------------------------------------

    //このベクトルを加算か減算して左右に動かす
    private Vector3 pos;

    //ボールと敵が親子関係になったかを取得するために宣言
    public GimmickParent tap;

    // 画面外に出た時の処理
    void OnBecameInvisible()
    {
        //敵と親子関係になっていなければ画面の右側へと移動
        if (tap.GetTapFlag() == false)
        {
            gameObject.transform.position = new Vector3(8f, -1.27f, gameObject.transform.position.z);
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //UFOの現在値をposへ代入
        pos = gameObject.transform.position;

        //x座標を減算して左方向へ移動
        pos.x -= 0.1f;

        //UFOのpositionへposを代入
        gameObject.transform.position = pos;
    }
}
