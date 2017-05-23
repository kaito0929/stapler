using UnityEngine;
using System.Collections;

public class ElectricalConnect : MonoBehaviour {

    //=======================================================================
    //観覧車の電線を繋げて、観覧車のアニメーションを再生させるスクリプト
    //=======================================================================

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

    public GameObject Spark;

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
