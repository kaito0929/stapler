using UnityEngine;
using System.Collections;

public class BoneStop : MonoBehaviour {

    //===============================================================
    //上方向へ投げられた骨を壁にくっつけるスクリプト
    //===============================================================

    // 変数宣言----------------------------------------------------------------------

    //壁の一部分に当たっているかのフラグ
    private bool WallCollFlag;

    //岩をタップしたかのフラグを受け取る変数
    private bool PlaybackFlag;
    //タップされたかのフラグを持つオブジェクト
    public GameObject Rock;

    //上に移動するアニメーションを持つ骨を格納する
    public Animation anim;
    //上記と同じオブジェクトを格納する
    public GameObject bone;

    //骨をタップして壁に止められたかのフラグ
    private bool BoneTapStopFlag;
    //骨をタップして壁に止められたかのフラグを別のスクリプトへ渡す関数
    public bool GetBoneTapStopFlag()
    {
        return BoneTapStopFlag;
    }


    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    //敵やギミックに取り付けるホッチキスの針
    public GameObject Needle;

    void OnTriggerEnter(Collider other)
    {
        //壁のコライダーと衝突した時に処理するように
        if (other.gameObject.name == "wall")
        {
            WallCollFlag = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //壁のコライダーと離れている間はフラグをfalseになるように
        WallCollFlag = false;
    }

    // Use this for initialization
    void Start () {
        WallCollFlag = false;
        BoneTapStopFlag = false;
        anim = bone.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {

        //岩がタップされたかのフラグを取得
        RockTap rocktap = Rock.GetComponent<RockTap>();
        PlaybackFlag = rocktap.GetRockTapFlag();

        //壁の一部分に当たっている場合に処理する
        if (WallCollFlag == true)
        {
            //タップしたものが骨のオブジェクトなら処理
            if (TouchManager.SelectedGameObject == gameObject)
            {
                //アニメーションをストップさせてそこに止められたように見せる
                anim.Stop();

                //止められたのでフラグをtrueにする
                BoneTapStopFlag = true;

                gameObject.transform.parent = null;

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
        else
        {
            //アニメーションの再生は壁に当たっていない状態なら
            //行うようにしておく
            if (PlaybackFlag == true)
            {
                anim.Play();
            }
        }
        

    }
}
