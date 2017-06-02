using UnityEngine;
using System.Collections;

//==============================================
//ページめくりのシェーダーを操作するスクリプト
//==============================================

public class PageTurn : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //ぺージをめくっているかのフラグ
    private bool PageTurnFlag;

    //ページがめくれているように演出する数値に代入する変数
    private float PageTurnNum;

    //表示されているストーリーの番号
    public static int StoryNum;
    //別のスクリプトへ変数を渡すための関数
    public static int GetNum()
    {
        return StoryNum;
    }

    //Rendererを取得
    private Renderer rend;

    // Use this for initialization
    void Start () {
        PageTurnFlag = false;
        PageTurnNum = 1f;
        StoryNum = 0;
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        //一枚ずつページをめくるため、先頭のStoryBoardから順番に処理する
        if (TouchManager.SelectedGameObject == gameObject)
        {
            //まだページをめくり始めていなかったら処理
            if (PageTurnFlag == false)
            {
                //ページ番号を加算
                StoryNum++;
                //ページのめくる音を再生
                AudioManager.Instance.PlaySE("paper-take2_01");
            }

            //タップされたからページめくりが開始されるので
            //フラグをtrueにしておく
            PageTurnFlag = true;
        }

        //フラグがtrueになったなら、数値を減算させる
        if(PageTurnFlag==true)
        {
            PageTurnNum -= 0.1f;
        }

        //数値が-1f以下になったら処理
        if (PageTurnNum < -1f)
        {
            PageTurnNum = -1f;

            //当たり判定を消して次のページがめくれるようにする
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        //数値を渡してページがめくれているように
        rend.material.SetFloat("_Flip", PageTurnNum);

	}
}
