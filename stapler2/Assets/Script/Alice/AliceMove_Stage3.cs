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

    //クリアされた時にアリスを動かす変数
    private Vector3 pos;

    //クリアしたかのフラグを受け取る変数
    private bool GetFloor1ClearFlag;
    private bool GetFloor2ClearFlag;

    //Animatorを取得
    private Animator AliceAnim;
    private AnimatorStateInfo AliceAnimInfo;

    private enum Floor
    {
        FLOOR1,
        FLOOR2,
        FLOOR3,
    }
    private Floor floor;


    //アリスがギミックに当たったかのフラグを受け取る変数
    private bool GetAliceCollFlag;

    //移動が終了したかのフラグ
    private bool Floor3MoveMentEndFlag;
    //フロア3に到達した時のフラグを渡す関数
    public bool GetFloor3MoveEndFlag()
    {
        return Floor3MoveMentEndFlag;
    }

    public WitchAction witchDown;


    //クリアした時にページめくりの演出を行うオブジェクト
    public GameObject Stage3_Clear_Obj;
    public GameObject black;


    // Use this for initialization
    void Start()
    {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);
        GetAliceCollFlag = false;
        Floor3MoveMentEndFlag = false;
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

        //アリスが敵か何かとぶつかった際のフラグを受け取る
        AliceGameOver coll = gameObject.GetComponent<AliceGameOver>();
        GetAliceCollFlag = coll.GetAliceCollFlag();

        //アリスが何かとぶつかっていなかったら処理を行う
        if (GetAliceCollFlag == false)
        {
            //フロア1をクリアした場合に処理
            if (GetFloor1ClearFlag == true && GetFloor2ClearFlag == false)
            {
                floor = Floor.FLOOR2;
            }
            //フロア2をクリアした場合に処理
            else if (GetFloor2ClearFlag == true)
            {
                floor = Floor.FLOOR3;
            }
            AliceMovePos();
        }

        if(witchDown.GetWitchCollNorma()<=0)
        {
            AliceAnim.SetTrigger("happy");
            AliceAnim.SetBool("clear", true);
        }

        if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.clear"))
        {
            //クリアした時にめくられるページを表示させる
            Stage3_Clear_Obj.SetActive(true);
            black.SetActive(true);
        }

    }

    void AliceMovePos()
    {
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);

        switch (floor)
        {
            case Floor.FLOOR2:
                if (pos.x < 8.1f)
                {
                    //再生中ならばフラグをtrueにしておいて再生するように
                    AliceAnim.SetTrigger("happy");
                    //喜びモーションが再生されている時間
                    if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.Dash"))
                    {
                        pos = transform.position;
                        pos.x += 0.1f;
                        transform.position = pos;
                        AliceAnim.ResetTrigger("happy");
                    }
                }
                else
                {
                    //指定した位置まで移動したら待機モーションに切り替わる
                    AliceAnim.SetBool("Idle", true);
                }
                break;
            case Floor.FLOOR3:
                if (pos.x < 21.35f)
                {
                    AliceAnim.SetBool("Idle", false);
                    //再生中ならばフラグをtrueにしておいて再生するように
                    AliceAnim.SetTrigger("happy");
                    //喜びモーションが再生されている時間
                    if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.Dash"))
                    {
                        pos = transform.position;
                        pos.x += 0.1f;
                        transform.position = pos;
                        AliceAnim.ResetTrigger("happy");
                    }
                }
                else
                {
                    //指定した位置まで移動したら待機モーションに切り替わる
                    AliceAnim.SetBool("Idle", true);
                    Floor3MoveMentEndFlag = true;
                }
                break;
        }
    }

}
