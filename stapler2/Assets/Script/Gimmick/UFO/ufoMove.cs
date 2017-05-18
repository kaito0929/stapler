using UnityEngine;
using System.Collections;

public class ufoMove : MonoBehaviour {

    //=======================================================
    //UFOの行動を制御するスクリプト
    //=======================================================

    // 変数宣言----------------------------------------------------------------------

    //移動する方向
    private enum ufoState
    {
        MOVE,
        COLL,
    }
    //enumを変数として宣言
    //この変数でUFOの行動を決定
    private ufoState moveDir;


    //このベクトルを加算か減算して左右に動かす
    private Vector3 pos;


    //ホッチキスによって止められていないかのフラグ
    private bool MoveStopFlag;
    //弾の発射制御のスクリプトへ変数を渡すための関数
    public bool GetMoveFlag()
    {
        return MoveStopFlag;
    }


    //猫に当たって吹き飛ばされているかのフラグ
    //クリアしているかの判断にもなる
    private bool CatCollFlag;
    //クリア判定を別のスクリプトに渡すための関数
    public bool GetClearFlag()
    {
        return CatCollFlag;
    }


    //次の場所へと移動するまでの間の時間
    private float CoolTime = 1.0f;

    //止まっている間に時間を加算してCoolTimeを超えたら移動するように
    private float StopTime;

    //弾の発射の為の変数
    public float ShotTime;

    //移動先を決定するランダムの数値を受け取る変数
    private int MovePoint_RandomNum;
    
    //UFOがランダムで移動する場所のオブジェクト
    //このオブジェクトの座標をUFOに代入して瞬間移動したように見せる
    private GameObject[] ufoMovePoint = new GameObject[8];

    //アリスがどのステージに達しているかのフラグ(ステージ2)
    //ステージの最初からと操作するために必要
    public static bool AliceStage2Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage2Flag()
    {
        return AliceStage2Flag;
    }

    //Ray関係
    //ホッチキスの針を付けるために必要
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //発射する弾を格納する変数
    public GameObject ufoThunder;


    //フロア2への移動が終了したかのフラグを持つオブジェクト
    public AliceMove_Stage2 Alice_MoveEnd;

    void OnTriggerEnter(Collider other)
    {
        //UFOが猫と当たった場合にMoveDirをCOLLに変更
        //くるくると回転しながら飛んでいくように
        if (other.gameObject.name == "Bone020")
        {
            //回転して吹っ飛ぶように変更
            moveDir = ufoState.COLL;
            //アリスがステージ2に移動するので
            //アリスがステージ1にいるというフラグはfalseにする
            AliceStage2Flag = false;
            //これがクリアしたかのフラグになっている
            CatCollFlag = true;
        }
    }

    private float time;


    // Use this for initialization
    void Start () {
        moveDir = ufoState.MOVE;
        MoveStopFlag = false;
        CatCollFlag = false;
        ShotTime = 0f;

        MovePoint_RandomNum = Random.Range(0, 8);

        ufoMovePoint[0] = GameObject.Find("ufoMovePoint1");
        ufoMovePoint[1] = GameObject.Find("ufoMovePoint2");
        ufoMovePoint[2] = GameObject.Find("ufoMovePoint3");
        ufoMovePoint[3] = GameObject.Find("ufoMovePoint4");
        ufoMovePoint[4] = GameObject.Find("ufoMovePoint5");
        ufoMovePoint[5] = GameObject.Find("ufoMovePoint6");
        ufoMovePoint[6] = GameObject.Find("ufoMovePoint7");
        ufoMovePoint[7] = GameObject.Find("ufoMovePoint8");
    }
	
	// Update is called once per frame
	void Update () {

        if (Alice_MoveEnd.GetFloar3MoveEndFlag() == true)
        {
            //UFOの進む方向を変更する
            switch (moveDir)
            {
                case ufoState.MOVE://右方向へ移動

                    //タップされたかのフラグがfalse、動きが止まっている状態なら処理
                    if (MoveStopFlag == false)
                    {
                        //UFOの座標と移動先候補のオブジェクトの座標が違うならば処理
                        if (gameObject.transform.position != ufoMovePoint[MovePoint_RandomNum].transform.position)
                        {
                            //UFOと移動先の座標を代入して瞬間移動している風に見せる
                            gameObject.transform.position = ufoMovePoint[MovePoint_RandomNum].transform.position;
                        }
                        else
                        {
                            ufoShot();
                            //移動した後から時間を加算
                            StopTime += Time.deltaTime;
                        }

                        //移動してから設定しているクールタイムを超えたなら処理
                        if (StopTime >= CoolTime)
                        {
                            //StopTimeを初期化
                            StopTime = 0;
                            //再びランダムで数値を取得する
                            MovePoint_RandomNum = Random.Range(0, 8);

                            ShotTime = 0;
                        }
                    }
                    else
                    {
                        ShotTime += Time.deltaTime;

                        if (ShotTime >= 3f)
                        {
                            //弾をInstantiateで作って発射している風に見せる
                            Instantiate(ufoThunder, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                            //弾の発射音を再生
                            AudioManager.Instance.PlaySE("robot-eye-flash1_01");
                            ShotTime = 0;
                        }
                    }

                    break;

                case ufoState.COLL://猫に当たった場合に処理

                    //UFOの現在値をposへ代入
                    pos = gameObject.transform.position;

                    //左斜め上に吹っ飛んでいくように加算と減算を行う
                    pos.x -= 0.1f;
                    pos.y += 0.1f;
                    //くるくると回転する
                    gameObject.transform.Rotate(new Vector3(0, 0, 5));

                    //UFOのpositionへposを代入
                    gameObject.transform.position = pos;

                    break;
            }
        }


        //UFOがタップされたならフラグをfalseにしておいて移動を止める
        if(TouchManager.SelectedGameObject==gameObject)
        {
            MoveStopFlag = true;
            ShotTime = 0;

            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針の位置をタップした位置へと移動させる。
                Needle.transform.position = hit.point;
                //gameObjectと親子関係に
                Needle.transform.parent = gameObject.transform;
            }
        }

        //移動が止まっていたらその時間を加算
        if(MoveStopFlag==true)
        {
            time += Time.deltaTime;
        }
        
        //一定時間動きが止まっていたら移動を再開
        if (time >= 5f)
        {
            MoveStopFlag = false;
            time = 0;
            Needle.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
        }

        

	}

    //UFOの弾発射の関数
    void ufoShot()
    {
        if (MoveStopFlag == false)
        {
            if (MovePoint_RandomNum < 5 && ShotTime <= 0)
            {
                //弾をInstantiateで作って発射している風に見せる
                Instantiate(ufoThunder, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                //弾の発射音を再生
                AudioManager.Instance.PlaySE("robot-eye-flash1_01");
                //ShotTimeを0以上にして連続で弾を発射できないようにする
                ShotTime = 5f;
            }
        }
    }

  
}
