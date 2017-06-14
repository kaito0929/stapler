using UnityEngine;
using System.Collections;

//=======================================================
//タイトル画面への遷移を行うスクリプト
//=======================================================

public class Title : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //タイトル画面への遷移を行うスクリプトを持つオブジェクト
    public GameObject Title_SceneChange;

    //ボタンをタップしたかのフラグ
    //このフラグがtrueになることによって画面遷移が開始される
    private bool TitleButtonClickFlag;
    //SceneChangeスクリプトへフラグを渡す関数
    public bool GetTitleFlag()
    {
        return TitleButtonClickFlag;
    }

    //タイトルへの遷移を行うボタンを押した時に処理
    public void OnClick()
    {
        //ボタンを押した時の効果音を再生
        AudioManager.Instance.PlaySE("paper-take2_01");
        //タイトル画面への遷移が行われるようにSetActiveをtrueにする
        Title_SceneChange.SetActive(true);
        //フラグをtrueにして画面遷移が行われるように
        TitleButtonClickFlag = true;

        Stage1Clear.AliceStage1Flag=false;
        ufoAction.AliceStage2Flag=false;
        AliceMove_Stage3.AliceStage3Flag=false;
    }

    // Use this for initialization
    void Start () {
        Title_SceneChange.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
