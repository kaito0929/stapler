using UnityEngine;
using System.Collections;

//==========================================================
//魔女の攻撃がアリスに追尾するようにするスクリプト
//==========================================================

public class MoveFlame : MonoBehaviour {


    // 変数宣言----------------------------------------------------------------------
    //攻撃が追尾するオブジェクト
    private GameObject alice;
    //火球のスピード
    private float speed = 4.0f;
    //動かす変数
    private float step = 0.0f;
    //動いている状態かのフラグ
    private bool FlameMoveFlag;

    //動きが止められてからの時間を加算する変数
    private float FlameStopTime;

    //Ray関係
    //ホッチキスの針を移動させるために必要
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    private GameObject Needle;


    //アリスか魔女に当たった場合に火球を消しておく
    void OnTriggerEnter(Collider other)
    {
        //針との親子関係を解除する
        gameObject.transform.DetachChildren();
        //針は画面外へ移動
        Needle.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
        //火球を消す
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        alice = GameObject.Find("alice");
        Needle = GameObject.Find("StickNeedle1");
        FlameMoveFlag = true;
        FlameStopTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        //フラグがtrueならば火球はアリスを追従するようにする
        if (FlameMoveFlag == true)
        {
            step = Time.deltaTime * speed;
            gameObject.transform.position = Vector3.MoveTowards
                (gameObject.transform.position, alice.transform.position, step);
        }
        else
        {
            FlameStopTime += Time.deltaTime;
        }

        //止められてから時間経過によって火球を消す
        if(FlameStopTime>=3f)
        {
            //針との親子関係を解除する
            gameObject.transform.DetachChildren();
            //針は画面外へ移動
            Needle.transform.position = new Vector3(-11.08f, 0.393f, -0.867f);
            //火球を消す
            Destroy(this.gameObject);
        }

        FleamStop();
        
    }

    void FleamStop()
    {
        //火球がタップされた場合フラグをfalseにして動きを止めるようにする
        if (TouchManager.SelectedGameObject == gameObject)
        {
            FlameMoveFlag = false;

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
    }

}
