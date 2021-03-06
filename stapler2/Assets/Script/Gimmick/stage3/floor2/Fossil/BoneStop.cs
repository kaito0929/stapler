﻿using UnityEngine;
using System.Collections;

//===============================================================
//上方向へ投げられた骨を壁にくっつけるスクリプト
//===============================================================

public class BoneStop : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //壁の一部分に当たっているかのフラグ
    private bool WallCollFlag;

    //岩をタップしたかのフラグを受け取る変数
    private bool PlaybackFlag;
    //タップされたかのフラグを持つオブジェクト
    public GameObject Rock;

    //上に移動するアニメーションを持つ骨を格納する
    public Animation BoneAnim;
    //上記と同じオブジェクトを格納する
    public GameObject bone;

    //骨をタップして壁に止められたかのフラグ
    private bool BoneTapStopFlag;
    //骨をタップして壁に止められたかのフラグを別のスクリプトへ渡す関数
    public bool GetBoneTapStopFlag()
    {
        return BoneTapStopFlag;
    }

    //骨が放り投げられるアニメーションが再生中かのフラグ
    private bool BoneAnimPlayFlag;
    //再生中かのフラグを別のスクリプトへ渡す関数
    public bool GetBoneAnimPlayFlag()
    {
        return BoneAnimPlayFlag;
    }


    //骨を止める場所に設置してあるオブジェクト
    //このオブジェクトに当たっている時に骨をタップして止める
    public GameObject StopWallColl;


    private float time;

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    public Renderer rd;

    void OnTriggerEnter(Collider other)
    {
        //設置されたコライダーを持つオブジェクトに当たっている場合に処理
        if (other.gameObject == StopWallColl)
        {
            WallCollFlag = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //壁のコライダーと離れている間はフラグをfalseになるように
        WallCollFlag = false;
    }

    // Use this for initialization
    void Start () {
        WallCollFlag = false;
        BoneTapStopFlag = false;
        BoneAnim = bone.GetComponent<Animation>();
        time = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //岩がタップされたかのフラグを取得
        RockTap rocktap = Rock.GetComponent<RockTap>();
        PlaybackFlag = rocktap.GetRockTapFlag();

        //壁の一部分に当たっている場合に処理する
        if (WallCollFlag == true)
        {
            //タップしたものが骨のオブジェクトなら処理
            if (TouchManager.SelectedGameObject == gameObject)
            {
                //アニメーションをストップさせてそこに止められたように見せる
                BoneAnim.Stop();

                bone.transform.parent = StopWallColl.transform;

                if (BoneTapStopFlag == false)
                {
                    rd.GetComponent<Renderer>().material.color = new Color(255.0f / 255.0f, 143.0f / 255.0f, 247.0f / 255.0f);

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

                //止められたのでフラグをtrueにする
                BoneTapStopFlag = true;


            }
        }
        else if(BoneTapStopFlag == false)
        {
            //アニメーションの再生は壁に当たっていない状態なら
            //行うようにしておく
            if (PlaybackFlag == true)
            {
                BoneAnim.Play();
            }
        }

        //骨をタップして止めた場合に時間をtimeに加算
        if(BoneTapStopFlag==true)
        {
            if (time >= 0)
            {
                time += Time.deltaTime;
            }
        }

        //timeが1fを超えたならパーティクルの色を白に戻す
        //その際にtimeを-1fに変えておいて変数に加算させないようにする
        if (time > 1f)
        {
            //パーティクルの色を変える
            rd.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            time = -1f;
        }


        if (BoneAnim.isPlaying)
        {
            BoneAnimPlayFlag = true;
        }
        else
        {
            BoneAnimPlayFlag = false;
        }

    }
}
