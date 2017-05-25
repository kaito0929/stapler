using UnityEngine;
using System.Collections;

public class AliceMove : MonoBehaviour {

    //=================================================================
    //ステージ1のギミックをクリアした時にアリスを動かすスクリプト
    //=================================================================

    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floor1Gimmick;
    public GameObject Floor2Gimmick;

    //クリアされた時にアリスを動かす変数
    private Vector3 pos;

    //クリアしたかのフラグを受け取る変数
    private bool GetFloar1ClearFlag;
    private bool GetFloar2ClearFlag;

    //Animatorを取得
    private Animator AliceAnim;
    //AnimatorStateInfoを取得
    private AnimatorStateInfo AliceAnimInfo;


    //どのフロアにいるか判別するための変数
    private enum Floor
    {
        FLOOR1,
        FLOOR2,
        FLOOR3,
    }
    private Floor floor;


    //アリスがギミックに当たったかのフラグを受け取る変数
    private bool GetAliceCollFlag;

    //ステージ1のフロア2に到達したかのフラグ
    //パンダの動き出すタイミングになっている
    private bool Floar2_ReachingFlag;
    //そのフラグをパンダを動かすスクリプトに渡す関数
    public bool GetReachingFlag()
    {
        return Floar2_ReachingFlag;
    }


    // Use this for initialization
    void Start () {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);
        GetAliceCollFlag = false;
        Floar2_ReachingFlag = false;
        floor = Floor.FLOOR1;
    }
	
	// Update is called once per frame
	void Update () {

        //エネミーがタップされてフリーフォールと親子関係になったかのフラグを受け取る
        Floor1Clear touch = Floor1Gimmick.GetComponent<Floor1Clear>();
        GetFloar1ClearFlag = touch.GetClear();

        //馬とメリーゴーランドが親子関係になったかのフラグを受け取る
        GimmickParent touch2 = Floor2Gimmick.GetComponent<GimmickParent>();
        GetFloar2ClearFlag = touch2.GetTapFlag();

        //アリスが敵か何かとぶつかった際のフラグを受け取る
        AliceGameOver coll = gameObject.GetComponent<AliceGameOver>();
        GetAliceCollFlag = coll.GetAliceCollFlag();

        //アリスが何かとぶつかっていなかったら処理を行う
        if (GetAliceCollFlag == false)
        {
            //フロア1をクリアした場合に処理
            if (GetFloar1ClearFlag == true && GetFloar2ClearFlag == false)
            {
                floor = Floor.FLOOR2;
            }
            //フロア2にをクリアした場合に処理
            else if (GetFloar2ClearFlag == true)
            {
                floor = Floor.FLOOR3;
            }

            AliceMovePos();
        }
        
        
    }


    //アリスのアニメーションを制御して、移動させるための関数
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
                    AliceAnim.SetBool("Idle",true);
                    AliceAnim.SetBool("happy", false);
                    Floar2_ReachingFlag = true;
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
                }
                break;
        }
    }


}
