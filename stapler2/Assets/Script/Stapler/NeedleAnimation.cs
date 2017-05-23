using UnityEngine;
using System.Collections;

public class NeedleAnimation : MonoBehaviour
{

    //====================================================================
    //正解以外の場所にタップした時の針のアニメーション再生
    //====================================================================

    // 変数宣言----------------------------------------------------------------------

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //弾かれるアニメーションを持つ針
    private GameObject[] MoveNeedle = new GameObject[15];
    private Animation[] NeedleAnim = new Animation[15];

    //再生する針の順番を決めるための変数を持つスクリプト
    public NeedleAnimPlayNum num;

    // Use this for initialization
    void Start()
    {

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
       
        //スクリプトを持つオブジェクトをタップした時に針が弾かれるように処理を行う
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
