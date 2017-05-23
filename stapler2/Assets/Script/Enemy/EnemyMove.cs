using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

    //===================================================================
    //エネミーが音に気付いて移動するスクリプト
    //音に気付くアニメーションも再生させる
    //===================================================================

    // 変数宣言----------------------------------------------------------------------

    //移動スピード
    private float speed = 2;
    //オブジェクトの初期位置
    private Vector3 vec;
    //タップしたかのフラグを受け取る変数
    bool NotTapFlag;

    //アニメーションを取得
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        NotTapFlag = false;
        vec = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //移動させたいオブジェクトのフラグを受け取る
        GimmickParent gimmick = gameObject.GetComponent<GimmickParent>();
        NotTapFlag = gimmick.GetTapFlag();

        //タップした時にタップしたオブジェクトがgameObjectじゃなかったら
        //タップした位置の座標を取得
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Sound");
            if (TouchManager.SelectedGameObject != gameObject)
            {
                vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        //フラグがfalseならば移動する
        if (NotTapFlag == false)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                   new Vector3(vec.x, gameObject.transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

    }
}
