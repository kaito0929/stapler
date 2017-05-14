using UnityEngine;
using System.Collections;

public class TapNotAnswer : MonoBehaviour {

    //========================================================================
    //タップした時に何も挟んでいなかったり、答えとは別の場所をタップした時に
    //針の弾かれるアニメーションを再生する
    //========================================================================

    // 変数宣言----------------------------------------------------------------------

    //NeedleManagerを取得する
    private GameObject NeedleManager;

    // Use this for initialization
    void Start () {
        NeedleManager = GameObject.Find("NeedleManager");
    }
	
	// Update is called once per frame
	void Update () {

        //NeedleAnimation内の関数を使用できるように
        NeedleAnimation needle = NeedleManager.GetComponent<NeedleAnimation>();

        if (TouchManager.SelectedGameObject == gameObject)
        {
            //針のアニメーションを再生
            needle.NeedelAnimPlay();
        }
    }
}
