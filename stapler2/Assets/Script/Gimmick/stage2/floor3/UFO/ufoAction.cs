using UnityEngine;
using System.Collections;

//=======================================================
//UFOの行動を制御するスクリプト
//=======================================================

public class ufoAction : MonoBehaviour {

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


    //このベクトルを加算して斜めに吹っ飛んでいくように見せる
    private Vector3 UfoPos;


    //ホッチキスによって止められていないかのフラグ
    private bool MoveStopFlag;

    //次の場所へと移動するまでの間の時間
    private float CoolTime = 1.0f;
    //移動してから止まっている間に時間を加算してCoolTimeを超えたら移動するように
    private float StopTime;


    //UFOが猫に当たったかのフラグ
    private bool ufoCollFlag;
    public bool GetUfoCollFlag()
    {
        return ufoCollFlag;
    }

    //弾の発射を制御するフラグ
    private bool ShotFlag;


    //移動先を決定するランダムの数値を受け取る変数
    private int MovePoint_RandomNum;

    private int rand;

    //UFOがランダムで移動する場所のオブジェクト
    //このオブジェクトの座標をUFOに代入して瞬間移動したように見せる
    public GameObject[] ufoMovePoint;


    //Ray関係
    //ホッチキスの針を付けるために必要
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //発射する弾を格納する変数
    public GameObject ufoThunder;


    //フロア2への移動が終了したかのフラグを持つオブジェクト
    public AliceMove aliceMove;

    //UFOのアニメーターを取得
    private Animator ufoAnim;
    private AnimatorStateInfo animInfo;

    //パーティクルの色を変化させるための変数
    public Renderer rd;

    //正解した時の音を再生するためのフラグ
    private bool SoundFlag;


    void OnTriggerEnter(Collider other)
    {
        //UFOが猫と当たった場合にMoveDirをCOLLに変更
        //くるくると回転しながら飛んでいくように
        if (other.gameObject.name == "Bone020")
        {
            //回転して吹っ飛ぶように変更
            moveDir = ufoState.COLL;
            
            ufoCollFlag = true;
        }
    }

    
    // Use this for initialization
    void Start () {
        moveDir = ufoState.MOVE;
        MoveStopFlag = false;
        ShotFlag = false;
        ufoCollFlag = false;
        SoundFlag = false;

        ufoAnim = GetComponent<Animator>();
        animInfo = ufoAnim.GetCurrentAnimatorStateInfo(0);

        MovePoint_RandomNum = Random.Range(0, 3);
        rand = MovePoint_RandomNum;
    }
	
	// Update is called once per frame
	void Update () {

        ufoUpdate();
        ufoTapStop();

        //UFOが猫に当たった時に正解音を一度だけ再生するようにする
        if(ufoCollFlag==true)
        {
            if (SoundFlag == true)
            {
                AudioManager.Instance.PlaySE("correct1_01");
                SoundFlag = false;
            }
        }
        else
        {
            SoundFlag = true;
        }

    }


    //UFOの行動
    void ufoUpdate()
    {
        animInfo = ufoAnim.GetCurrentAnimatorStateInfo(0);

        //アリスがフロア3に移動してきたというフラグがtrueになれば処理
        if (aliceMove.GetFloor3MoveEndFlag() == true)
        {
            //UFOの進む方向を変更する
            switch (moveDir)
            {
                case ufoState.MOVE://瞬間移動のように移動

                    //タップされたかのフラグがfalse、動きが止まっていない状態なら処理
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
                            if (MovePoint_RandomNum < 2 && ShotFlag==false)
                            {
                                ufoAnim.SetBool("Attack", true);                          
                            }
                            else if (MovePoint_RandomNum == 2)
                            {
                                StopTime += Time.deltaTime;
                            }

                        }


                        if (animInfo.nameHash == Animator.StringToHash("Base Layer.Attack"))
                        {
                            if (animInfo.normalizedTime >= 0.9f)
                            {
                                ufoAnim.SetBool("Attack", false);
                                ufoShot();
                                ShotFlag = true;
                            }
                        }

                        if (ShotFlag == true)
                        {
                            StopTime += Time.deltaTime;
                        }

                        //移動してから設定しているクールタイムを超えたなら処理
                        if (StopTime >= CoolTime)
                        {
                            //StopTimeを初期化
                            StopTime = 0;

                            AudioManager.Instance.PlaySE("syunnkann");

                            //再びランダムで数値を取得する
                            MovePoint_RandomNum = Random.Range(0, 3);

                            while (rand == MovePoint_RandomNum)
                            {
                                MovePoint_RandomNum = Random.Range(0, 3);
                            }

                            rand = MovePoint_RandomNum;

                            ShotFlag = false;
                        }
                    }
                    else
                    {
                        if (ShotFlag==false)
                        {
                            ufoAnim.SetBool("Attack", true);
                        }

                        if (animInfo.nameHash == Animator.StringToHash("Base Layer.Attack"))
                        {
                            if (animInfo.normalizedTime >= 0.9f)
                            {
                                ufoAnim.SetBool("Attack", false);
                                ufoShot();
                                ShotFlag = true;
                            }
                        }
                    }


                    break;

                case ufoState.COLL://猫に当たった場合に処理

                    //UFOの現在値をposへ代入
                    UfoPos = gameObject.transform.position;

                    //左斜め上に吹っ飛んでいくように加算と減算を行う
                    UfoPos.x -= 0.1f;
                    UfoPos.y += 0.1f;
                    //くるくると回転する
                    gameObject.transform.Rotate(new Vector3(0, 0, 5));

                    //UFOのpositionへposを代入
                    gameObject.transform.position = UfoPos;

                    break;
            }
        }
    }


    //UFOの弾発射の関数
    void ufoShot()
    {
        if (ShotFlag==false)
        {
            //弾をInstantiateで作って発射している風に見せる
            Instantiate(ufoThunder, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity);
            //弾の発射音を再生
            AudioManager.Instance.PlaySE("robot-eye-flash1_01");
            //ShotTimeを0以上にして連続で弾を発射できないようにする
            //ShotFlag
        }

    }


    void ufoTapStop()
    {
        if (MoveStopFlag == false)
        {
            //UFOがタップされたならフラグをfalseにしておいて移動を止める
            if (TouchManager.SelectedGameObject == gameObject)
            {
                rd.GetComponent<Renderer>().material.color = new Color(255.0f / 255.0f, 143.0f / 255.0f, 247.0f / 255.0f);

                //Rayを飛ばして
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //針の位置をタップした位置へと移動させる。
                    Needle.transform.position = hit.point;
                    //gameObjectと親子関係に
                    Needle.transform.parent = gameObject.transform;
                }
                
                MoveStopFlag = true;
            }
        }

        //移動が止まっていたらその時間を加算
        if (MoveStopFlag == true)
        {
            StopTime += Time.deltaTime;
        }

        //一定時間動きが止まっていたら移動を再開
        if (StopTime >= 5f)
        {
            MoveStopFlag = false;
            StopTime = 0;
            Needle.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
            //ShotTime = 0;
            ShotFlag = false;
        }
    }
  
}
