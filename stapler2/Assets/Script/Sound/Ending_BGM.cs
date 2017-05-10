using UnityEngine;
using System.Collections;

public class Ending_BGM : MonoBehaviour {

    //=============================================================
    //エンディング画面のBGM再生用スクリプト
    //=============================================================

    void Awake()
    {
        AudioManager.Instance.PlayBGM("game_maoudamashii_4_field02");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
