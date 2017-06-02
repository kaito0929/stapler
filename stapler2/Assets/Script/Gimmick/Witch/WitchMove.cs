using UnityEngine;
using System.Collections;

//===================================================
//魔女の動きを制御するスクリプト
//===================================================

public class WitchMove : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //魔女が追尾するオブジェクト
    public GameObject[] WitchMovePoint;
    //移動スピード
    private float Speed;
    //動かす変数
    private float Step;

    //五芒星の動きのために必要な変数
    //次に移動する場所を変更するために使用する
    private int NextMovePointNum;

    //魔女を移動させるためのベクトル
    private Vector3 WitchMovePos;

    //Animatorの取得
    private Animator WitchAnim;
    //AnimatorStateInfoの取得
    private AnimatorStateInfo WitchAnimInfo;

    //魔女に攻撃が当たった後に移動する場所との距離
    private float Distance;
    public float GetDistance()
    {
        return Distance;
    }

    //移動のための変数
    //加算して場所を更新し続ける
    private float MoveAddNum;

    // Use this for initialization
    void Start () {
        WitchAnim = GetComponent<Animator>();
        Speed = 6.0f;
        Step = 0.0f;
        NextMovePointNum = 1;
        MoveAddNum = 0;
    }
	
	// Update is called once per frame
	void Update () {

    }

    // 円移動--------------------------------------------------------------------------
    public void MoveToCircle()
    {

        MoveAddNum += 0.02f;
        // 円運動の座標演算
        WitchMovePos.x = 29f + Mathf.Sin(MoveAddNum) * 3f;
        WitchMovePos.y =       Mathf.Cos(MoveAddNum) * 3f;
        WitchMovePos.z = -5f;
        // オブジェクトに座標を代入
        transform.position = WitchMovePos;


        MovePosDistance();
    }

    // ８の字移動-----------------------------------------------------------------------
    public void MoveToFigureOfEight()
    {
        MoveAddNum += 0.02f;
        // ８の字移動の座標演算
        WitchMovePos.x = 27.5f + Mathf.Cos(MoveAddNum) * 4f;
        WitchMovePos.y =         Mathf.Sin(MoveAddNum * 2f) * 2f;
        WitchMovePos.z = -5f;

        // オブジェクトに座標を代入
        transform.position = WitchMovePos;

        MovePosDistance();
    }

    // 五芒星の軌道---------------------------------------------------------------------
    public void MoveToStar()
    {
        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = WitchMovePoint[NextMovePointNum].transform.position;
        Distance = Vector3.Distance(Apos, Bpos);

        //目標の位置に達したら次の場所へ動くように
        if (Distance <= 0f)
        {
            NextMovePointNum++;
        }

        //numをリセット。最初の場所へ
        if (NextMovePointNum == 5)
        {
            NextMovePointNum = 0;
        }

        //動くスピード
        Step = Time.deltaTime * Speed;
        //魔女の位置を更新
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, WitchMovePoint[NextMovePointNum].transform.position, Step);
    }


    //攻撃を喰らった魔女が次の行動に移すインターバル
    //指定した位置に移動するように処理
    public void WitchStandby()
    {
        WitchAnimInfo = WitchAnim.GetCurrentAnimatorStateInfo(0);

        MovePosDistance();

        //動くスピード
        Step = Time.deltaTime * Speed;

        MoveAddNum = 0;

        //アニメーションがDefaultになったら処理
        if (WitchAnimInfo.nameHash == Animator.StringToHash("Base Layer.Default"))
        {
            //魔女の位置を更新
            gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, WitchMovePoint[0].transform.position, Step);
        }
    }

    //魔女と初期位置との距離
    void MovePosDistance()
    {
        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = WitchMovePoint[0].transform.position;
        Distance = Vector3.Distance(Apos, Bpos);
    }

}
