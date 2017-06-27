using UnityEngine;
using System.Collections;

//=====================================
//臼の当たり判定を操作するスクリプト
//=====================================

public class MortarColl : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    public GimmickParent gimmickParent;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(gimmickParent.GetTapFlag()==true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

	}
}
