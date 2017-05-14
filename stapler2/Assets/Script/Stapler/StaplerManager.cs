using UnityEngine;
using System.Collections;

public class StaplerManager : MonoBehaviour {

    //===============================================================
    //ホッチキスの表示非表示を切り替えて
    //タップした位置へ移動させるスクリプト
    //表示するスプライトも変えて挟んでいるように見せる
    //背景や別のオブジェクトにつけて使う
    //================================================================

    // 変数宣言----------------------------------------------------------------------

    //ホッチキス
    public GameObject Stapler;

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //表示する時間
    private float DisplayTime = 1f;
    //時間を加算する変数
    private float time = 0f;

    //時間を加算するかのフラグ
    private bool CountFlag;

    //タップした場所にホッチキスの先端部分へ移動させるための変数
    Vector3 pos;

    //表示するスプライト
    public Sprite[] sprite;
    //表示するスプライトを切り替える変数
    private int ChangeStaplerNum;
    //二つ目のスプライトに変化する時間
    private float ChangeTime;

    // Use this for initialization
    void Start () {
        //ホッチキスの表示を消す
        Stapler.SetActive(false);
        time = 0;
        CountFlag = false;
        ChangeTime = 0.2f;
        ChangeStaplerNum = 0;
    }
	
	// Update is called once per frame
	void Update () {


        //画面をタップした時に処理
        if (Input.GetMouseButtonDown(0))
        {
            //ホッチキスを表示させる
            Stapler.SetActive(true);
            //時間を加算するフラグをtrueに
            CountFlag = true;
            //変数を初期化
            time = 0f;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //ホッチキスの場所をタップした位置へ移動
                Stapler.transform.position = hit.point;
                //ホッチキスのx座標をずらしてタップした位置へ先端部分が来るようにする
                pos = Stapler.transform.position;
                pos.x += 0.5f;
                //改めて場所を代入
                Stapler.transform.position = pos;
            }
        }

        StaplerSpriteChange();

    }

    //ホッチキスのスプライトを切り替えて動かしている風に見せる関数
    void StaplerSpriteChange()
    {
        //マテリアルを切り替える秒数
        if (time > ChangeTime)
        {
            ChangeStaplerNum = 1;
        }
        else
        {
            ChangeStaplerNum = 0;
        }

        //フラグがtrueの間に時間を加算
        if (CountFlag == true)
        {
            time += Time.deltaTime;
        }

        //加算した時間が設定した制限を超えたら処理
        if (time > DisplayTime)
        {
            //ホッチキスの表示を消す
            Stapler.SetActive(false);
            //フラグをfalse
            CountFlag = false;
            //変数を初期化
            time = 0f;
            //ホッチキスの表示を初期に
            ChangeStaplerNum = 0;
        }

        //ホッチキスの表示するスプライト
        SpriteRenderer sr = Stapler.GetComponent<SpriteRenderer>();
        sr.sprite = sprite[ChangeStaplerNum];
    }

}
