using UnityEngine;
using System.Collections;

public class GameOver_BGM : MonoBehaviour {

    //=============================================================
    //ゲームオーバー画面のBGM再生用スクリプト
    //=============================================================

    void Awake()
    {
        AudioManager.Instance.PlayBGM("game_maoudamashii_7_event31");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
