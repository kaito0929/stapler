using UnityEngine;
using System.Collections;

public class AliceMove : MonoBehaviour {

    //=================================================================
    //ステージ1のギミックをクリアした時にアリスを動かすスクリプト
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

    //Animatorを取得
    Animator anim;

    //喜ぶアニメーションの再生時間
    private float HappyAnimPlayTime;
    private float HappyAnimPlayTime2;

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
        anim = GetComponent<Animator>();
        HappyAnimPlayTime = 0;
        HappyAnimPlayTime2 = 0;
        GetAliceCollFlag = false;
        Floar2_ReachingFlag = false;
    }
	
	// Update is called once per frame
	void Update () {

        //エネミーがタップされてフリーフォールと親子関係になったかのフラグを受け取る
        Floar1Clear touch = Floar1Gimmick.GetComponent<Floar1Clear>();
        GetFloar1ClearFlag = touch.GetClear();

        //馬とメリーゴーランドが親子関係になったかのフラグを受け取る
        GimmickParent touch2 = Floar2Gimmick.GetComponent<GimmickParent>();
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
                        Floar2_ReachingFlag = true;                       
                    }
                }
            }
            //フロア2にをクリアした場合に処理
            //内容は上と同じなのでコメントはそっちを参考に
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
                    }
                }
            }
        }

        
    }
}
