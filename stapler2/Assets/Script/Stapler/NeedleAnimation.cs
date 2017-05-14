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
    public GameObject[] MoveNeedle;
    public Animation[] NeedleAnim;
    //表示する針の順番
    private int MoveNeedleNumber;

    //弾かれるアニメーションの針な最大数
    private int NeedleMaxNum;

    // Use this for initialization
    void Start()
    {
        MoveNeedleNumber = 0;
        NeedleMaxNum = 8;

        for (int i = 0; i < 9; i++)
        {
            NeedleAnim[i] = MoveNeedle[i].GetComponent<Animation>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NeedelAnimPlay()
    {
        //9になると一巡したことになるので数字を0に戻す
        if (MoveNeedleNumber == NeedleMaxNum)
        {
            MoveNeedleNumber = 0;
        }

        //指定したオブジェクトとタップしたオブジェクトが一緒なら処理
        if (Input.GetMouseButtonDown(0))
        {
            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針をタップした場所へと移動
                MoveNeedle[MoveNeedleNumber].transform.position = hit.point;

                //アニメーション再生中ならば再生を止めて
                NeedleAnim[MoveNeedleNumber].Stop();
                //アニメーションを再生させる
                NeedleAnim[MoveNeedleNumber].Play();

                //移動させる針を切り替える
                MoveNeedleNumber++;
            }
        }
    }
}
