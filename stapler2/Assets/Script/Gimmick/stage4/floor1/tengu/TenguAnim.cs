using UnityEngine;
using System.Collections;

//=============================================
//天狗のアニメーションを再生するスクリプト
//=============================================

public class TenguAnim : MonoBehaviour
{

    // 変数宣言----------------------------------------------------------------------

    //Animatorを取得
    private Animator TenguAnimator;
    private AnimatorStateInfo animInfo;

    //岩がタップされたフラグを受け取る
    public RockTap rockTap;

    //風のパーティクル
    public GameObject Wind;
    //天狗が攻撃したかのフラグ
    private bool TenguAttackFlag;
    public bool GetAttackFlag()
    {
        return TenguAttackFlag;
    }

    // Use this for initialization
    void Start()
    {
        TenguAnimator = GetComponent<Animator>();
        animInfo = TenguAnimator.GetCurrentAnimatorStateInfo(0);
        TenguAttackFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        animInfo = TenguAnimator.GetCurrentAnimatorStateInfo(0);

        //岩がタップされたフラグを受け取って
        //天狗の一連のアニメーションを再生
        if (rockTap.GetRockTapFlag() == true)
        {
            TenguAnimator.SetTrigger("RockTap");
        }

        //天狗をタップした際のアニメーションを再生
        if (TouchManager.SelectedGameObject == gameObject)
        {
            TenguAnimator.SetTrigger("Tap");
        }

        //天狗の攻撃アニメーションが再生されてから
        //風が起こっているように見せるパーティクルを表示するように
        if (animInfo.nameHash == Animator.StringToHash("Base Layer.atack"))
        {
            if (animInfo.normalizedTime >= 0.5f)
            {
                Wind.SetActive(true);
                TenguAttackFlag = true;
            }
        }

    }
}
