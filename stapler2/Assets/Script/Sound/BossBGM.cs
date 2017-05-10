using UnityEngine;
using System.Collections;

public class BossBGM : MonoBehaviour {

    //===================================================================
    //ステージ3のフロア3に到達した際にBGMを切り替えるスクリプト
    //===================================================================

    // 変数宣言----------------------------------------------------------------------

    //フロア3に到達した際に魔女戦用のBGMを再生させるフラグ
    public bool BossBGMPlayFlag;
    //フロア3に到達したかのフラグを持っているオブジェクト
    public GameObject Alice;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //魔女戦BGM再生のフラグを取得する
        AliceMove_Stage3 alice = Alice.GetComponent<AliceMove_Stage3>();
        BossBGMPlayFlag = alice.GetReachingFlag();

        //フラグがtrueならばBGMを切り替える
        if (BossBGMPlayFlag == true)
        {
            AudioManager.Instance.PlayBGM("game_maoudamashii_5_town11_01");
        }
    }
}
