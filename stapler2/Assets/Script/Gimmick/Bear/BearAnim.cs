using UnityEngine;
using System.Collections;

//============================================================
//熊の腕が直った時に腕のアニメーションを再生するスクリプト
//============================================================

public class BearAnim : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //腕のアニメーションを取得
    private Animation BearArmAnim;

    public GameObject BearArm;

    private bool ArmRepairFlag;

    // Use this for initialization
    void Start () {
        BearArmAnim = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        BearRepair bearRepair = BearArm.GetComponent<BearRepair>();
        ArmRepairFlag = bearRepair.GetRepairFlag();

        if(ArmRepairFlag==true)
        {
            BearArmAnim.Play();
        }
	}
}
