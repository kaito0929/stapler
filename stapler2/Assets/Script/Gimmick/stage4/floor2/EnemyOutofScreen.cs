using UnityEngine;
using System.Collections;

//====================================================
//ステージ4フロア2の敵が画面外へ行った時のスクリプト
//====================================================

public class EnemyOutofScreen : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    public bool flag;
    public bool GetFlag()
    {
        return flag;
    }

    // 画面外に出た時の処理
    void OnBecameInvisible()
    {
        flag = true;
    }

    // Use this for initialization
    void Start () {
        flag = false;
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
