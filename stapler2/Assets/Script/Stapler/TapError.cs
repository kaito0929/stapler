using UnityEngine;
using System.Collections;

public class TapError : MonoBehaviour {

    //========================================================================
    //タップした時に何も挟んでいなかったり、答えとは別の場所をタップした時に
    //針の弾かれるアニメーションを再生する
    //========================================================================

    // 変数宣言----------------------------------------------------------------------

    //NeedleManagerを取得する
    public NeedleAnimation needleAnim;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {


        //針のアニメーションを再生
        needleAnim.NeedelAnimPlay();

    }
}
