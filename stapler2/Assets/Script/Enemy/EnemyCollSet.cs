using UnityEngine;
using System.Collections;

public class EnemyCollSet : MonoBehaviour {

    //==========================================================================
    //ステージ1のフロア3にいるエネミーに当たり判定を付けるスクリプト
    //電線を繋げないとギミックに止められないようにするため
    //==========================================================================

    // 変数宣言----------------------------------------------------------------------

    //電線が繋がったかのフラグを受け取る変数
    private bool GetWireConnectFlag;
    //電線のオブジェクト
    //フラグを受け取るために使う
    public GameObject ElectricalWire;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        GetWireConnectFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

        //電線が繋がったかのフラグを受け取る
        ElectricalConnect connect = ElectricalWire.GetComponent<ElectricalConnect>();
        GetWireConnectFlag = connect.GetConnectFlag();

        //電線が繋がったのならば当たり判定のフラグをtrueにしておいてタップ可能に
        if(GetWireConnectFlag==true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

	}
}
