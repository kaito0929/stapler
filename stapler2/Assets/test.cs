using UnityEngine;
using System.Collections;


//================================================
//アリスを右へ動かすスクリプト（テスト）
//=================================================

public class test : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //アリスがギミックに当たったかのフラグを受け取る変数
    private bool GetAliceCollFlag;

    //Rigidbodyを取得
    private Rigidbody rd;

    public bool AliceCameraInFlag;

    // Use this for initialization
    void Start () {
        GetAliceCollFlag = false;
        AliceCameraInFlag = true;

        rd = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        AliceMovePos();
    }

    //アリス移動用の関数
    void AliceMovePos()
    {
        //アリスが敵か何かとぶつかった際のフラグを受け取る
        AliceGameOver coll = gameObject.GetComponent<AliceGameOver>();
        GetAliceCollFlag = coll.GetAliceCollFlag();

        //アリスが何かとぶつかっていなかったら処理を行う
        if (GetAliceCollFlag == false)
        {
            rd.velocity = Vector3.right * 0.5f;
            rd.MovePosition(transform.position + Vector3.right * Time.deltaTime);
        }
        else
        {
            rd.velocity = Vector3.right * 0.0f;
        }
    }



    


}
