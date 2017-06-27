using UnityEngine;
using System.Collections;

//==============================================
//天狗の起こした風で岩を吹き飛ばすスクリプト
//==============================================

public class RockBlowOff : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    public TenguAnim tenguAnim;

    private Vector3 pos;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(tenguAnim.GetAttackFlag()==true)
        {
            pos = gameObject.transform.position;

            pos.x++;
            pos.y++;

            //くるくると回転する
            gameObject.transform.Rotate(new Vector3(0, 0, 5));

            gameObject.transform.position = pos;
        }
	
	}
}
