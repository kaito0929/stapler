using UnityEngine;
using System.Collections;

//==============================================================
//メリーゴーランドの動きを制御するスクリプト
//==============================================================

public class MerryMove : MonoBehaviour {


    // 変数宣言----------------------------------------------------------------------

    //移動する方向
    private enum MoveDir
    {
        RIGHT,
        LEFT,
    }
    //enumを変数として宣言
    //この変数を使って移動する方向を決める
    private MoveDir moveDir;

    //このベクトルを加算か減算して左右に動かす
    private Vector3 pos;

    //ホッチキスによって止められていないかのフラグ
    private bool MoveStopFlag;

    //四角い点線のオブジェクト
    public GameObject CollPoint;

    //正解した時の音を再生するためのフラグ
    private bool SoundFlag;


    // Use this for initialization
    void Start()
    {
        moveDir = MoveDir.RIGHT;
        MoveStopFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        GimmickParent gimmick = gameObject.GetComponent<GimmickParent>();
        MoveStopFlag = gimmick.GetTapFlag();

        pos = gameObject.transform.position;

        if (MoveStopFlag == false)
        {
            switch (moveDir)
            {
                case MoveDir.RIGHT://右方向へ移動

                    if (MoveStopFlag == false)
                    {
                        //x座標を加算して右方向へ移動
                        pos.x += 0.1f;
                    }
                    //画面の右端へたどり着くと進行方向を変更
                    if (pos.x >= 16.5f)
                    {
                        moveDir = MoveDir.LEFT;
                    }

                    break;
                case MoveDir.LEFT://左方向へ移動

                    if (MoveStopFlag == false)
                    {
                        //x座標を減算して左方向へ移動
                        pos.x -= 0.1f;
                    }
                    //画面の左端へたどり着くと進行方向を変更
                    if (pos.x <= 10.5f)
                    {
                        moveDir = MoveDir.RIGHT;
                    }

                    break;
            }

        }
        else
        {
            CollPoint.SetActive(false);
        }

        if(MoveStopFlag==true)
        {
            if (SoundFlag == true)
            {
                AudioManager.Instance.PlaySE("correct1_01");
                SoundFlag = false;
            }
        }
        else
        {
            SoundFlag = true;
        }

        gameObject.transform.position = pos;
    }
}
