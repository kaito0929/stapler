﻿using UnityEngine;
using System.Collections;

//===============================================================
//ステージ1・フロア1でのクリア判定をとるスクリプト
//===============================================================


public class Floor1Clear : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //タップされたかの判定を持つオブジェクト（エネミー）
    public GameObject Floar1Enemy1;
    public GameObject Floar1Enemy2;

    //タップされたかの判定を受け取る変数
    private bool[] TapFlag = new bool[2];

    //二つのフラグを受けてクリアの判定をする変数
    private bool GimmickClearFlag;
    //クリアの判定を別のスクリプトへ渡す関数
    public bool GetClear()
    {
        return GimmickClearFlag;
    }

    //正解した時の音を再生するためのフラグ
    private bool SoundFlag;

    // Use this for initialization
    void Start()
    {
        TapFlag[0] = false;
        TapFlag[1] = false;
        GimmickClearFlag = false;
        SoundFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //設置されている片方のエネミーのタップされたかのフラグを受け取る
        GimmickParent tap = Floar1Enemy1.GetComponent<GimmickParent>();
        TapFlag[0] = tap.GetTapFlag();
        //設置されている片方のエネミーのタップされたかのフラグを受け取る
        GimmickParent tap2 = Floar1Enemy2.GetComponent<GimmickParent>();
        TapFlag[1] = tap2.GetTapFlag();

        //両方のフラグがtrueになれば両方タップされたと判定がとられ
        //ステージ1はクリアとなる
        if (TapFlag[0] == true && TapFlag[1] == true)
        {
            //このフラグがステージ1をクリアしたかのフラグになっている
            GimmickClearFlag = true;
        }


        if (GimmickClearFlag == true)
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

    }
}
