using UnityEngine;
using System.Collections;

public class NeedleAnimPlayNum : MonoBehaviour {

    //============================================================
    //弾かれる針のアニメーションを再生する順番を決めるスクリプト
    //============================================================

    // 変数宣言----------------------------------------------------------------------

    //アニメーションを再生する順番の変数
    private int AnimPlayNum;

    public int GetAnimPlayNum()
    {
        return AnimPlayNum;
    }

    private int MaxPlayNum;

    // Use this for initialization
    void Start () {
        MaxPlayNum = 15;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            AnimPlayNum++;
        }

        //9になると一巡したことになるので数字を0に戻す
        if (AnimPlayNum == MaxPlayNum)
        {
            AnimPlayNum = 0;
        }
    }
}
