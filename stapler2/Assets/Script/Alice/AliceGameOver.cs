using UnityEngine;
using System.Collections;

public class AliceGameOver : MonoBehaviour {

    //==================================================================
    //ゲームオーバー処理を行うスクリプト
    //==================================================================

    // 変数宣言----------------------------------------------------------------------

    //アニメーションの取得
    private Animator anim;
    //アニメーションのステートを取得する
    private AnimatorStateInfo animInfo;

    //ゲームオーバーになったかのフラグ
    private bool GameOverFlag;
    //別のスクリプトへゲームオーバーのフラグを渡す関数
    public bool GetGameOverFlag()
    {
        return GameOverFlag;
    }

    //アリスが何かに当たった時のフラグ
    private bool AliceCollFlag;
    //フラグを渡すための関数
    public bool GetAliceCollFlag()
    {
        return AliceCollFlag;
    }

    //ゲームオーバー画面への遷移用のSceneChangeを持つオブジェクトを取得
    //最初にSetActiveをfalseにしておいて攻撃が当たった場合にtrueにしておいて
    //ゲームオーバー画面への遷移が行える状態にする
    public GameObject GameOver_SceneChange;

    //次のステージへの遷移用のSceneChangeを持つオブジェクトを取得
    //攻撃が当たった時にSetActiveをfalseにしておいて、クリアした場合にtrueにして
    //ゲームオーバー画面への遷移が行える状態にする
    public GameObject NextStage_SceneChange;

    void OnTriggerEnter(Collider other)
    {
        //指定してあるオブジェクトに当たった場合に処理
        //当たったかどうかのフラグを操作してゲームオーバー画面への遷移を行う
        if (other.gameObject.name == "panda" || other.gameObject.name == "apple(Clone)" ||
            other.gameObject.name == "wicth_ball1(Clone)"|| other.gameObject.name == "wicth_ball2(Clone)" ||
            other.gameObject.name == "Thunder(Clone)"|| other.gameObject.name == "rod" || other.gameObject.name == "enemy_s2")
        {
            //やられた時のアニメーションを再生
            anim.SetBool("collision", true);

            AliceCollFlag = true;
        }
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        GameOverFlag = false;
        AliceCollFlag = false;

        GameOver_SceneChange.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        animInfo = anim.GetCurrentAnimatorStateInfo(0);

        //アニメーションがcollStandbyになったら処理
        //CollFlagをtrueにして膝をついてから画面遷移
        if (animInfo.nameHash == Animator.StringToHash("Base Layer.collStandby"))
        {
            //次のステージへの画面遷移を行うためのスクリプトを持ったオブジェクトの
            //SetActiveをfalseにしておく
            NextStage_SceneChange.SetActive(false);

            //ゲームオーバー画面への遷移を行うオブジェクトの
            //SetActiveをtrueにして画面遷移が出来るように
            GameOver_SceneChange.SetActive(true);

            //当たったかどうかのフラグを立ててゲームオーバーへと画面遷移
            GameOverFlag = true;
        }
    }
}
