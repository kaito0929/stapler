using UnityEngine;
using System.Collections;

//==================================================================
//魔女の攻撃を制御するスクリプト
//==================================================================


public class WitchShot : MonoBehaviour {


    // 変数宣言----------------------------------------------------------------------
    //発射する弾のオブジェクトを格納する変数
    public GameObject[] WitchAttack;
    //弾の発射タイミング
    private float ShotTime;
    //攻撃が出現する場所
    private Vector3[] ShotPos=new Vector3[3];

    //このスクリプトを持つオブジェクト
    public GameObject objA;
    //距離を比べるオブジェクト
    public GameObject objB;

    //アニメーションを取得
    private Animator WitchAttackAnim;

    private float Distance;

    // Use this for initialization
    void Start () {
        ShotTime = 0f;
        WitchAttackAnim = GetComponent<Animator>();
        Distance = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < 3; i++)
        {
            ShotPos[i] = gameObject.transform.position;
            ShotPos[i].x -= 2f;
        }

        ShotPos[1].y -= 2f;
        ShotPos[2].y += 2f;
    }


    //第一段階での魔女の攻撃関数
    public void WitchShotCreate(int num, int max)
    {
        Vector3 Apos = objA.transform.position;
        Vector3 Bpos = objB.transform.position;
        Distance = Vector3.Distance(Apos, Bpos);

        //アリスとの距離が9f以上離れていて
        if (Distance >= 8f)
        {
            ShotTime += Time.deltaTime;

            //timeが2f以上になったら処理
            if (ShotTime >= 2f)
            {
                WitchAttackAnim.SetTrigger("Attack");

                while (num < max)
                {
                    //弾をInstantiateで作って発射している風に見せる
                    Instantiate(WitchAttack[num], ShotPos[num], Quaternion.identity);
                    num++;
                }

                //弾の発射音を再生
                AudioManager.Instance.PlaySE("se_maoudamashii_magical12");
                //timeを初期化する
                ShotTime = 0f;

                num = 0;
            }
        }

    }


}
