using UnityEngine;
using System.Collections;

public class Stage3_BGM : MonoBehaviour {

    //=============================================================
    //ステージ3のBGM再生用スクリプト
    //=============================================================

    void Awake()
    {
        AudioManager.Instance.PlayBGM("game_maoudamashii_3_theme14");
    }

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

	}
}
