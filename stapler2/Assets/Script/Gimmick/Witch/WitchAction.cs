using UnityEngine;
using System.Collections;

//======================================
//魔女の行動を制御するスクリプト
//移動や攻撃を実行する
//======================================

public class WitchAction : MonoBehaviour
{
    // 変数宣言----------------------------------------------------------------------

    //魔女に火球を当てる回数(三回)
    private int WitchCollNorma;
    //変数を別のスクリプトに渡す関数
    //魔女の色を変えるために使用する
    public int GetWitchCollNorma()
    {
        return WitchCollNorma;
    }


    //対象との距離
    private float Distance;

    private enum WitchMoveState
    {
        MOVE_CIRCLE,    //円運動
        MOVE_EIGHT,     //八の字移動
        MOVE_STAR,      //五芒星の軌道
        MOVE_STANDBY,   //待機状態
    }
    private WitchMoveState witchMoveState;

    //魔女の弾発射の数を制御する変数
    private int WitchShotMax;
    private int WitchShotNum;


    //Animatorの取得
    private Animator WitchAnim;


    //アリスがフロア3に到達したかのフラグを取得する
    public AliceMove aliceMove;
   

    // Use this for initialization
    void Start()
    {
        WitchCollNorma = 3;
        WitchAnim = GetComponent<Animator>();
        witchMoveState = WitchMoveState.MOVE_CIRCLE;
        Distance = 0;

        WitchShotMax = 1;
        WitchShotNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //魔女の行動制御
        WitchActionControl();
        //魔女がやられる処理
        WitchDown();
    }

    //魔女の動きの制御（主に移動と攻撃）
    void WitchActionControl()
    {
        WitchMove move = gameObject.GetComponent<WitchMove>();
        Distance = move.GetDistance();

        WitchShot shot = gameObject.GetComponent<WitchShot>();

        if (aliceMove.GetFloor3MoveEndFlag() == true)
        {
            //攻撃が当たった場合に移動方法を変更
            //移動方法に伴って弾の発射する数を増やしていく
            switch (witchMoveState)
            {
                case WitchMoveState.MOVE_CIRCLE://円運動
                    move.MoveToCircle();
                    WitchShotMax = 1;
                    break;

                case WitchMoveState.MOVE_EIGHT://八の字移動
                    move.MoveToFigureOfEight();
                    WitchShotMax = 2;
                    break;

                case WitchMoveState.MOVE_STAR://五芒星の軌道
                    move.MoveToStar();
                    WitchShotMax = 3;
                    break;

                case WitchMoveState.MOVE_STANDBY://待機状態
                    move.WitchStandby();
                    break;
            }

            //魔女が倒されていない状態で待機状態でないならば弾を発射するように
            if (WitchCollNorma >= 0 && witchMoveState != WitchMoveState.MOVE_STANDBY)
            {
                shot.WitchShotCreate(WitchShotNum, WitchShotMax);
            }
        }

        //魔女が行動を開始する初期位置との距離を測る
        if (Distance <= 0)
        {
            if (WitchCollNorma == 2)
            {
                //魔女の動きを八の字移動に変更
                witchMoveState = WitchMoveState.MOVE_EIGHT;
            }
            else if (WitchCollNorma == 1)
            {
                //魔女の動きを五芒星の軌道で移動するように変更
                witchMoveState = WitchMoveState.MOVE_STAR;
            }
        }
    }

    //魔女に火球が当たるたびに当てるノルマを減らしていく
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "wicth_ball1(Clone)")
        {
            //魔女の残りライフ
            WitchCollNorma--;
            //攻撃を喰らったので魔女を落下させる
            witchMoveState = WitchMoveState.MOVE_STANDBY;

            if (WitchCollNorma > 0)
            {
                //攻撃を受けたモーションの再生
                WitchAnim.SetTrigger("Damage");
            }
        }
    }


    void WitchDown()
    {
        //指定した回数分火球にぶつけることが出来たなら
        //アニメーションを再生して倒したように見せる
        if (WitchCollNorma <= 0)
        {
            //吹き飛ばされるアニメーションを再生
            WitchAnim.SetTrigger("Finish");
        }
    }



}
