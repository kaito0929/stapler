using UnityEngine;
using System.Collections;

public class RockBreak : MonoBehaviour {

    //=================================================================================
    //竜の骨が岩とぶつかった時にアニメーション(岩が壊れる)を再生させるスクリプト
    //=================================================================================

    // 変数宣言----------------------------------------------------------------------
    //Animatorを取得
    private Animator BreakAnim;

    //岩が壊れたかのフラグ
    private bool RockBreakFlag;
    //フラグを別のスクリプトに渡す関数
    public bool GetClearFlag()
    {
        return RockBreakFlag;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bone006")
        {
            //竜の骨とぶつかったので岩が壊れる
            BreakAnim.SetBool("Break", true);

            //岩の破砕音を再生
            AudioManager.Instance.PlaySE("RockTap");

            //岩が壊れたかのフラグをtrueに
            RockBreakFlag = true;
        }
    }

    // Use this for initialization
    void Start () {
        BreakAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
