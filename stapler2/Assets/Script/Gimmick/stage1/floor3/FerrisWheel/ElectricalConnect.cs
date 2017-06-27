using UnityEngine;
using System.Collections;

//=======================================================================
//観覧車の電線を繋げて、観覧車のアニメーションを再生させるスクリプト
//=======================================================================

public class ElectricalConnect : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //再生するアニメーションを持つ観覧車
    public GameObject FerrisWheel;

    //Ray関係
    //ホッチキスの針を移動させるために必要
    private RaycastHit hit;
    private Ray ray;

    //電線が繋がったかのフラグ
    private bool ConnectFlag;
    //フラグを渡すための関数
    public bool GetConnectFlag()
    {
        return ConnectFlag;
    }

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //火花のエフェクト
    public GameObject Spark;


    //パーティクルの色を変化させるための変数
    public Renderer rd;

    // Use this for initialization
    void Start () {
        ConnectFlag = false;
    }
	
	// Update is called once per frame
	void Update () {

        //Animationを取得
        Animation anim = FerrisWheel.GetComponent<Animation>();

        if (ConnectFlag == false)
        {
            if (TouchManager.SelectedGameObject == gameObject)
            {
                anim.Play();
                ConnectFlag = true;
                Spark.SetActive(false);

                rd.GetComponent<Renderer>().material.color = new Color(255.0f / 255.0f, 143.0f / 255.0f, 247.0f / 255.0f);

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
            else if (TouchManager.SelectedGameObject != gameObject)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //パーティクルの色を変える
                    rd.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
                }
            }
        }
	    
	}
}
