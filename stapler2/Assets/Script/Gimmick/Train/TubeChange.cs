using UnityEngine;
using System.Collections;

//==============================================================
//列車の動力チューブを直すスクリプト
//==============================================================

public class TubeChange : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //チューブが直ったかのフラグ
    private bool TubeRepairFlag;
    //フラグを別のスクリプトへ渡す関数
    public bool GetTubeRepairFlag()
    {
        return TubeRepairFlag;
    }

    //Ray関係
    //ホッチキスの針を移動させるために使う
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //パーティクルの色を変化させるための変数
    public Renderer rd;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (TubeRepairFlag == false)
        {
            //タップされたら変数の数値を1にして表示するマテリアルを切り替える
            if (TouchManager.SelectedGameObject == gameObject)
            {
                TubeRepairFlag = true;

                rd.GetComponent<Renderer>().material.SetColor("_Color", new Color(255, 0, 255));

                //チューブを直すことで列車が動くので列車の動く音を再生
                AudioManager.Instance.PlaySE("locomotive-pass1_01");

                //Rayを飛ばして
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    //針の位置をタップした位置へと移動させる。
                    Needle.transform.position = hit.point;
                    //gameObjectと親子関係に
                    Needle.transform.parent = gameObject.transform;
                }
            }
        }
	}
}
