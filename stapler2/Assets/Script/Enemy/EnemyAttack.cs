using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    //====================================================================
    //ステージ2のフロア2にいる敵をアリスへ接近、
    //近づいたら攻撃するように動かすスクリプト
    //====================================================================

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

    public GameObject Rod;

    // Use this for initialization
    void Start () {
        EnemyAnim = GetComponent<Animator>();

        MoveStateFlag = false;
        MoveStopFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        AliceAccess();
        EnemyAttackAnim();
    }

    //アリスへ接近させる関数
    void AliceAccess()
    {
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = TargetObj.transform.position;
        Distance = Vector3.Distance(Apos, Bpos);

        AliceMove_Stage2 alice = TargetObj.GetComponent<AliceMove_Stage2>();
        MoveStateFlag = alice.GetFloar2MoveEndFlag();

        GimmickParent gimmick = gameObject.GetComponent<GimmickParent>();
        MoveStopFlag = gimmick.GetTapFlag();


        if (MoveStateFlag == true && MoveStopFlag == false)
        {
            if (Distance >= 2f)
            {
                MoveVec = gameObject.transform.position;
                MoveVec.x -= 0.01f;
                gameObject.transform.position = MoveVec;
            }
        }
        else if (MoveStopFlag == true)
        {
            Rod.GetComponent<BoxCollider>().enabled = false;
        }

    }

    void EnemyAttackAnim()
    {
        if(Distance<=2f)
        {
            EnemyAnim.SetTrigger("Attack");
        }
    }
}
