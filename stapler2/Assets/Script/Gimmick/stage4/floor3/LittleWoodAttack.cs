using UnityEngine;
using System.Collections;

//==================================================
//ステージ4の小さい木の攻撃を操作するスクリプト
//==================================================

public class LittleWoodAttack : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //発射する弾を格納する変数
    public GameObject WoodApple;
    //弾の発射タイミング
    private float ShotTime;
    //Animatorを取得
    private Animator WoodAnim;

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
    public bool GetAttackFlag()
    {
        return WoodAttackFlag;
    }

    public RootsAnim[] rootsAnim;


    private bool SoundFlag;

    // Use this for initialization
    void Start()
    {
        ShotTime = 0;
        WoodAnim = GetComponent<Animator>();
        WoodStopFlag = false;
        WoodAttackFlag = false;
        SoundFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        WoodAttackAnim();
        WoodStop();


        if (WoodStopFlag == true)
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
                WoodAnim.SetTrigger("Attack");
                //攻撃している時のSEを再生
                AudioManager.Instance.PlaySE("wood");
                //攻撃中かのフラグ
                WoodAttackFlag = true;
            }

            //timeが4.5f以上になったら処理
            if (ShotTime >= 4.0f)
            {
                //弾をInstantiateで作って発射している風に見せる
                Instantiate(WoodApple, new Vector3(29.3f, 1.9f, -0.4f), Quaternion.identity);
                //このタイミングでも一度変数を初期化
                ShotTime = 0;
                //攻撃が終了したのでフラグをfalseに
                WoodAttackFlag = false;
                WoodAnim.ResetTrigger("Attack");
            }
        }
        else
        {
            WoodAnim.Stop();
        }
    }


    void WoodStop()
    {
        for (int i = 0; i < 4; i++)
        {
            //根っこが止められたかのフラグを調べて全部止められたら
            //木のアニメーションを止める
            if (rootsAnim[i].GetAnimStopFlag() == false)
            {
                WoodStopFlag = false;
                break;
            }
            else
            {
                WoodStopFlag = true;
            }
        }
    }

}
