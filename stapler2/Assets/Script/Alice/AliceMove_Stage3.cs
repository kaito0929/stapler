using UnityEngine;
using System.Collections;

//=================================================================
//ステージ3のギミックをクリアした時にアリスを動かすスクリプト
//=================================================================

public class AliceMove_Stage3 : MonoBehaviour
{
    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floor1Gimmick;
    public GameObject Floor2Gimmick;


    //クリアしたかのフラグを受け取る変数
    private bool GetFloor1ClearFlag;
    private bool GetFloor2ClearFlag;

    //Animatorを取得
    private Animator AliceAnim;
    private AnimatorStateInfo AliceAnimInfo;

    public WitchAction witchDown;


    //クリアした時にページめくりの演出を行うオブジェクト
    public GameObject Stage3_Clear_Obj;
    public GameObject black;


    //アリスがどのステージに達しているかのフラグ(ステージ3)
    //ステージの最初からと操作するために必要
    public static bool AliceStage3Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage3Flag()
    {
        return AliceStage3Flag;
    }

    public AliceMove aliceMove;

    // Use this for initialization
    void Start()
    {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);

        GetFloor1ClearFlag = false;
        GetFloor2ClearFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //木の根っこが指定した回数分タップされたかのフラグを受け取る
        WoodAttack touch = Floor1Gimmick.GetComponent<WoodAttack>();
        GetFloor1ClearFlag = touch.StopFlag();

        //岩が壊されているかのフラグを受け取る
        RockBreak touch2 = Floor2Gimmick.GetComponent<RockBreak>();
        GetFloor2ClearFlag = touch2.GetClearFlag();

        aliceMove.AliceMovePos(GetFloor1ClearFlag, GetFloor2ClearFlag);

        Stage3Clear();
    }

    //ステージ3をクリアした時にページめくりが行われるようにする
    void Stage3Clear()
    {
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);

        //フロア3をクリアしたフラグを受け取って
        //アリスのアニメーションを再生する
        if (witchDown.GetWitchCollNorma() <= 0)
        {
            AliceAnim.SetTrigger("happy");
            AliceAnim.SetBool("clear", true);
        }


        //アニメーションがclearに遷移するとページめくりのオブジェクトを表示
        if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.clear"))
        {
            //クリアした時にめくられるページを表示させる
            Stage3_Clear_Obj.SetActive(true);
            black.SetActive(true);

            //エンディング画面へ遷移するのでフラグをfalseに戻しておく
            AliceStage3Flag = false;
        }

    }


}
