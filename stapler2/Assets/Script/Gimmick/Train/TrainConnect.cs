using UnityEngine;
using System.Collections;

//=========================================================================
//列車と貨物を親子関係にするスクリプト
//=========================================================================

public class TrainConnect : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //列車と貨物が親子関係になったかのフラグ
    private bool TrainConnectFlag;
    //他のスクリプトへ変数を渡す関数
    public bool GetConnectFlag()
    {
        return TrainConnectFlag;
    }

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;
    //親子関係になる列車の先頭部分
    public GameObject ParentTrain;

    //パーティクルの色を変化させるための変数
    public Renderer rd;

    // Use this for initialization
    void Start () {
        TrainConnectFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

        //フラグがfalseの場合に処理する
	    if(TrainConnectFlag==false)
        {
            if(TouchManager.SelectedGameObject==gameObject)
            {
                //フラグをtrueにしておく
                TrainConnectFlag = true;

                rd.GetComponent<Renderer>().material.SetColor("_Color", new Color(255, 0, 255));

                //先頭車両を親にする
                gameObject.transform.parent = ParentTrain.transform;

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
