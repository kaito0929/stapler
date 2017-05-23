using UnityEngine;
using System.Collections;

public class OilCreate : MonoBehaviour {

    //====================================================
    //チューブから垂れているオイルを生成するスクリプト
    //====================================================

    // 変数宣言----------------------------------------------------------------------

    //生成するオブジェクト
    public GameObject oil;

    //オイルを精製する場所
    private Vector3 OilPos;

    //deltaTimeで加算し続けて指定した数値に達したら
    //オブジェクトを生成するように
    private float ElapsedTime;

    //生成するタイミング
    private float CreateTime;

    //チューブが直ったかのフラグを取る
    public TubeChange tubeFlag;
    //アリスが移動したかのフラグを取る
    public AliceMove_Stage2 moveFlag;

    // Use this for initialization
    void Start () {
        OilPos = new Vector3(10.86f, 1.9f, -1.132f);
        ElapsedTime = 0;
        CreateTime = 2f;
	}
	
	// Update is called once per frame
	void Update () {

        if (tubeFlag.GetTubeRepairFlag() == false && moveFlag.GetFloor2MoveEndFlag() == true)
        {
            //時間を加算
            ElapsedTime += Time.deltaTime;
        }

        //指定した時間を超えたら処理
        if(ElapsedTime >= CreateTime)
        {
            Instantiate(oil, OilPos, Quaternion.identity);
            ElapsedTime = 0;
        }
    }

}
