using UnityEngine;
using System.Collections;

public class WitchShot : MonoBehaviour {

    //==================================================================
    //魔女の攻撃を制御するスクリプト
    //==================================================================

    // 変数宣言----------------------------------------------------------------------
    //発射する弾のオブジェクトを格納する変数
    public GameObject WitchFlame;
    //弾の発射タイミング
    public float time;
    //火球を打ち出す座標
    private Vector3 pos;
    //火球を打ち出していいかのフラグ
    public AliceMove_Stage3 reaching;

    //このスクリプトを持つオブジェクト
    public GameObject objA;
    //距離を比べるオブジェクト
    public GameObject objB;

    //アニメーションを取得
    private Animator anim;

    // Use this for initialization
    void Start () {
        time = 0f;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        pos = gameObject.transform.position;
        pos.x -= 2f;

        Vector3 Apos = objA.transform.position;
        Vector3 Bpos = objB.transform.position;
        float dis = Vector3.Distance(Apos, Bpos);

        //アリスとの距離が9f以上離れていて
        if (dis >= 9f)
        {
            //3フロア目に到達していたら処理を行う
            if (reaching.GetReachingFlag() == true)
            {
                time += Time.deltaTime;

                //timeが2f以上になったら処理
                if (time >= 2f)
                {
                    anim.SetTrigger("Attack");
                    //弾をInstantiateで作って発射している風に見せる
                    Instantiate(WitchFlame, pos, Quaternion.identity);
                    //弾の発射音を再生
                    AudioManager.Instance.PlaySE("se_maoudamashii_magical12");
                    //timeを初期化する
                    time = 0f;
                }
            }
        }

    }
}
