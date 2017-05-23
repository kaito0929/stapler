using UnityEngine;
using System.Collections;

public class WitchMove : MonoBehaviour {

    //===================================================
    //魔女の動きを制御するスクリプト
    //===================================================

    // 変数宣言----------------------------------------------------------------------

    // 移動速度
    private float m_moveSpeed = 1f;
    // 円の半径
    private float m_radius = 4f;


    //魔女が追尾するオブジェクト
    private GameObject[] WitchMovePoint=new GameObject[5];
    //移動スピード
    private float speed = 6.0f;
    //動かす変数
    private float step = 0.0f;

    private int NextMovePointNum = 1;
    
    public WitchColl number;

    private Vector3 pos;

    private Animator WitchAnim;
    //アニメーションのステートを取得する
    private AnimatorStateInfo animInfo;

    private float dis;
    public float GetDis()
    {
        return dis;
    }


    public GameObject TargetObj;


    // Use this for initialization
    void Start () {

        WitchMovePoint[0] = GameObject.Find("WitchMovePoint1");
        WitchMovePoint[1] = GameObject.Find("WitchMovePoint2");
        WitchMovePoint[2] = GameObject.Find("WitchMovePoint3");
        WitchMovePoint[3] = GameObject.Find("WitchMovePoint4");
        WitchMovePoint[4] = GameObject.Find("WitchMovePoint5");


        WitchAnim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    // 円移動--------------------------------------------------------------------------
    public void MoveToCircle()
    {
        // 経過時間の取得
        float time = Time.time;
        // 円運動の座標演算
        pos.x = 29f + Mathf.Sin(time) * 3f;
        pos.y = Mathf.Cos(time) * 3f;
        pos.z = -5f;
        // オブジェクトに座標を代入
        transform.position = pos;


        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = TargetObj.transform.position;
        dis = Vector3.Distance(Apos, Bpos);
    }

    // ８の字移動-----------------------------------------------------------------------
    public void MoveToFigureOfEight()
    {
        // 経過時間の取得
        float time = Time.time;
        // ８の字移動の座標演算
        pos.x = 27.6f + Mathf.Cos(time) * m_radius;
        pos.y = Mathf.Sin(time * 2f) * 2f;
        pos.z = -5f;

        // オブジェクトに座標を代入
        transform.position = pos;


        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = TargetObj.transform.position;
        dis = Vector3.Distance(Apos, Bpos);
    }

    // 五芒星の軌道---------------------------------------------------------------------
    public void MoveToStar()
    {
        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = WitchMovePoint[NextMovePointNum].transform.position;
        dis = Vector3.Distance(Apos, Bpos);

        //目標の位置に達したら次の場所へ動くように
        if (dis <= 0f)
        {
            NextMovePointNum++;
        }

        //numをリセット。最初の場所へ
        if (NextMovePointNum == 5)
        {
            NextMovePointNum = 0;
        }

        //動くスピード
        step = Time.deltaTime * speed;
        //魔女の位置を更新
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, WitchMovePoint[NextMovePointNum].transform.position, step);
    }


    //攻撃を喰らった魔女が次の行動に移すインターバル
    //指定した位置に移動するように処理
    public void WitchStandby()
    {
        animInfo = WitchAnim.GetCurrentAnimatorStateInfo(0);

        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = gameObject.transform.position;
        Vector3 Bpos = TargetObj.transform.position;
        dis = Vector3.Distance(Apos, Bpos);

        //動くスピード
        step = Time.deltaTime * speed;

        //アニメーションがDefaultになったら処理
        if (animInfo.nameHash == Animator.StringToHash("Base Layer.Default"))
        {
            //魔女の位置を更新
            gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, TargetObj.transform.position, step);
        }
    }

}
