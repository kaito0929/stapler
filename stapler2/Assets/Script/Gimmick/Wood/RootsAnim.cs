using UnityEngine;
using System.Collections;

//=================================================
//木の根っこのアニメーション制御
//=================================================

public class RootsAnim : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------
    
    private Animation anim;

    //木の攻撃が行われているかのフラグ
    public WoodAttack woodAttack;

    //根っこのアニメーション制御用のフラグ
    private bool AnimStopFlag;
    public bool GetAnimStopFlag()
    {
        return AnimStopFlag;
    }


    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;


    //失敗した時に弾かれる針のオブジェクト
    public GameObject[] MoveNeedle;
    private Animation[] NeedleAnim = new Animation[15];

    //再生する針の順番を決めるための変数を持つスクリプト
    public NeedleAnimPlayNum num;

    //パーティクルの色を変化させるための変数
    public Renderer rd;

    private float time;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        AnimStopFlag = false;

        for (int i = 0; i < 15; i++)
        {
            NeedleAnim[i] = MoveNeedle[i].GetComponent<Animation>();
        }

        time = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //木が攻撃時に処理が行われるようにする
        if (woodAttack.GetAttackFlag() == true)
        {
            if (TouchManager.SelectedGameObject == gameObject)
            {
                if (AnimStopFlag == false)
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
                }
                AnimStopFlag = true;
            }
            anim.Stop();
        }
        else if (woodAttack.GetAttackFlag() == false && AnimStopFlag == false)
        {
            anim.Play();
            NeedleAnimPlay();
        }
        

        //木のアニメーションが止められたら時間をtimeに加算
        if(AnimStopFlag==true)
        {
            if (time >= 0)
            {
                time += Time.deltaTime;
            }
        }

        //timeが1fを超えたならパーティクルの色を白に戻す
        //その際にtimeを-1fに変えておいて変数に加算させないようにする
        if (time > 1f)
        {
            //パーティクルの色を変える
            rd.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            time = -1f;
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
