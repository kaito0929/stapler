using UnityEngine;
using System.Collections;

public class AliceMove_Stage2 : MonoBehaviour {

    //=================================================================
    //ステージ2のギミックをクリアした時にアリスを動かすスクリプト
    //=================================================================

    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floor1Gimmick;
    public GameObject Floor2Gimmick;

    //クリアされた時にアリスを動かす変数
    private Vector3 pos;

    //クリアしたかのフラグを受け取る変数
    private bool GetFloor1ClearFlag;
    private bool GetFloor2ClearFlag;

    //移動が終了したかのフラグ
    private bool Floor2MoveMentEndFlag;
    //フロア2に到達した時のフラグを渡す関数
    public bool GetFloor2MoveEndFlag()
    {
        return Floor2MoveMentEndFlag;
    }

    //移動が終了したかのフラグ
    private bool Floor3MoveMentEndFlag;
    //フロア3に到達した時のフラグを渡す関数
    public bool GetFloor3MoveEndFlag()
    {
        return Floor3MoveMentEndFlag;
    }

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

    // Use this for initialization
    void Start () {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);
        GetAliceCollFlag = false;
        Floor2MoveMentEndFlag = false;
        Floor3MoveMentEndFlag = false;
        floor = Floor.FLOOR1;
    }
	
	// Update is called once per frame
	void Update () {

        //熊が完全に治ったかのフラグを受け取る
        BearClear touch = Floor1Gimmick.GetComponent<BearClear>();
        GetFloor1ClearFlag = touch.GetClearFlag();

        //列車と貨物が全て連結してしっかりと発射したかのフラグを受け取る
        TrainMove touch2 = Floor2Gimmick.GetComponent<TrainMove>();
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
        }

        AliceMovePos();
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
                    AliceAnim.SetBool("happy", true);
                    //喜びモーションが再生されている時間
                    if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.Dash"))
                    {
                        pos = transform.position;
                        pos.x += 0.1f;
                        transform.position = pos;
                    }
                }
                else
                {
                    //指定した位置まで移動したら待機モーションに切り替わる
                    AliceAnim.SetBool("Idle", true);
                    AliceAnim.SetBool("happy", false);
                    //フロア2への移動が終了したのでフラグをtrueに
                    Floor2MoveMentEndFlag = true;
                }
                break;

            case Floor.FLOOR3:
                if (pos.x < 21.35f)
                {
                    AliceAnim.SetBool("Idle", false);
                    //再生中ならばフラグをtrueにしておいて再生するように
                    AliceAnim.SetBool("happy", true);
                    //喜びモーションが再生されている時間
                    if (AliceAnimInfo.nameHash == Animator.StringToHash("Base Layer.Dash"))
                    {
                        pos = transform.position;
                        pos.x += 0.1f;
                        transform.position = pos;
                    }
                }
                else
                {
                    //指定した位置まで移動したら待機モーションに切り替わる
                    AliceAnim.SetBool("Idle", true);
                    AliceAnim.SetBool("happy", false);
                    //フロア3への移動が終了したのでフラグをtrueに
                    Floor3MoveMentEndFlag = true;
                }
                break;
        }
    }
}
