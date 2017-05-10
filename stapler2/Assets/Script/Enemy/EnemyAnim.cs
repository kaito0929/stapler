using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour {

    //=========================================================================================
    //エネミーがギミックに止められた時のじたばたと、もがくアニメーションを再生させる
    //=========================================================================================

    // 変数宣言----------------------------------------------------------------------

    //アニメーションを取得
    private Animator anim;
    //ギミックに止められたかのフラグを受け取る変数
    //じたばたアニメーションの再生に使う
    private bool GetTapFlag;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        GetTapFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

        //ギミックに止められたかのフラグを常に受け取っておく
        GimmickParent enemy = gameObject.GetComponent<GimmickParent>();
        GetTapFlag = enemy.GetTapFlag();

        //ギミックに止められたのならばじたばたアニメーションを再生させる
        if(GetTapFlag==true)
        {
            anim.SetTrigger("Stop");
        }
	}
}
