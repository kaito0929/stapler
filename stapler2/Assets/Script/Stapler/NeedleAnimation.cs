using UnityEngine;
using System.Collections;

public class NeedleAnimation : MonoBehaviour
{

    //====================================================================
    //正解以外の場所にタップした時の針のアニメーション再生
    //アニメーションを制御する関数を用意する
    //====================================================================

    // 変数宣言----------------------------------------------------------------------

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //表示させる針
    private GameObject[] MoveNeedle = new GameObject[15];
    private Animation[] NeedleAnim = new Animation[15];
    //表示する針の順番
    private int MoveNeedleNumber;

    //弾かれるアニメーションの針の最大数
    private int NeedleMaxNum;

    public NeedleAnimPlayNum num;

    // Use this for initialization
    void Start()
    {
        MoveNeedleNumber = 0;
        NeedleMaxNum = 8;

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
       
        //指定したオブジェクトとタップしたオブジェクトが一緒なら処理
        if (TouchManager.SelectedGameObject == gameObject)
        {
            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針をタップした場所へと移動
                MoveNeedle[num.GetAnimPlayNum()].transform.position = hit.point;

                if (NeedleAnim[num.GetAnimPlayNum()].isPlaying)
                {
                    //アニメーションを再生させる
                    NeedleAnim[num.GetAnimPlayNum()].Stop();
                }

                //アニメーションを再生させる
                NeedleAnim[num.GetAnimPlayNum()].Play();
            }
        }

    }

    public void NeedelAnimPlay()
    {
        ////9になると一巡したことになるので数字を0に戻す
        //if (MoveNeedleNumber == NeedleMaxNum)
        //{
        //    MoveNeedleNumber = 0;
        //}

        ////指定したオブジェクトとタップしたオブジェクトが一緒なら処理
        //if (TouchManager.SelectedGameObject == gameObject)
        //{
        //    //Rayを飛ばして
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit, 100f))
        //    {
        //        //針をタップした場所へと移動
        //        MoveNeedle[MoveNeedleNumber].transform.position = hit.point;

        //        if (!NeedleAnim[MoveNeedleNumber].isPlaying)
        //        {
        //            NeedleAnim[MoveNeedleNumber].Stop();
        //        }

        //        //アニメーションを再生させる
        //        NeedleAnim[MoveNeedleNumber].Play();

        //        //移動させる針を切り替える
        //        MoveNeedleNumber++;
        //    }
        //}

    }
}
