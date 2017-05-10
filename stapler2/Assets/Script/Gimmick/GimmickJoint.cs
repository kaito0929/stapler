using UnityEngine;
using System.Collections;

public class GimmickJoint : MonoBehaviour
{

    //==========================================================
    //オブジェクト同士をくっつけるスクリプト
    //親子関係にしない場合で、Joint機能を使用する
    //==========================================================

    // 変数宣言----------------------------------------------------------------------
    //タップしたかのフラグ
    public bool TapFlag;

    //別のスクリプトに判定を渡す関数
    public bool GetTapFlag()
    {
        return TapFlag;
    }

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //ギミックと敵をくっつけるFixedJoint
    FixedJoint fx;

    //Box001とオブジェクトが重なっているとフラグをtrueに
    void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.name == "sirusii")
        //{
            //タップしたものがgameObjectだった場合に処理
            if (TouchManager.SelectedGameObject == gameObject)
            {
                //gameObjecutをギミックへとくっつける
                fx = GetComponent<FixedJoint>();
                fx.connectedBody = other.GetComponent<Rigidbody>();
                //TapFlagをtrueに
                TapFlag = true;

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
        //}
    }



    // Use this for initialization
    void Start()
    {
        TapFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
