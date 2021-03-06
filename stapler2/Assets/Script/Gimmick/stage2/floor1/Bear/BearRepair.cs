﻿using UnityEngine;
using System.Collections;

//============================================================================
//熊をタップして修理するスクリプト
//============================================================================

public class BearRepair : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //熊の表示するマテリアルを格納する変数
    public Material[] material;
    //表示するマテリアルを示す変数
    private int BearChangeNum;
    //熊を直すために必要なタップ回数
    private int BearChangeNorma;

    //熊を完全に直したかのフラグ
    private bool RepairFlag;
    //他のスクリプトへ変数を渡すための関数
    public bool GetRepairFlag()
    {
        return RepairFlag;
    }

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject[] Needle;
    private int NeedleMoveNum;


    //パーティクルの色を変化させるための変数
    public Renderer rd;

    // Use this for initialization
    void Start () {
        BearChangeNum = 0;
        BearChangeNorma = 0;
        RepairFlag = false;
        NeedleMoveNum = 0;
	}
	
	// Update is called once per frame
	void Update () {

        //タップしたオブジェクトが熊ならば処理
        //タップできる場所は敗れている部分だけ
        if (TouchManager.SelectedGameObject == gameObject)
        {
            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針の位置をタップした位置へと移動させる。
                Needle[NeedleMoveNum].transform.position = hit.point;
                //gameObjectと親子関係に
                Needle[NeedleMoveNum].transform.parent = gameObject.transform;
            }

            if (RepairFlag == false)
            {
                //タップした回数分プラス
                BearChangeNorma++;
                rd.GetComponent<Renderer>().material.color = new Color(255.0f / 255.0f, 143.0f / 255.0f, 247.0f / 255.0f);
            }
            else
            {
                //パーティクルの色を変える
                rd.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            }

            if (BearChangeNorma < 3)
            {
                NeedleMoveNum++;
            }
        }



        //三回タップしたならば熊は直ったことに
        if (BearChangeNorma == 3)
        {
            //表示するマテリアルを切り替えて
            BearChangeNum = 1;
            //完全に直せたフラグをtrueに
            RepairFlag = true;

            gameObject.transform.DetachChildren();
        }

        //表示するマテリアル
        this.GetComponent<Renderer>().material = material[BearChangeNum];

	}
}
