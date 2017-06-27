using UnityEngine;
using System.Collections;

//=================================================================
//ステージ2のギミックをクリアした時にアリスを動かすスクリプト
//=================================================================

public class AliceMove_Stage2 : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floor1Gimmick;
    public GameObject Floor2Gimmick;

    //クリアされた時にアリスを動かす変数
    private Vector3 pos;

    //クリアしたかのフラグを受け取る変数
    private bool GetFloor1ClearFlag;
    private bool GetFloor2ClearFlag;

    //Animatorを取得
    private Animator AliceAnim;
    private AnimatorStateInfo AliceAnimInfo;

    public ufoAction clearFlag;


    //ページめくりの演出を行うオブジェクト
    public GameObject Stage2_Clear_Obj;
    public GameObject black;


    public AliceMove aliceMove;


    //アリスがどのステージに達しているかのフラグ(ステージ2)
    //ステージの最初からと操作するために必要
    public static bool AliceStage2Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage2Flag()
    {
        return AliceStage2Flag;
    }


    // Use this for initialization
    void Start () {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);

        GetFloor1ClearFlag = false;
        GetFloor2ClearFlag = false;

        AliceStage2Flag = true;

    }

    // Update is called once per frame
    void Update () {

        //熊が完全に治ったかのフラグを受け取る
        BearClear touch = Floor1Gimmick.GetComponent<BearClear>();
        GetFloor1ClearFlag = touch.GetClearFlag();

        //列車と貨物が全て連結してしっかりと発射したかのフラグを受け取る
        TrainMove touch2 = Floor2Gimmick.GetComponent<TrainMove>();
        GetFloor2ClearFlag = touch2.GetClearFlag();

        aliceMove.AliceMovePos(GetFloor1ClearFlag, GetFloor2ClearFlag);


        Stage2Clear();
    }


    void Stage2Clear()
    {
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);

        //フロア3をクリアしたフラグを受け取って
        //アリスのアニメーションを再生する
        if (clearFlag.GetUfoCollFlag() == true)
        {
            AliceAnim.SetTrigger("happy");
            AliceAnim.SetBool("clear", true);
        }

        //アニメーションがclearに遷移するとページめくりのオブジェクトを表示
        if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.clear"))
        {
            //クリアした時にめくられるページを表示させる
            Stage2_Clear_Obj.SetActive(true);
            black.SetActive(true);

            //アリスがステージ2に移動するので
            //アリスがステージ1にいるというフラグはfalseにする
            AliceStage2Flag = false;
        }

    }


}
