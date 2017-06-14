using UnityEngine;
using System.Collections;

//====================================================================
//ステージ2にいる敵をアリスへ接近させて、
//近づいたら攻撃するように動かすスクリプト
//フラグはGimmickJointのフラグを受け取る
//====================================================================

public class Stage2EnemyAction : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //アニメーターを取得
    private Animator EnemyAnim;

    //目標のオブジェクト（アリス）
    public GameObject TargetObj;

    //移動させるためのベクトル
    private Vector3 MoveVec;

    //移動開始のタイミング
    private bool MoveStateFlag;

    //移動終了のタイミング
    private bool MoveStopFlag;

    //敵とアリスの距離
    private float Distance;

    //アリスと当たった場合にゲームオーバーになるオブジェクト
    public GameObject Rod;

    // Use this for initialization
    void Start()
    {
        EnemyAnim = GetComponent<Animator>();

        MoveStateFlag = false;
        MoveStopFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        AliceAccess();
        EnemyAttackAnim();
    }

    //アリスへ接近させる関数
    void AliceAccess()
    {
        //敵とアリスの距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = TargetObj.transform.position;
        Distance = Vector3.Distance(Apos, Bpos);

        //敵が無力化されたかのフラグを受け取る
        GimmickJoint gimmick = gameObject.GetComponent<GimmickJoint>();
        MoveStopFlag = gimmick.GetTapFlag();

        //アリスと敵の距離が11f以下になったらエネミーが動き出すように
        if (Distance <= 11f)
        {
            MoveStateFlag = true;
        }

        //移動可能かのフラグと無力化されていないかのフラグの
        //両方がしていした通りなら処理
        if (MoveStateFlag == true && MoveStopFlag == false)
        {
            //アリスとの距離が2f以上ならば敵を移動
            if (Distance >= 2f)
            {
                MoveVec = gameObject.transform.position;
                MoveVec.x -= 0.01f;
                gameObject.transform.position = MoveVec;
            }
        }
        //敵がタップされて無力化された場合に処理
        else if (MoveStopFlag == true)
        {
            //棒の当たり判定を消しておく
            Rod.GetComponent<BoxCollider>().enabled = false;
        }

    }

    //アリスに一定距離近づいたら攻撃アニメーションを再生するように
    void EnemyAttackAnim()
    {
        if (Distance <= 2f)
        {
            EnemyAnim.SetTrigger("Attack");
        }
    }
}
