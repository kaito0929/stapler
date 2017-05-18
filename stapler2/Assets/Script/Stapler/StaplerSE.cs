using UnityEngine;
using System.Collections;

public class StaplerSE : MonoBehaviour {

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
    void Update()
    {

        //タップした先のオブジェクトがTapHitならば何も挟んでいない音を再生
        //別のオブジェクトならば複数挟んでいる重い音を再生
        if (TouchManager.SelectedGameObject == NotInterposeObj)
        {
            audioSource.PlayOneShot(LightStaplerSound);
        }
        else if (TouchManager.SelectedGameObject != NotInterposeObj&& TouchManager.SelectedGameObject != null)
        {
            audioSource.PlayOneShot(HeavyStaplerSound);
        }

    }
}
