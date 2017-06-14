using UnityEngine;
using System.Collections;

//====================================================
//ステージ1の移動を実行するスクリプト
//====================================================

public class AliceMove_Stage1 : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floor1Gimmick;
    public GameObject Floor2Gimmick;

    //クリアしたかのフラグを受け取る変数
    private bool GetFloor1ClearFlag;
    private bool GetFloor2ClearFlag;

    public AliceMove aliceMove;

    // Use this for initialization
    void Start () {
        GetFloor1ClearFlag = false;
        GetFloor2ClearFlag = false;
    }

    // Update is called once per frame
    void Update () {
        //エネミーがタップされてフリーフォールと親子関係になったかのフラグを受け取る
        Floor1Clear touch = Floor1Gimmick.GetComponent<Floor1Clear>();
        GetFloor1ClearFlag = touch.GetClear();

        //馬とメリーゴーランドが親子関係になったかのフラグを受け取る
        GimmickParent touch2 = Floor2Gimmick.GetComponent<GimmickParent>();
        GetFloor2ClearFlag = touch2.GetTapFlag();

        aliceMove.AliceMovePos(GetFloor1ClearFlag, GetFloor2ClearFlag);

    }
}
