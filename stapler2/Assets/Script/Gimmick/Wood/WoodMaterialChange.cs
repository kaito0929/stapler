using UnityEngine;
using System.Collections;

public class WoodMaterialChange : MonoBehaviour {

    //表示するマテリアル
    public Material[] WoodMaterial;
    private int WoodChangeNum = 0;

    public WoodAttack woodAttack;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (woodAttack.StopFlag() == true)
        {
            WoodChangeNum = 1;
        }

        this.GetComponent<Renderer>().material = WoodMaterial[WoodChangeNum];
    }
}
