using UnityEngine;
using System.Collections;

public class EnemyAnim_Parent : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    public GimmickParent gimmickParent;

    //アニメーションを取得
    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update () {
	
        if(gimmickParent.GetTapFlag()==true)
        {
            anim.SetTrigger("Stop");
        }

	}
}
