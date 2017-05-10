using UnityEngine;
using System.Collections;

public class TubeChange : MonoBehaviour {

    //==============================================================
    //列車の動力チューブを直すスクリプト
    //==============================================================

    // 変数宣言----------------------------------------------------------------------

    //チューブのマテリアルを取得
    public Material[] material;
    //表示するマテリアルを切り替える変数
    private int TubeChangeNum;

    //チューブが直ったかのフラグ
    private bool TubeRepairFlag;
    //フラグを別のスクリプトへ渡す関数
    public bool GetTubeRepairFlag()
    {
        return TubeRepairFlag;
    }

    // Use this for initialization
    void Start () {
        TubeChangeNum = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (TubeRepairFlag == false)
        {
            //タップされたら変数の数値を1にして表示するマテリアルを切り替える
            if (TouchManager.SelectedGameObject == gameObject)
            {
                TubeChangeNum = 1;
                TubeRepairFlag = true;

                //チューブを直すことで列車が動くので列車の動く音を再生
                AudioManager.Instance.PlaySE("locomotive-pass1_01");
            }
        }

        this.GetComponent<Renderer>().material = material[TubeChangeNum];
	}
}
