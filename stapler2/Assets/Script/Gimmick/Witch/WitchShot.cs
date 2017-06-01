using UnityEngine;
using System.Collections;

//==================================================================
//魔女の攻撃を制御するスクリプト
//==================================================================


public class WitchShot : MonoBehaviour {


    // 変数宣言----------------------------------------------------------------------
    //発射する弾のオブジェクトを格納する変数
    public GameObject[] WitchFlame;
    //弾の発射タイミング
    private float ShotTime;
    //攻撃が出現する場所
    private Vector3 ShotPos;
    //攻撃を打ち出していいかのフラグ
    public AliceMove_Stage3 reaching;

    //このスクリプトを持つオブジェクト
    public GameObject objA;
    //距離を比べるオブジェクト
    public GameObject objB;

    //アニメーションを取得
    private Animator WitchAttackAnim;

    //魔女の発射する弾を切り替える
    private int WitchShotChangeNum;

    // Use this for initialization
    void Start () {
        ShotTime = 0f;
        WitchAttackAnim = GetComponent<Animator>();
        WitchShotChangeNum = 0;
    }
	
	// Update is called once per frame
	void Update () {

        ShotPos = gameObject.transform.position;
        ShotPos.x -= 2f;

        Vector3 Apos = objA.transform.position;
        Vector3 Bpos = objB.transform.position;
        float dis = Vector3.Distance(Apos, Bpos);

        //アリスとの距離が9f以上離れていて
        if (dis >= 9f)
        {
            //3フロア目に到達していたら処理を行う
            if (reaching.GetFloor3MoveEndFlag() == true)
            {
                ShotTime += Time.deltaTime;

                //timeが2f以上になったら処理
                if (ShotTime >= 2f)
                {
                    WitchAttackAnim.SetTrigger("Attack");
                    //弾をInstantiateで作って発射している風に見せる
                    Instantiate(WitchFlame[WitchShotChangeNum], ShotPos, Quaternion.identity);
                    //弾の発射音を再生
                    AudioManager.Instance.PlaySE("se_maoudamashii_magical12");
                    //timeを初期化する
                    ShotTime = 0f;

                    WitchShotChangeNum++;
                }
            }
        }

        if (WitchShotChangeNum == 2)
        {
            WitchShotChangeNum = 0;
        }

    }
}
