using UnityEngine;
using System.Collections;

public class DragonAnim : MonoBehaviour {

    //======================================================================
    //両方の骨がタップされて壁に止められたかのフラグを受け取って
    //ドラゴンのアニメーションを再生するスクリプト
    //======================================================================

    // 変数宣言----------------------------------------------------------------------

    //骨の竜のアニメーション再生用
    private Animator dragonAnim;

    //フラグを持つオブジェクト
    public GameObject[] Bone=new GameObject[2];

    //オブジェクトからフラグを受け取る変数
    private bool[] BoneStopFlag=new bool[2];

    // Use this for initialization
    void Start () {
        dragonAnim = gameObject.GetComponent<Animator>();

        BoneStopFlag[0] = false;
        BoneStopFlag[1] = false;
    }

    // Update is called once per frame
    void Update()
    {
        //骨が指定の位置で止められてかのフラグを受け取る
        BoneStop bone1 = Bone[0].GetComponent<BoneStop>();
        BoneStopFlag[0] = bone1.GetBoneTapStopFlag();

        BoneStop bone2 = Bone[1].GetComponent<BoneStop>();
        BoneStopFlag[1] = bone2.GetBoneTapStopFlag();

        //両方のフラグがtrueになっていたらドラゴンのアニメーションを再生
        //岩を破壊する
        if (BoneStopFlag[0] == true && BoneStopFlag[1] == true)
        {
            dragonAnim.SetBool("Break", true);
        }

    }
}
