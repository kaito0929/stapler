using UnityEngine;
using System.Collections;

public class EnemyThrowAnim : MonoBehaviour {

    //=================================================================
    //岩をタップした音に驚いて敵が骨を投げるように見せるスクリプト
    //=================================================================

    // 変数宣言----------------------------------------------------------------------
    //Animatorを取得
    private Animator anim;

    //岩をタップしたかのフラグを受け取る変数
    private bool PlaybackFlag;
    //タップされたかのフラグを持つオブジェクト
    public GameObject Rock;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        PlaybackFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

       
	}
}
