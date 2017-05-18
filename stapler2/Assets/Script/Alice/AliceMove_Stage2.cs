using UnityEngine;
using System.Collections;

public class AliceMove_Stage2 : MonoBehaviour {

    //=================================================================
    //ステージ2のギミックをクリアした時にアリスを動かすスクリプト
    //=================================================================

    // 変数宣言----------------------------------------------------------------------

    //ギミックがクリアされたかのフラグを持つギミック
    public GameObject Floar1Gimmick;
    public GameObject Floar2Gimmick;

    //クリアされた時にアリスを動かす変数
    private Vector3 pos;

    //クリアしたかのフラグを受け取る変数
    private bool GetFloar1ClearFlag;
    private bool GetFloar2ClearFlag;

    //移動が終了したかのフラグ
    private bool Floar2MoveMentEndFlag;
    //フロア2に到達した時のフラグを渡す関数
    public bool GetFloar2MoveEndFlag()
    {
        return Floar2MoveMentEndFlag;
    }

    //移動が終了したかのフラグ
    private bool Floar3MoveMentEndFlag;
    //フロア2に到達した時のフラグを渡す関数
    public bool GetFloar3MoveEndFlag()
    {
        return Floar3MoveMentEndFlag;
    }

    //Animatorを取得
    private Animator anim;

    //喜ぶアニメーションの再生時間
    private float HappyAnimPlayTime;
    private float HappyAnimPlayTime2;

    //アリスがギミックに当たったかのフラグを受け取る変数
    private bool GetAliceCollFlag;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        HappyAnimPlayTime = 0;
        HappyAnimPlayTime2 = 0;
        GetAliceCollFlag = false;
        Floar2MoveMentEndFlag = false;
        Floar3MoveMentEndFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        //熊が完全に治ったかのフラグを受け取る
        BearClear touch = Floar1Gimmick.GetComponent<BearClear>();
        GetFloar1ClearFlag = touch.GetClearFlag();

        //列車と貨物が全て連結してしっかりと発射したかのフラグを受け取る
        TrainMove touch2 = Floar2Gimmick.GetComponent<TrainMove>();
        GetFloar2ClearFlag = touch2.GetClearFlag();

        //アリスが何かとぶつかっていなかったら処理を行う
        if (GetAliceCollFlag == false)
        {
            //フロア1をクリアした場合に処理
            if (GetFloar1ClearFlag == true && GetFloar2ClearFlag == false)
            {
                //喜びモーションが再生されている時間
                if (HappyAnimPlayTime < 3.66f)
                {
                    HappyAnimPlayTime += Time.deltaTime;

                    //再生中ならばフラグをtrueにしておいて再生するように
                    anim.SetBool("happy", true);
                    anim.SetBool("Standby", false);
                }
                else
                {
                    //再生が終わったらフラグをfalseにしておく
                    anim.SetBool("happy", false);
                    //アリスの座標が指定された位置に行くまで加算
                    if (pos.x < 8.1f)
                    {
                        pos = transform.position;
                        pos.x += 0.1f;
                        transform.position = pos;
                    }
                    else
                    {
                        //指定した位置まで移動したら待機モーションに切り替わる
                        anim.SetBool("Standby", true);
                        //フロア3への移動が終了したのでフラグをtrueに
                        Floar2MoveMentEndFlag = true;
                    }
                }
            }//フロア2をクリアした場合に処理
            else if (GetFloar2ClearFlag == true)
            {
                if (HappyAnimPlayTime2 < 3.66f)
                {
                    HappyAnimPlayTime2 += Time.deltaTime;
                    anim.SetBool("happy", true);
                    anim.SetBool("Standby", false);
                }
                else
                {
                    anim.SetBool("happy", false);
                    if (pos.x < 21.35f)
                    {
                        pos = transform.position;
                        pos.x += 0.1f;
                        transform.position = pos;
                    }
                    else
                    {
                        anim.SetBool("Standby", true);
                        //フロア3への移動が終了したのでフラグをtrueに
                        Floar3MoveMentEndFlag = true;
                    }
                }
            }
        }


    }


}
