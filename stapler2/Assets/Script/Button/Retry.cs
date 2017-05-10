using UnityEngine;
using System.Collections;

public class Retry : MonoBehaviour {

    //======================================================================
    //進行途中のステージの最初に戻る為のスクリプト
    //ボタンを押して戻るようになっている
    //======================================================================

    // 変数宣言----------------------------------------------------------------------

    //ボタンがタップされてステージの最初に戻るかのフラグ
    //この変数がtrueになれば画面遷移を行う
    private bool RetryButtonClickFlag = false;
    //タップされたかのフラグを別のスクリプトへ渡す関数
    public bool GetRetryFlag()
    {
        return RetryButtonClickFlag;
    }

    //どのステージが進行中かのフラグ
    private bool[] EnrouteStageFlag=new bool[3];

    //それぞれのステージへの画面遷移を行うスクリプトを持ったオブジェクト
    //ステージ1への遷移を行うオブジェクト
    public GameObject Stage1_SceneChange;
    //ステージ2への遷移を行うオブジェクト
    public GameObject Stage2_SceneChange;
    //ステージ3への遷移を行うオブジェクト
    public GameObject Stage3_SceneChange;

    //ボタンが押された時に処理する
    public void OnClick()
    {
        //ボタンを押した時の効果音を再生
        AudioManager.Instance.PlaySE("paper-take2_01");

        //進行中のステージによってSetActiveを切り替えるオブジェクトを変える
        //ステージ1を進行中ならば処理
        if (EnrouteStageFlag[0] == true)
        {
            //ステージ1の画面遷移が行わるようにSetActiveをtrueに
            //以下も同じように処理
            Stage1_SceneChange.SetActive(true);
        }
        //ステージ2を進行中ならば処理
        else if (EnrouteStageFlag[1] == true)
        {
            Stage2_SceneChange.SetActive(true);
        }
        //ステージ3を進行中ならば処理
        else if (EnrouteStageFlag[2] == true)
        {
            Stage3_SceneChange.SetActive(true);
        }
        RetryButtonClickFlag = true;
    }

	// Use this for initialization
	void Start () {
        //それぞれのステージにあるスクリプトからフラグを受け取る
        EnrouteStageFlag[0] = Stage1Clear.GetAliceStage1Flag();
        EnrouteStageFlag[1] = ufoMove.GetAliceStage2Flag();
        EnrouteStageFlag[2] = WitchColl.GetAliceStage3Flag();

        //画面遷移を行うスクリプトを持ったオブジェクトは
        //最初にSetActiveをfalseにしておく
        Stage1_SceneChange.SetActive(false);
        Stage2_SceneChange.SetActive(false);
        Stage3_SceneChange.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
