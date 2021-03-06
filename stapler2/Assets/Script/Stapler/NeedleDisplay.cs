﻿using UnityEngine;
using System.Collections;

//================================================================
//針の弾かれるアニメーションの再生終了を取得して、
//針を画面外へ移動させるスクリプト
//================================================================

public class NeedleDisplay : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //アニメーションを取得
    private Animation NeedleAnim;

    // Use this for initialization
    void Start()
    {
        NeedleAnim = this.gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーションの再生が終わったのならば画面外へ移動
        if(!NeedleAnim.isPlaying)
        {
            gameObject.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
        }

    }
}
