﻿using UnityEngine;
using System.Collections;

public class WitchChange : MonoBehaviour {

    //===================================================================
    //魔女のマテリアルを切り替えるスクリプト
    //WitchCollのスクリプト内のWitchCollNormaでマテリアルを切り替える
    //===================================================================

    // 変数宣言----------------------------------------------------------------------
    //表示するマテリアル
    public Material[] material;

    public WitchColl num;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Renderer>().material = material[num.GetWitchCollNorma()];
    }
}