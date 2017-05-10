using UnityEngine;
using System.Collections;

public class NeedleDisplay : MonoBehaviour {

    //================================================================
    //針の表示非表示を切り替えるスクリプト
    //================================================================

    // 変数宣言----------------------------------------------------------------------

    //アニメーションを取得
    private Animation anim;
    //タップするオブジェクト
    public GameObject obj;

    // Use this for initialization
    void Start()
    {
        anim = this.gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        //アニメーションの再生が終わったのならば画面外へ移動
        if(!anim.IsPlaying("Take 001"))
        {
            gameObject.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
        }

    }
}
