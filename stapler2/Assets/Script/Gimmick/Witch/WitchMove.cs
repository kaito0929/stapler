using UnityEngine;
using System.Collections;

public class WitchMove : MonoBehaviour {

    //===================================================
    //魔女の動きを制御するスクリプト
    //===================================================

    // 変数宣言----------------------------------------------------------------------

    // 移動速度
    public float m_moveSpeed = 1f;
    // 円の半径
    public float m_radius = 4f;


    //魔女が追尾するオブジェクト
    private GameObject[] WitchMovePoint=new GameObject[5];
    //移動スピード
    private float speed = 6.0f;
    //動かす変数
    private float step = 0.0f;

    int num = 1;

    public GameObject objA;
    
    public WitchColl number;

    Vector3 pos;

    // Use this for initialization
    void Start () {

        WitchMovePoint[0] = GameObject.Find("WitchMovePoint1");
        WitchMovePoint[1] = GameObject.Find("WitchMovePoint2");
        WitchMovePoint[2] = GameObject.Find("WitchMovePoint3");
        WitchMovePoint[3] = GameObject.Find("WitchMovePoint4");
        WitchMovePoint[4] = GameObject.Find("WitchMovePoint5");

    }
	
	// Update is called once per frame
	void Update () {

        switch (number.GetWitchCollNorma())
        {
            case 1:
                // 五芒星の軌道
                MoveToStar();
                break;
            case 2:
                // ８の字移動
                MoveToFigureOfEight();
                break;
            case 3:
                // 円移動
                MoveToCircle();
                break;
        }        
    }

    // 円移動--------------------------------------------------------------------------
    void MoveToCircle()
    {
        // 経過時間の取得
        float time = Time.time;
        // 円運動の座標演算
        float x = 29f + Mathf.Sin(time) * 3f;
        float y = Mathf.Cos(time) * 3f;
        float z = -5f;
        // オブジェクトに座標を代入
        transform.position = new Vector3(x, y, z);
    }

    // ８の字移動-----------------------------------------------------------------------
    void MoveToFigureOfEight()
    {
        // 経過時間の取得
        float time = Time.time;
        // ８の字移動の座標演算
        pos.x = 27.19f + Mathf.Cos(time) * m_radius;
        pos.y = Mathf.Sin(time * 2f) * 2f;
        pos.z = -5f;

        // オブジェクトに座標を代入
        transform.position = pos;
    }

    // 五芒星の軌道---------------------------------------------------------------------
    void MoveToStar()
    {
        //魔女の現在の場所と次の移動場所との距離を測る
        Vector3 Apos = objA.transform.position;
        Vector3 Bpos = WitchMovePoint[num].transform.position;
        float dis = Vector3.Distance(Apos, Bpos);

        //目標の位置に達したら次の場所へ動くように
        if (dis <= 0f)
        {
            num++;
        }

        //numをリセット。最初の場所へ
        if (num == 5)
        {
            num = 0;
        }

        //動くスピード
        step = Time.deltaTime * speed;
        //魔女の位置を更新
        gameObject.transform.position = Vector3.MoveTowards
            (gameObject.transform.position, WitchMovePoint[num].transform.position, step);
    }



}
