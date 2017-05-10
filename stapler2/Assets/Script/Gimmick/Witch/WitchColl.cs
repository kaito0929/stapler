using UnityEngine;
using System.Collections;

public class WitchColl : MonoBehaviour
{

    //=============================================================
    //魔女が火球に当たった時の処理を行うスクリプト
    //=============================================================

    // 変数宣言----------------------------------------------------------------------

    //魔女に火球を当てる回数(三回)
    private int WitchCollNorma;
    //残りの火球を当てる回数
    public int GetWitchCollNorma()
    {
        return WitchCollNorma;
    }

    //魔女が倒されたかどうかのフラグ
    private bool WitchDestroyFlag;
    //魔女を倒したフラグを他のスクリプトに渡す関数
    public bool GetWitchDestroy()
    {
        return WitchDestroyFlag;
    }

    //アリスがどのステージに達しているかのフラグ(ステージ3)
    //ステージの最初からと操作するために必要
    public static bool AliceStage3Flag = true;
    //そのフラグを渡すための関数
    //SceneChangeスクリプトに渡す
    public static bool GetAliceStage3Flag()
    {
        return AliceStage3Flag;
    }

    public Animator anim;

    // Use this for initialization
    void Start()
    {
        WitchCollNorma = 3;
        WitchDestroyFlag = false;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //指定した回数分火球にぶつけることが出来たなら
        //魔女をDestoryで消す
        if (WitchCollNorma == 0)
        {
            //アリスがステージ2に移動するので
            //アリスがステージ1にいるというフラグはfalseにする
            AliceStage3Flag = false;
            //魔女は消滅
            Destroy(this.gameObject);
            //魔女が倒されたことによってフラグがtrueになり
            //エンディング画面へと遷移する
            WitchDestroyFlag = true;
        }
    }

    //魔女に火球が当たるたびに当てるノルマを減らしていく
    void OnTriggerEnter(Collider other)
    {
        WitchCollNorma--;

        anim.SetTrigger("Damage");
    }
}
