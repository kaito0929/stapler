﻿using UnityEngine;
using System.Collections;

public class RockTap : MonoBehaviour {

    //==================================================================================
    //ステージ3のフロア2での大きな岩をタップした際の処理を行うスクリプト
    //==================================================================================

    // 変数宣言----------------------------------------------------------------------

    //タップした際に鳴らす効果音を格納
    public AudioSource audioSource;

    //岩がタップされているかのフラグ
    //敵の骨を投げるアニメーションの再生のために使う
    private bool RockTapFlag;
    //別のスクリプトへ変数を渡すための関数
    public bool GetRockTapFlag()
    {
        return RockTapFlag;
    }

    public RockBreak rockBreak;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        RockTapFlag = false;
    }
	
	// Update is called once per frame
	void Update () {

        //岩のオブジェクトをタップしたら効果音が鳴るように
	    if(TouchManager.SelectedGameObject==gameObject)
        {
            //岩が破壊されていなかったら効果音が再生
            if (rockBreak.GetClearFlag() == false)
            {
                audioSource.PlayOneShot(audioSource.clip);
                RockTapFlag = true;
            }
        }
        else
        {
           RockTapFlag = false;
        }
	}
}
