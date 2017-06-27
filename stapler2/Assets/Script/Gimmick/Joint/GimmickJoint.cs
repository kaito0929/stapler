using UnityEngine;
using System.Collections;

//==========================================================
//オブジェクト同士をくっつけるスクリプト
//当たっているオブジェクトの指定が必要ない場合
//==========================================================


public class GimmickJoint : MonoBehaviour
{
    // 変数宣言----------------------------------------------------------------------

    //タップしたかのフラグ
    private bool TapFlag;
    //別のスクリプトに判定を渡す関数
    public bool GetTapFlag()
    {
        return TapFlag;
    }

    //何かとぶつかっているかのフラグ
    private bool CollFlag;

    //　針関係------------------------------------------------------------------------
    //Ray関係
    //ホッチキスの針を移動させるのに使う
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //失敗した時に弾かれる針のオブジェクト
    private GameObject[] MoveNeedle = new GameObject[15];
    private Animation[] NeedleAnim = new Animation[15];

    //再生する針の順番を決めるための変数を持つスクリプト
    public NeedleAnimPlayNum num;

    //--------------------------------------------------------------------------------

    //パーティクルの色を変化させるための変数
    public Renderer rd;




    // Use this for initialization
    void Start()
    {
        TapFlag = false;
        CollFlag = false;


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
        NeedleAnimPlay();
    }

    void OnTriggerStay(Collider other)
    {
        //タップしたものがgameObjectだった場合に処理
        if (TouchManager.SelectedGameObject == gameObject)
        {
            //ぶつかっている相手と親子関係になる
            gameObject.transform.parent = other.transform;

            if (TapFlag == false)
            {
                //パーティクルの色を変える
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

            TapFlag = true;
        }

        else if (TouchManager.SelectedGameObject != gameObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //パーティクルの色を変える
                rd.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
            }
        }


        CollFlag = true;
    }

    void OnTriggerExit(Collider other)
    {
        CollFlag = false;
    }


    //針の弾かれるアニメーションを再生するための関数
    void NeedleAnimPlay()
    {
        //指定したオブジェクトとタップしたオブジェクトが一緒なら処理
        if (TouchManager.SelectedGameObject == gameObject)
        {
            if (CollFlag == false)
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

}
