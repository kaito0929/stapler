﻿using UnityEngine;
using System.Collections;

public class EnemyAction : MonoBehaviour {

    //==================================================================
    //ステージ3フロア2の敵の行動を制御するスクリプト
    //岩がタップされた時に骨を投げるアニメーションの再生
    //左右への移動
    //==================================================================

    // 変数宣言----------------------------------------------------------------------

    //骨を投げるアニメーションを取得
    private Animator ThrowAnim;

    //岩をタップしたかのフラグを受け取ってアニメーションを再生させるフラグ
    private bool PlaybackFlag;
    //タップされたかのフラグを持つオブジェクト
    public GameObject Rock;

    //移動する方向
    private enum EnemyMoveDir
    {
        RIGHT,
        LEFT,
    }
    //この変数を使って移動する方向を決定
    private EnemyMoveDir moveDir;

    //ベクトルを加算、減算して移動
    private Vector3 EnemyMoveVec;

    //敵が移動できるかのフラグ
    private bool EnemyMoveFlag;

    //敵が止められている間の時間を取得する変数
    private float EnemyStopTime;

    //敵が止まっている最大の時間
    private float MaxStopTime;

    //骨のアニメーションが再生中かのフラグを受け取るための変数
    public GameObject bone;
    private bool BoneAnimFlag;

    //Ray関係
    //ホッチキスの針を付けるために必要
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    // Use this for initialization
    void Start () {
        ThrowAnim = GetComponent<Animator>();
        PlaybackFlag = false;

        moveDir = EnemyMoveDir.RIGHT;

        EnemyMoveFlag = true;
        BoneAnimFlag = false;

        MaxStopTime = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        ThrowAnimPlay();
        EnemyMovePos();
        EnemyTap();
	}

    //骨を投げるアニメーションを再生する関数
    void ThrowAnimPlay()
    {
        //岩がタップされたかのフラグを取得
        RockTap rocktap = Rock.GetComponent<RockTap>();
        PlaybackFlag = rocktap.GetRockTapFlag();

        //フラグがtrueならば再生
        ThrowAnim.SetBool("Action", PlaybackFlag);
    }



    //敵の移動を制御する関数
    void EnemyMovePos()
    {
        BoneStop boneStop = bone.GetComponent<BoneStop>();
        BoneAnimFlag = boneStop.GetBoneAnimPlayFlag();

        //タップされていなかったら移動する
        if (EnemyMoveFlag == true&&BoneAnimFlag==false)
        {
            //敵の移動する方向を変えて移動させる
            switch (moveDir)
            {
                case EnemyMoveDir.RIGHT://右方向

                    //ベクトルに現在の敵の位置を代入
                    EnemyMoveVec = gameObject.transform.position;

                    //決められた位置まで移動
                    if (EnemyMoveVec.x <= 18.3)
                    {
                        EnemyMoveVec.x += 0.1f;
                    }
                    else
                    {
                        //移動し終わったら方向変換する
                        moveDir = EnemyMoveDir.LEFT;
                    }

                    //敵オブジェクトのポジションへベクトルを代入して移動
                    gameObject.transform.position = EnemyMoveVec;

                    break;

                case EnemyMoveDir.LEFT://左方向

                    //内容は上の部分と変わらないので
                    //コメントは上記を参照
                    EnemyMoveVec = gameObject.transform.position;

                    if (EnemyMoveVec.x >= 9.7)
                    {
                        EnemyMoveVec.x -= 0.1f;
                    }
                    else
                    {
                        moveDir = EnemyMoveDir.RIGHT;
                    }

                    gameObject.transform.position = EnemyMoveVec;

                    break;
            }
        }
        else
        {
            //タップされて動きが止まっていたら
            //止まっている間の時間を変数に加算
            EnemyStopTime += Time.deltaTime;
        }

        //止まっている時間がMaxStopTime(8f)を超えたら処理
        //再び敵が左右に動き出すようになる
        if(EnemyStopTime>=MaxStopTime)
        {
            EnemyMoveFlag = true;
            EnemyStopTime = 0;
            Needle.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
        }
    }

    //敵をタップして行う処理
    void EnemyTap()
    {
        if (TouchManager.SelectedGameObject == gameObject)
        {
            //フラグをfalseにして敵の動きを止める
            EnemyMoveFlag = false;

            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針の位置をタップした位置へと移動させる。
                Needle.transform.position = hit.point;
                //gameObjectと親子関係に
                Needle.transform.parent = gameObject.transform;
            }
        }
    }
}
