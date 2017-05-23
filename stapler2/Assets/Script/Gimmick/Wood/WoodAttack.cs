using UnityEngine;
using System.Collections;

public class WoodAttack : MonoBehaviour
{

    //================================================================
    //木が攻撃している風に見せるスクリプト
    //木の根っこをタップすれば動きを止めてクリアにする
    //根っこをタップするタイミングは木が攻撃している間のみ
    //================================================================

    // 変数宣言----------------------------------------------------------------------

    //発射する弾を格納する変数
    public GameObject WoodApple;
    //弾の発射タイミング
    private float ShotTime;
    //Animatorを取得
    private Animator WoodAnim;

    //木を止めるために必要な回数
    private int WoodStopNorma;
    //木が止まったかのフラグ
    private bool WoodStopFlag;
    //他のスクリプトへ変数を渡すための関数
    public bool StopFlag()
    {
        return WoodStopFlag;
    }

    //木が攻撃している間にtrueになるフラグ
    //このフラグがtrueの間にタップすることでホッチキスで止めることが出来る
    private bool WoodAttackFlag;

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject[] Needle;
    private int NeedleMoveNum;

    //失敗した時に弾かれる針のオブジェクト
    private GameObject[] MoveNeedle = new GameObject[15];
    private Animation[] NeedleAnim = new Animation[15];

    //再生する針の順番を決めるための変数を持つスクリプト
    public NeedleAnimPlayNum num;

    // Use this for initialization
    void Start()
    {
        ShotTime = 0;
        WoodAnim = GetComponent<Animator>();
        WoodStopFlag = false;
        WoodAttackFlag = false;
        WoodStopNorma = 6;


        MoveNeedle[0] = GameObject.Find("RepelledNeedle1");
        MoveNeedle[1] = GameObject.Find("RepelledNeedle2");
        MoveNeedle[2] = GameObject.Find("RepelledNeedle3");
        MoveNeedle[3] = GameObject.Find("RepelledNeedle4");
        MoveNeedle[4] = GameObject.Find("RepelledNeedle5");
        MoveNeedle[5] = GameObject.Find("RepelledNeedle6");
        MoveNeedle[6] = GameObject.Find("RepelledNeedle7");
        MoveNeedle[7] = GameObject.Find("RepelledNeedle8");
        MoveNeedle[8] = GameObject.Find("RepelledNeedle9");
        MoveNeedle[9] = GameObject.Find("RepelledNeedle10");
        MoveNeedle[10] = GameObject.Find("RepelledNeedle11");
        MoveNeedle[11] = GameObject.Find("RepelledNeedle12");
        MoveNeedle[12] = GameObject.Find("RepelledNeedle13");
        MoveNeedle[13] = GameObject.Find("RepelledNeedle14");
        MoveNeedle[14] = GameObject.Find("RepelledNeedle15");

        for (int i = 0; i < 15; i++)
        {
            NeedleAnim[i] = MoveNeedle[i].GetComponent<Animation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        WoodAttackAnim();
        WoodTapStop();
        

        if (WoodStopNorma == 0)
        {
            WoodStopFlag = true;
        }

        
    }


    void WoodAttackAnim()
    {
        ShotTime += Time.deltaTime;

        //木が止められた判定を取られていなければ処理する
        if (WoodStopFlag == false)
        {
            //timeが3f以上になったら処理
            if (ShotTime >= 3f && ShotTime <= 3.5f)
            {
                //木のアニメーションを再生
                WoodAnim.SetBool("attack", true);
                //攻撃している時のSEを再生
                AudioManager.Instance.PlaySE("wood");
                //攻撃中かのフラグ
                WoodAttackFlag = true;
            }

            //timeが4.5f以上になったら処理
            if (ShotTime >= 4.0f)
            {
                //弾をInstantiateで作って発射している風に見せる
                Instantiate(WoodApple, new Vector3(-4.69f, 6.75f, -5.2f), Quaternion.identity);
                //このタイミングでも一度変数を初期化
                ShotTime = 0;
                //攻撃が終了したのでフラグをfalseに
                WoodAttackFlag = false;
                WoodAnim.SetBool("attack", false);
            }
        }
        else
        {
            //三回以上タップされたのならアニメーションの再生を止める
            WoodAnim.Stop();
        }
    }


    void WoodTapStop()
    {

        //木が攻撃中じゃないと処理を行わないように
        if (WoodAttackFlag == true && WoodStopFlag == false)
        {
            //根っこの部分がタップされるとノルマが減る
            //それに加えて根っこの部分に針を付ける処理も行う
            if (TouchManager.SelectedGameObject == gameObject)
            {
                WoodStopNorma--;

                //Rayを飛ばして
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //針の位置をタップした位置へと移動させる。
                    Needle[NeedleMoveNum].transform.position = hit.point;
                    //gameObjectと親子関係に
                    Needle[NeedleMoveNum].transform.parent = gameObject.transform;
                }

                //くっつける針を切り替えるために変数を加算する
                if (NeedleMoveNum != 5)
                {
                    NeedleMoveNum++;
                }
            }
        }
        else
        {
            NeedleAnimPlay();
        }
    }


    //針の弾かれるアニメーションを再生するための関数
    void NeedleAnimPlay()
    {
        //指定したオブジェクトとタップしたオブジェクトが一緒なら処理
        if (TouchManager.SelectedGameObject == gameObject)
        {
            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針をタップした場所へと移動
                MoveNeedle[num.GetAnimPlayNum()].transform.position = hit.point;
            }

            //針が既に再生中だったら処理
            if (NeedleAnim[num.GetAnimPlayNum()].isPlaying)
            {
                //アニメーションを一時停止させる
                NeedleAnim[num.GetAnimPlayNum()].Stop();
            }

            //アニメーションを再生させる
            NeedleAnim[num.GetAnimPlayNum()].Play();

        }
    }

}
