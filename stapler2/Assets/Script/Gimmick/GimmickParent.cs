using UnityEngine;
using System.Collections;

public class GimmickParent : MonoBehaviour {

    //==========================================================
    //オブジェクト同士を親子関係にしてくっつけるスクリプト
    //ステージ2のフロア1の熊とぶつかっていた場合は
    //処理を行わないようにしておく
    //==========================================================

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

    //NeedleManagerを取得する
    private GameObject NeedleManager;

    //何かとぶつかっているかのフラグ
    private bool CollFlag;

    // Use this for initialization
    void Start()
    {
        BearCollFlag = false;
        TapFlag = false;
        NeedleManager = GameObject.Find("NeedleManager");
        CollFlag = false;
    }

    // Update is called once per frame
    void Update()
    {

        //NeedleAnimation内の関数を使用できるように
        NeedleAnimation needle;
        needle = NeedleManager.GetComponent<NeedleAnimation>();

        if (TouchManager.SelectedGameObject == gameObject)
        {
            if (CollFlag == false)
            {
                //針のアニメーションを再生
                needle.NeedelAnimPlay();
            }
        }
    }


    
    void OnTriggerStay(Collider other)
    {
        //もしも熊とぶつかっていた場合に処理
        if (other.gameObject.name == "kuma_b")
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

        CollFlag = true;
    }




    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "kuma_b")
        {
            //壁のコライダーと離れている間はフラグをfalseになるように
            BearCollFlag = false;
        }

        CollFlag = false;
    }
}
