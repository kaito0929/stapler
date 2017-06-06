using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//=========================================================
//リザルトで何本針を使ったかの数を表示する
//=========================================================

public class ResultNeedleNum : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //表示するテキスト
    public Text ScoreText;
    //実際に表示する数字
    private int Score;

    // Use this for initialization
    void Start () {
        Score = StaplerSE.GetTapNum();

    }
	
	// Update is called once per frame
	void Update () {

        //数値の桁数によって空白を変える
        if (Score < 10)
        {
            ScoreText.text = "　　" + Score;
        }
        if (Score >= 10 && Score < 100)
        {
            ScoreText.text = "　" + Score;
        }
        if (Score >= 100 && Score < 1000)
        {
            ScoreText.text = "　" + Score;
        }
        if (Score >= 1000 && Score < 10000)
        {
            ScoreText.text = " " + Score;
        }
        if (Score >= 10000)
        {
            ScoreText.text = "" + Score;
        }

    }
}
