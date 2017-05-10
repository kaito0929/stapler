using UnityEngine;
using System.Collections;

public class ThunderDestroy : MonoBehaviour {

    //==========================================================================
    //UFOの弾（雷）が猫に当たった時に消えるようにするスクリプト
    //==========================================================================

    //猫と衝突した場合に弾をDestroyで消しておく
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "toy_cat_1"|| other.gameObject.name == "alice")
        {
            Destroy(gameObject);
        }
    }

    // 画面外に出た時の処理
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
}
