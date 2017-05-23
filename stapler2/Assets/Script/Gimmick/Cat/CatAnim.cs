using UnityEngine;
using System.Collections;

public class CatAnim : MonoBehaviour {

    //================================================
    //猫のアニメーションを制御するスクリプト
    //================================================

    // 変数宣言----------------------------------------------------------------------

    //再生するAnimatorを取得
    private Animator CatAnimator;

    // Use this for initialization
    void Start () {
        CatAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
        if(TouchManager.SelectedGameObject==gameObject)
        {
            CatAnimator.SetTrigger("Tap");
        }

	}

    //猫が雷に当たった時に処理
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Thunder(Clone)")
        {
            //アニメーションを再生
            CatAnimator.SetBool("Hit", true);
            //猫の鳴き声の効果音を再生
            AudioManager.Instance.PlaySE("cat2_01");
        }
    }

}
