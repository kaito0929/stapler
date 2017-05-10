using UnityEngine;
using System.Collections;

public class BallJoint : MonoBehaviour {

    //==============================================================================
    //ステージ2のボールとエネミーを親子関係にしてくっつけるスクリプト
    //熊と当たっている場合には処理しないようにするのでGimmickParentと一緒にすると
    //ややこしくなりそうなので、分けて処理を行う
    //==============================================================================

    // 変数宣言----------------------------------------------------------------------

    //タップしたかのフラグ
    private bool TapFlag;
    //別のスクリプトに判定を渡す関数
    public bool GetTapFlag()
    {
        return TapFlag;
    }

    //熊と敵がぶつかっていないかのフラグ
    //ぶつかっているとtrueにして親子関係にする処理を行わないようにする
    private bool BearCollFlag;

    //Ray関係
    //ホッチキスの針を移動させるのに使う
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    //オブジェクトが重なっているとフラグをtrueに
    void OnTriggerStay(Collider other)
    {
        //もしも熊とぶつかっていた場合に処理
        if(other.gameObject.name== "kuma_b")
        {
            //フラグをtrueにして親子関係にならないように
            BearCollFlag = true;
        }

        if (BearCollFlag == false)
        {
            //タップしたものがgameObjectだった場合に処理
            if (TouchManager.SelectedGameObject == gameObject)
            {
                //ぶつかっている相手と親子関係になる
                gameObject.transform.parent = other.transform;

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
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "kuma_b")
        {
            //壁のコライダーと離れている間はフラグをfalseになるように
            BearCollFlag = false;
        }
    }

    // Use this for initialization
    void Start () {
        BearCollFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
