using UnityEngine;
using System.Collections;

public class CatAnim : MonoBehaviour {

    //================================================
    //猫のアニメーションを制御するスクリプト
    //================================================

    // 変数宣言----------------------------------------------------------------------

    //再生するAnimatorを取得
    private Animator CatAnimator;

    private AnimatorStateInfo animInfo;

    public GameObject Stage2_Clear_Obj;
    public GameObject black;

    // Use this for initialization
    void Start () {
        CatAnimator = GetComponent<Animator>();
        animInfo = CatAnimator.GetCurrentAnimatorStateInfo(0);
    }
	
	// Update is called once per frame
	void Update () {

        animInfo = CatAnimator.GetCurrentAnimatorStateInfo(0);

        if (TouchManager.SelectedGameObject==gameObject)
        {
            CatAnimator.SetTrigger("Tap");
        }


        if(animInfo.nameHash==Animator.StringToHash("Base Layer.StandUp"))
        {
            //クリアした時にめくられるページを表示させる
            Stage2_Clear_Obj.SetActive(true);
            black.SetActive(true);
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
