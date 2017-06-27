using UnityEngine;
using System.Collections;

//===================================================================
//魔女のマテリアルを切り替えるスクリプト
//WitchCollのスクリプト内のWitchCollNormaでマテリアルを切り替える
//===================================================================

public class WitchChange : MonoBehaviour {


    // 変数宣言----------------------------------------------------------------------

    //表示するマテリアル
    public Material[] WitchMaterial;

    public WitchAction WitchCangeNum;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Renderer>().material = WitchMaterial[WitchCangeNum.GetWitchCollNorma()];
    }
}
