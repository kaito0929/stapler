using UnityEngine;
using System.Collections;

//===============================================================
//ステージ1・フロア3でのクリア判定をとるスクリプト
//===============================================================

public class Stage1Clear : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------
    
    //タップされたかの判定を持つオブジェクト（エネミー）
    public GameObject Floar3Enemy1;
    public GameObject Floar3Enemy2;

    //タップされたかの判定を受け取る変数
    private bool[] TapFlag = new bool[2];

    //ギミックをクリアしたかのフラグ
    private bool GimmickClearFlag;

    //正解した時の音を再生するためのフラグ
    private bool SoundFlag;


    //アリスがどのステージに達しているかのフラグ(ステージ1)
    //ステージの最初からと操作するために必要
    public static bool AliceStage1Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage1Flag()
    {
        return AliceStage1Flag;
    }

    public GameObject alice;

    //アリスのAnimatorを取得
    private Animator AliceAnim;
    //アリスのAnimatorStateInfoを取得
    public AnimatorStateInfo AliceAnimInfo;


    //ページめくりの演出を行うオブジェクト
    public GameObject Stage1_Clear_Obj;
    public GameObject black;

    // Use this for initialization
    void Start () {
        TapFlag[0] = false;
        TapFlag[1] = false;

        GimmickClearFlag = false;
        SoundFlag = false;

        AliceAnim = alice.GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);


        //フロア3に設置されている片方のエネミーのタップされたかのフラグを受け取る
        GimmickParent tap = Floar3Enemy1.GetComponent<GimmickParent>();
        TapFlag[0] = tap.GetTapFlag();
        //フロア3に設置されている片方のエネミーのタップされたかのフラグを受け取る
        GimmickParent tap2 = Floar3Enemy2.GetComponent<GimmickParent>();
        TapFlag[1] = tap2.GetTapFlag();

        //両方のフラグがtrueになれば両方タップされたと判定がとられ
        //ステージ1はクリアとなる
        if (TapFlag[0] == true && TapFlag[1] == true)
        {
            //アリスがステージ2に移動するので
            //アリスがステージ1にいるというフラグはfalseにする
            AliceStage1Flag = false;

            GimmickClearFlag = true;

            //アリスの喜びアニメーションを再生
            AliceAnim.SetTrigger("happy");
            AliceAnim.SetBool("clear",true);

            if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.clear"))
            {
                //クリアした時にめくられるページを表示させる
                Stage1_Clear_Obj.SetActive(true);
                black.SetActive(true);
            }

        }


        if(GimmickClearFlag==true)
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
