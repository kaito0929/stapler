using UnityEngine;
using System.Collections;

public class WitchColl : MonoBehaviour
{

    //=============================================================
    //魔女が火球に当たった時の処理を行うスクリプト
    //=============================================================

    // 変数宣言----------------------------------------------------------------------

    //魔女に火球を当てる回数(三回)
    private int WitchCollNorma;
    //残りの火球を当てる回数
    public int GetWitchCollNorma()
    {
        return WitchCollNorma;
    }

    //魔女が倒されたかどうかのフラグ
    private bool WitchDestroyFlag;
    //魔女を倒したフラグを他のスクリプトに渡す関数
    public bool GetWitchDestroy()
    {
        return WitchDestroyFlag;
    }

    //アリスがどのステージに達しているかのフラグ(ステージ3)
    //ステージの最初からと操作するために必要
    public static bool AliceStage3Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage3Flag()
    {
        return AliceStage3Flag;
    }

    //移動スピード
    private float speed = 1.0f;
    //動かす変数
    private float step = 0.0f;

    private float dis;

    private enum WitchMoveState
    {
        MOVE_CIRCLE,//円運動
        MOVE_EIGHT,//八の字移動
        MOVE_STAR,//五芒星の軌道
        MOVE_STANDBY,//待機状態
    }

    private WitchMoveState witchMoveState;

    private Animator WitchAnim;
    //アニメーションのステートを取得する
    private AnimatorStateInfo animInfo;

    // Use this for initialization
    void Start()
    {
        WitchCollNorma = 3;
        WitchDestroyFlag = false;
        WitchAnim = GetComponent<Animator>();
        witchMoveState = WitchMoveState.MOVE_CIRCLE;
    }

    // Update is called once per frame
    void Update()
    {
        WitchMove move = gameObject.GetComponent<WitchMove>();
        dis = move.GetDis();

        //攻撃が当たった場合に移動方法を変更
        switch (witchMoveState)
        {
            case WitchMoveState.MOVE_CIRCLE://円運動
                move.MoveToCircle();
                break;

            case WitchMoveState.MOVE_EIGHT://八の字移動
                move.MoveToFigureOfEight();
                break;

            case WitchMoveState.MOVE_STAR://五芒星の軌道
                move.MoveToStar();
                break;

            case WitchMoveState.MOVE_STANDBY://待機状態
                move.WitchStandby();
                break;
        }

        if (dis <= 0)
        {
            if (WitchCollNorma == 2)
            {
                witchMoveState = WitchMoveState.MOVE_EIGHT;
            }
            else if (WitchCollNorma == 1)
            {
                witchMoveState = WitchMoveState.MOVE_STAR;
            }
        }

        Debug.Log(witchMoveState);


        //魔女の消滅処理
        WitchDestroy();
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
            //攻撃を受けたモーションの再生
            WitchAnim.SetTrigger("Damage");
        }
    }


    void WitchDestroy()
    {
        //指定した回数分火球にぶつけることが出来たなら
        //魔女をDestoryで消す
        if (WitchCollNorma == 0)
        {
            //エンディング画面へ遷移するのでフラグをfalseに戻しておく
            AliceStage3Flag = false;
            //魔女は消滅
            Destroy(this.gameObject);
            //魔女が倒されたことによってフラグがtrueになり
            //エンディング画面へと遷移する
            WitchDestroyFlag = true;
        }
    }



}
