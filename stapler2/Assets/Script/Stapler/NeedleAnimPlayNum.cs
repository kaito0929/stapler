using UnityEngine;
using System.Collections;

//============================================================
//弾かれる針のアニメーションを再生する順番を決めるスクリプト
//============================================================

public class NeedleAnimPlayNum : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //アニメーションを再生する順番の変数
    public int AnimPlayNum;

    public int GetAnimPlayNum()
    {
        return AnimPlayNum;
    }

    private int MaxPlayNum;

    // Use this for initialization
    void Start () {
        AnimPlayNum = 0;
        MaxPlayNum = 15;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            AnimPlayNum++;
        }

        //15になると一巡したことになるので数字を0に戻す
        if (AnimPlayNum == MaxPlayNum)
        {
            AnimPlayNum = 0;
        }
    }
}
