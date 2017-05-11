using UnityEngine;
using System.Collections;

public class StoryChange : MonoBehaviour {

    //===============================================================
    //ストーリーを表示しているオブジェクトのマテリアルを切り替えて
    //ストーリーが進んでいる風に見せるスクリプト
    //===============================================================

    // 変数宣言----------------------------------------------------------------------
    //表示するマテリアル
    public Sprite[] sprite;
    //表示するマテリアルを切り替える変数
    private int ChangeStoryNum=0;
    public int GetNum()
    {
        return ChangeStoryNum;
    }

    // Use this for initialization
    void Start () {
        ChangeStoryNum = 0;
	}

    // Update is called once per frame
    void Update()
    {
        //画面をタップした時に処理
        if (Input.GetMouseButtonDown(0))
        {
            if (ChangeStoryNum != 5)
            {
                ChangeStoryNum++;
            }

            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlaySE("paper-take2_01");
            }
        }

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite[ChangeStoryNum];

    }
}
