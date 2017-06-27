using UnityEngine;
using System.Collections;

//=================================================================
//ステージ4のギミックをクリアした時にアリスを動かすスクリプト
//=================================================================

public class AliceMove_Stage4 : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floor1Gimmick;
    public GameObject Floor2Gimmick;


    //クリアしたかのフラグを受け取る変数
    private bool GetFloor1ClearFlag;
    public bool GetFloor2ClearFlag;

    //Animatorを取得
    private Animator AliceAnim;
    private AnimatorStateInfo AliceAnimInfo;

    //クリアした時にページめくりの演出を行うオブジェクト
    public GameObject Stage4_Clear_Obj;
    public GameObject black;


    //アリスがどのステージに達しているかのフラグ(ステージ4)
    //ステージの最初からと操作するために必要
    public static bool AliceStage4Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage4Flag()
    {
        return AliceStage4Flag;
    }

    public AliceMove aliceMove;

    // Use this for initialization
    void Start () {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);
        AliceStage4Flag = true;

        GetFloor1ClearFlag = false;
        GetFloor2ClearFlag = false;

    }
	
	// Update is called once per frame
	void Update () {

        TenguAnim flag = Floor1Gimmick.GetComponent<TenguAnim>();
        GetFloor1ClearFlag = flag.GetAttackFlag();

        EnemyOutofScreen flag2 = Floor2Gimmick.GetComponent<EnemyOutofScreen>();
        GetFloor2ClearFlag = flag2.GetFlag();

        aliceMove.AliceMovePos(GetFloor1ClearFlag, GetFloor2ClearFlag);

    }
}
