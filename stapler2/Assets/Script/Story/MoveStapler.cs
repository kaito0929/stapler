using UnityEngine;
using System.Collections;

//==========================================
//妖精ステープラーを下から上へ移動させる
//==========================================

public class MoveStapler : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    //Rigidbodyを取得
    private Rigidbody rd;

    //めくられているページ数
    private int pageNum;

    //セリフの吹き出しのオブジェクト
    public GameObject serif;

    private bool SceneChangeFlag;
    public bool GetSceneChangeFlag()
    {
        return SceneChangeFlag;
    }

    // Use this for initialization
    void Start () {
        rd = GetComponent<Rigidbody>();
        SceneChangeFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

        pageNum = PageTurn.GetNum();
        if (pageNum == 4)
        {
            if (gameObject.transform.position.y < 1.18f)
            {
                rd.velocity = Vector3.up * 0.5f;
                rd.MovePosition(transform.position + Vector3.up * Time.deltaTime);
            }
            else
            {
                rd.velocity = Vector3.up * 0;
                serif.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    SceneChangeFlag = true;
                }
            }
        }

	}
}
