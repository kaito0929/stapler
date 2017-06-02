using UnityEngine;
using System.Collections;

//==========================================================
//ステージをクリアした時にページめくりを行うスクリプト
//画面遷移も出来るようにする
//==========================================================

public class Stage_Clear_NextPage : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //ページがめくれているように演出する数値に代入する変数
    private float PageTurnNum;

    public float GetTurnNum()
    {
        return PageTurnNum;
    }

    //Rendererを取得
    private Renderer rend;

    // Use this for initialization
    void Start () {
        PageTurnNum = 1f;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //数値が-1f以下になったら処理
        if (PageTurnNum < -1f)
        {
            PageTurnNum = -1f;
        }
        else
        {
            PageTurnNum -= 0.01f;
        }

        //数値を渡してページがめくれているように
        rend.material.SetFloat("_Flip", PageTurnNum);

    }
}
