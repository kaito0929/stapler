using UnityEngine;
using System.Collections;

//=========================================================================
//熊の腕と体が直ったフラグを受け取るスクリプト
//その二つのフラグを受け取ると熊の顔のマテリアルを切り替える
//=========================================================================

public class BearClear : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //実際に直したオブジェクト（体）
    public GameObject BearBody;
    //実際に直したオブジェクト（腕）
    public GameObject BearHand;

    //体が直ったフラグを受け取る変数
    private bool[] GetRepairFlag=new bool[2];

    //表示するマテリアルを格納する変数
    //これには頭のマテリアルを格納する
    public Material[] material;
    //頭の表示するマテリアルを変更する変数
    private int BearHeadChange;

    //フロア1をクリアしたかのフラグ
    private bool ClearFlag;
    //クリアしたというフラグを他のスクリプトへ渡す関数
    public bool GetClearFlag()
    {
        return ClearFlag;
    }

    //正解した時の音を再生するためのフラグ
    private bool SoundFlag;


    // Use this for initialization
    void Start () {
        GetRepairFlag[0] = false;
        GetRepairFlag[1] = false;
        BearHeadChange = 0;
        ClearFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

        //体の修理したかのフラグを受け取る
        BearRepair body = BearBody.GetComponent<BearRepair>();
        GetRepairFlag[0] = body.GetRepairFlag();

        //腕の修理したかのフラグを受け取る
        BearRepair hand = BearHand.GetComponent<BearRepair>();
        GetRepairFlag[1] = hand.GetRepairFlag();

        //二つとも直ってフラグがtrueになれば顔を笑顔にしてクリア
        if (GetRepairFlag[0] == true && GetRepairFlag[1] == true)
        {
            BearHeadChange = 1;
            ClearFlag = true;
        }

        //実際に表示するマテリアル
        this.GetComponent<Renderer>().material = material[BearHeadChange];


        if(ClearFlag==true)
        {
            if(SoundFlag==true)
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
}
