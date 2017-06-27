using UnityEngine;
using System.Collections;

public class EnemyAnim_Joint : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    public GimmickJoint gimmickJoint;

    //アニメーションを取得
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (gimmickJoint.GetTapFlag() == true)
        {
            anim.SetTrigger("Stop");
        }
    }
}
