using UnityEngine;
using System.Collections;

//=================================================================
//ステージ1のギミックをクリアした時にアリスを動かすスクリプト
//=================================================================

public class AliceMove : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //クリアされた時にアリスを動かす変数
    private Vector3 pos;

    //Animatorを取得
    private Animator AliceAnim;
    //AnimatorStateInfoを取得
    private AnimatorStateInfo AliceAnimInfo;


    //アリスがギミックに当たったかのフラグを受け取る変数
    private bool GetAliceCollFlag;

    //フロア2に移動完了したかのフラグ
    private bool Floor2MoveEndFlag;
    //そのフラグをパンダを動かすスクリプトに渡す関数
    public bool GetFloor2MoveEndFlag()
    {
        return Floor2MoveEndFlag;
    }


    //移動が終了したかのフラグ
    private bool Floor3MoveEndFlag;
    //フロア3に到達した時のフラグを渡す関数
    public bool GetFloor3MoveEndFlag()
    {
        return Floor3MoveEndFlag;
    }


    // Use this for initialization
    void Start () {
        AliceAnim = GetComponent<Animator>();
        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);
        GetAliceCollFlag = false;

        Floor2MoveEndFlag = false;
        Floor3MoveEndFlag = false;
    }
	
	// Update is called once per frame
	void Update () {

    }


    //アリスのアニメーションを制御して、移動させるための関数
    public void AliceMovePos(bool floor1Flag, bool floor2Flag)
    {
        //アリスが敵か何かとぶつかった際のフラグを受け取る
        AliceGameOver coll = gameObject.GetComponent<AliceGameOver>();
        GetAliceCollFlag = coll.GetAliceCollFlag();


        AliceAnimInfo = AliceAnim.GetCurrentAnimatorStateInfo(0);

        //アリスが何かとぶつかっていなかったら処理を行う
        if (GetAliceCollFlag == false)
        {
            if (floor1Flag == true && floor2Flag == false)
            {
                if (pos.x < 8.1f)
                {
                    //再生中ならばフラグをtrueにしておいて再生するように
                    AliceAnim.SetTrigger("happy");

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
                    Floor2MoveEndFlag = true;
                }

            }
            else if (floor2Flag == true)
            {
                if (pos.x < 21.35f)
                {
                    AliceAnim.SetBool("Idle", false);
                    //再生中ならばフラグをtrueにしておいて再生するように
                    AliceAnim.SetTrigger("happy");

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
                    //フロア3への移動が終了したのでフラグをtrueに
                    Floor3MoveEndFlag = true;
                }
            }
        }



    }


}
