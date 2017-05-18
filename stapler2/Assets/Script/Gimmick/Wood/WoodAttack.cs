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
    private float time;
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

    //NeedleManagerを取得する
    private GameObject NeedleManager;

    // Use this for initialization
    void Start()
    {
        time = 0;
        WoodAnim = GetComponent<Animator>();
        WoodStopFlag = false;
        WoodAttackFlag = false;
        WoodStopNorma = 6;

        NeedleManager = GameObject.Find("NeedleManager");
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
        time += Time.deltaTime;

        //木が止められた判定を取られていなければ処理する
        if (WoodStopFlag == false)
        {
            //timeが3f以上になったら処理
            if (time >= 3f && time <= 3.5f)
            {
                //木のアニメーションを再生
                WoodAnim.SetBool("attack", true);
                //攻撃している時のSEを再生
                AudioManager.Instance.PlaySE("wood");
                //攻撃中かのフラグ
                WoodAttackFlag = true;
            }

            //timeが4.5f以上になったら処理
            if (time >= 4.0f)
            {
                //弾をInstantiateで作って発射している風に見せる
                Instantiate(WoodApple, new Vector3(-4.69f, 6.75f, -5.2f), Quaternion.identity);
                //このタイミングでも一度変数を初期化
                time = 0;
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
        //NeedleAnimation内の関数を使用できるように
        NeedleAnimation needle = NeedleManager.GetComponent<NeedleAnimation>();

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
            if (TouchManager.SelectedGameObject == gameObject)
            {
                //針のアニメーションを再生
                needle.NeedelAnimPlay();
            }
        }
    }
}
