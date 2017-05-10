using UnityEngine;
using System.Collections;

public class Story_BGM : MonoBehaviour {

    //======================================================
    //世界観の説明画面のBGM再生用スクリプト
    //======================================================

    void Awake()
    {
        AudioManager.Instance.PlayBGM("mayonakanoomochabako_01");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
