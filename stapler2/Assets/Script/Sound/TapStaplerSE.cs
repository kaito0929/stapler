using UnityEngine;
using System.Collections;

public class TapStaplerSE : MonoBehaviour {

    //===============================================
    //ホッチキスの音を鳴らすスクリプト
    //===============================================

    // 変数宣言----------------------------------------------------------------------
    
    //何も挟んでいない状態でタップした音
    public AudioClip LightStaplerSound;

    //何かを挟んでいる状態でのタップ音
    public AudioClip HeavyStaplerSound;

    //AudioSourceを取得
    private AudioSource audioSource;

    //何も挟んでいないと判定を取る為のオブジェクト
    public GameObject NotInterposeObj;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //画面をタップした時に処理
        if (Input.GetMouseButtonDown(0))
        {
            if (TouchManager.SelectedGameObject == NotInterposeObj)
            {
                audioSource.PlayOneShot(LightStaplerSound);
            }
            else
            {
                audioSource.PlayOneShot(HeavyStaplerSound);
            }
        }
    }
}
