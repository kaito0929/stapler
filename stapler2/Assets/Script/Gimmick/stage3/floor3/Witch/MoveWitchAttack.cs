using UnityEngine;
using System.Collections;

//==========================================================
//魔女の攻撃がアリスに追尾するようにするスクリプト
//==========================================================

public class MoveWitchAttack : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //攻撃が追尾するオブジェクト（アリス）
    private GameObject alice;

    //攻撃の移動スピード
    private float AttackSpeed = 5.0f;
    //攻撃を動かす変数
    private float AttackStep = 0.0f;

    //動いている状態かのフラグ
    private bool AttackMoveFlag;

    //動きが止められてからの時間を加算する変数
    private float AttackStopTime;

    //Ray関係
    //ホッチキスの針を移動させるために必要
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;
    //作られた針を格納する変数
    private GameObject CreateNeedle;


    //アリスか魔女に当たった場合に火球を消しておく
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "witch")
        {
            //作った針も消去
            Destroy(CreateNeedle);
            //攻撃を消す
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        alice = GameObject.Find("alice");
        AttackMoveFlag = true;
        AttackStopTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {

        //フラグがtrueならば攻撃はアリスを追従するようにする
        if (AttackMoveFlag == true)
        {
            AttackStep = Time.deltaTime * AttackSpeed;
            gameObject.transform.position = Vector3.MoveTowards
                (gameObject.transform.position, alice.transform.position, AttackStep);
        }
        else
        {
            AttackStopTime += Time.deltaTime;
        }

        //止められてから時間経過によって攻撃を消す
        if(AttackStopTime >= 3f)
        {
            //針は画面外へ移動
            Destroy(CreateNeedle);
            //火球を消す
            Destroy(this.gameObject);
        }

        AttackStop();
        
    }

    //攻撃を止めるように見せる関数
    void AttackStop()
    {
        if (AttackMoveFlag == true)
        {
            //火球がタップされた場合フラグをfalseにして動きを止めるようにする
            if (TouchManager.SelectedGameObject == gameObject)
            {
                AttackMoveFlag = false;

                //Rayを飛ばして
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    CreateNeedle = (GameObject)Instantiate(Needle, hit.point, Quaternion.identity);
                }
            }
        }
    }

}
