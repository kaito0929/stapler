using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    //=======================================================================
    //アリスに合わせてカメラを移動させるスクリプト
    //=======================================================================

    // 変数宣言----------------------------------------------------------------------

    //カメラが追いかけるゲームオブジェクト
    public GameObject Player;

    //カメラとプレイヤーの位置の差分を受け取る変数
    private Vector3 Offset = Vector3.zero;

    // Use this for initialization
    void Start () {
       // Player = GameObject.FindGameObjectWithTag("Player");
        Offset = transform.position - Player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void LateUpdate()
    {
        //カメラの現在値を取得
        Vector3 newPosition = transform.position;
        //プレイヤーの現在値と差分を加算した数値を代入
        newPosition.x = Player.transform.position.x + Offset.x;
        newPosition.y = Player.transform.position.y + Offset.y;
        newPosition.z = Player.transform.position.z + Offset.z;
        //カメラを新しい位置へ移動させる
        transform.position = newPosition;
    }

}
