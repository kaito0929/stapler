using UnityEngine;
using System.Collections;

//===============================================
//ホッチキスの音を鳴らすスクリプト
//===============================================


public class StaplerSE : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //何も挟んでいない状態でタップした音
    public AudioClip LightStaplerSound;

    //何かを挟んでいる状態でのタップ音
    public AudioClip HeavyStaplerSound;

    //AudioSourceを取得
    private AudioSource audioSource;

    //何も挟んでいないと判定を取る為のオブジェクト
    public GameObject NotInterposeObj;

    public static int TapNum;
    public static int GetTapNum()
    {
        return TapNum;
    }
    public int num;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        if (Application.loadedLevelName == "stage1")
        {
            TapNum = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        num = TapNum;
        //タップした先のオブジェクトがTapHitならば何も挟んでいない音を再生
        //別のオブジェクトならば複数挟んでいる重い音を再生
        if (TouchManager.SelectedGameObject == NotInterposeObj)
        {
            audioSource.PlayOneShot(LightStaplerSound);
            TapNum++;
        }
        else if (TouchManager.SelectedGameObject != NotInterposeObj&& TouchManager.SelectedGameObject != null)
        {
            audioSource.PlayOneShot(HeavyStaplerSound);
            TapNum++;
        }

    }
}
