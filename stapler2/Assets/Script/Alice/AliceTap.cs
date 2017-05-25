using UnityEngine;
using System.Collections;

public class AliceTap : MonoBehaviour {

    //============================================================
    //アリスをタップした時にアニメーションを再生させるスクリプト
    //============================================================

    // 変数宣言----------------------------------------------------------------------

    //Animatorを取得
    private Animator AliceAnim;

    // Use this for initialization
    void Start () {
        AliceAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (TouchManager.SelectedGameObject == gameObject)
        {
            AliceAnim.SetTrigger("Tap");
        }

	}
}
