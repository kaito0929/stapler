using UnityEngine;
using System.Collections;

public class ThunderStop : MonoBehaviour {

    //==================================================================
    //UFOの弾（雷）をタップして防ぐスクリプト
    //タップしてから時間が経過すると消滅するように
    //==================================================================

    // 変数宣言----------------------------------------------------------------------

    //動きを止めるためにRigidbodyを操作できるようにする
    private Rigidbody rb;

    //オブジェクト（雷）の落下が止められたかのフラグ
    //時間をカウントし始めるタイミングに使う
    private bool ThunderStopFlag;
    //動きが止まってからカウントされるオブジェクト（雷）の消滅の時間
    private float time;

    //Ray関係
    //ホッチキスの針を移動させるために必要
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    private GameObject Needle;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        ThunderStopFlag = false;
        Needle = GameObject.Find("StickNeedle1");
    }
	
	// Update is called once per frame
	void Update () {

        //タップして落下を停止
        if (TouchManager.SelectedGameObject == gameObject)
        {
            rb.isKinematic = true;
            ThunderStopFlag = true;

            //Rayを飛ばして
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                //針の位置をタップした位置へと移動させる。
                Needle.transform.position = hit.point;
                //gameObjectと親子関係に
                Needle.transform.parent = gameObject.transform;
            }
        }

        //ThunderStopFlagがtrueになっていればカウントを開始
        if (ThunderStopFlag == true)
        {
            time += Time.deltaTime;
        }

        //カウントが2fを超えたならばオブジェクトを削除
        if (time >= 2f)
        {
            //針との親子関係を解除する
            gameObject.transform.DetachChildren();
            //針は画面外へ移動
            Needle.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
            //雷を消す
            Destroy(this.gameObject);
        }
    }
}
