using UnityEngine;
using System.Collections;

public class Title_BGM : MonoBehaviour {

    //===============================================================
    //タイトルBGM再生用のスクリプト
    //タップした時に効果音が鳴るようにもしておく
    //===============================================================

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        AudioManager.Instance.PlayBGM("otenbahime_01");

        if(Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySE("paper-take2_01");
        }
    }
}
