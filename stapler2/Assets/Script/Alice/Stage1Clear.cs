using UnityEngine;
using System.Collections;

public class Stage1Clear : MonoBehaviour {

    //===============================================================
    //ステージ1・フロア3でのクリア判定をとるスクリプト
    //===============================================================

    // 変数宣言----------------------------------------------------------------------
    
    //タップされたかの判定を持つオブジェクト（エネミー）
    public GameObject Floar3Enemy1;
    public GameObject Floar3Enemy2;

    //タップされたかの判定を受け取る変数
    private bool[] TapFlag = new bool[2];

    //二つのフラグを受けてクリアの判定をする変数
    private bool OllTapFlag;
    //クリアの判定を別のスクリプトへ渡す関数
    //このフラグを渡すことによってステージ2へと画面遷移が行われる
    public bool GetClear()
    {
        return OllTapFlag;
    }

    //アリスがどのステージに達しているかのフラグ(ステージ1)
    //ステージの最初からと操作するために必要
    public static bool AliceStage1Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage1Flag()
    {
        return AliceStage1Flag;
    }

    // Use this for initialization
    void Start () {
        TapFlag[0] = false;
        TapFlag[1] = false;
        OllTapFlag = false;
	}

    // Update is called once per frame
    void Update()
    {
        //フロア3に設置されている片方のエネミーのタップされたかのフラグを受け取る
        GimmickParent tap = Floar3Enemy1.GetComponent<GimmickParent>();
        TapFlag[0] = tap.GetTapFlag();
        //フロア3に設置されている片方のエネミーのタップされたかのフラグを受け取る
        GimmickParent tap2 = Floar3Enemy2.GetComponent<GimmickParent>();
        TapFlag[1] = tap2.GetTapFlag();

        //両方のフラグがtrueになれば両方タップされたと判定がとられ
        //ステージ1はクリアとなる
        if (TapFlag[0] == true && TapFlag[1] == true)
        {
            //アリスがステージ2に移動するので
            //アリスがステージ1にいるというフラグはfalseにする
            AliceStage1Flag = false;

            //このフラグがステージ1をクリアしたかのフラグになっている
            OllTapFlag = true;
        }

    }
}
