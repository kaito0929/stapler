using UnityEngine;
using System.Collections;

//============================================================================
//チューブが繋がれたかのフラグを受け取って列車を発射させるスクリプト
//失敗した時にゲームオーバーの判定を取る
//============================================================================

public class TrainMove : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //TrainConnectから親子関係になったかのフラグを受け取る変数
    private bool[] GetTrainConnectFlag = new bool[3];

    //実際に繋がる貨物のオブジェクトを格納する変数
    public GameObject[] ConnectTrain;

    //貨物のアニメーションを取得
    public Animation[] anim;

    //列車を動かすベクトル
    private Vector3 pos;

    //チューブが繋がったかのフラグを受け取る変数
    private bool TubeRepairFlag;
    //燃料を補給しているチューブのオブジェクト
    public GameObject tube;

    //全ての貨物と親子関係になっているとtrueになるフラグ
    private bool OllConnectFlag;
    //全ての貨物が繋がった状態でチューブが直った時にtrueになるフラグ
    private bool ClearFlag;
    public bool GetClearFlag()
    {
        return ClearFlag;
    }

    //間違って全部の車両を繋げずにチューブを直した場合にゲームオーバーにする
    private bool ConnectErrorFlag;
    //SceneChangeにフラグを渡すための関数
    public bool GetConnectErrorFlag()
    {
        return ConnectErrorFlag;
    }

    //アリスのアニメーションを取得
    public Animator AliceAnim;

    // Use this for initialization
    void Start () {
        GetTrainConnectFlag[0] = false;
        GetTrainConnectFlag[1] = false;
        GetTrainConnectFlag[2] = false;

        TubeRepairFlag = false;
        ClearFlag = false;
        ConnectErrorFlag = false;

    }
	
	// Update is called once per frame
	void Update () {

        //==■貨物が列車と親子関係になったかのフラグを受け取る■===========================================
        TrainConnect train1 = ConnectTrain[0].GetComponent<TrainConnect>();
        GetTrainConnectFlag[0] = train1.GetConnectFlag();

        TrainConnect train2 = ConnectTrain[1].GetComponent<TrainConnect>();
        GetTrainConnectFlag[1] = train2.GetConnectFlag();

        TrainConnect train3 = ConnectTrain[2].GetComponent<TrainConnect>();
        GetTrainConnectFlag[2] = train3.GetConnectFlag();
        //=================================================================================================

        //ClearFlagがfalseの時にだけフラグを切り替える
        if (ClearFlag == false)
        {
            //全ての貨物が親子関係になっていればOllConnectFlagはtrueに
            if (GetTrainConnectFlag[0] == true&& GetTrainConnectFlag[1] == true&& GetTrainConnectFlag[2] == true)
            {
                OllConnectFlag = true;
            }
        }

        //チューブを直す関数
        TubeRepair();

        //列車を繋げないでチューブを直した場合に処理
        if(ConnectErrorFlag==true)
        {
            //アリスのやられモーションを再生する
            AliceAnim.SetBool("collision", true);
        }
       
    }


    //チューブを直して、クリアやゲームオーバーとする関数
    void TubeRepair()
    {
        //チューブが直されたかのフラグを受け取る
        TubeChange tubeflag = tube.GetComponent<TubeChange>();
        TubeRepairFlag = tubeflag.GetTubeRepairFlag();

        //チューブが直ったなら列車は発車
        if (TubeRepairFlag == true)
        {
            pos = gameObject.transform.position;
            pos.x -= 0.1f;
            gameObject.transform.position = pos;
        }

        //全ての貨物が先頭と繋がったうえでチューブが直ったらクリアとする
        if (OllConnectFlag == true && TubeRepairFlag == true)
        {
            ClearFlag = true;

            //列車のアニメーションを再生
            for (int i = 0; i < 4; i++)
            {
                anim[i].Play();
            }
        }
        else if (OllConnectFlag == false && TubeRepairFlag == true)
        {
            //繋げずに発進してしまったのでフラグをtrueに
            ConnectErrorFlag = true;
        }
    }

}
