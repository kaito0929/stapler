using UnityEngine;
using System.Collections;

public class Stage1_BGM : MonoBehaviour {

    //=============================================================
    //ステージ1のBGM再生用スクリプト
    //=============================================================

    void Awake()
    {
        AudioManager.Instance.PlayBGM("game_maoudamashii_5_town21");
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }
}
