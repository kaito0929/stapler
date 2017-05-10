using UnityEngine;
using System.Collections;

public class Stage2_BGM : MonoBehaviour {

    //=============================================================
    //ステージ2のBGM再生用スクリプト
    //=============================================================

    void Awake()
    {
        AudioManager.Instance.PlayBGM("game_maoudamashii_5_town25b");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
