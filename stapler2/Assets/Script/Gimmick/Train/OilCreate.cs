using UnityEngine;
using System.Collections;

public class OilCreate : MonoBehaviour {

    //====================================================
    //チューブから垂れているオイルを生成するスクリプト
    //====================================================

    // 変数宣言----------------------------------------------------------------------

    private Vector3 OilPos;

    // Use this for initialization
    void Start () {
        OilPos = new Vector3(10.86f, 1.9f, -1.132f);
	}
	
	// Update is called once per frame
	void Update () {
        //OilPos = gameObject.transform.position;
	}

    // 画面外に出た時の処理
    void OnBecameInvisible()
    {
        Instantiate(gameObject, OilPos, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
