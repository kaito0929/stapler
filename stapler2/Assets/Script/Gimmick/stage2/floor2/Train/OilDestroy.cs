using UnityEngine;
using System.Collections;

public class OilDestroy : MonoBehaviour {

    //======================================================
    //オイルが画面外へ出たらDestroyして消去するスクリプト
    //======================================================

    // 画面外に出た時の処理
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
