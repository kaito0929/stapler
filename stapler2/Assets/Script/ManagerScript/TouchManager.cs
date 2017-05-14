using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    //====================================================================================
    //タップした場所にRayを飛ばしてヒットしたオブジェクトの情報を取得するスクリプト
    //====================================================================================

    // 変数宣言----------------------------------------------------------------------

    //自身のインスタンス
    private static TouchManager instance = null;

    //Rayがヒットしたものの情報
    private RaycastHit hit;

    //RayがヒットしたGameObjectを格納
    public static GameObject SelectedGameObject;

    void Awake()
    {
        //TouchManagerの唯一のインスタンスを作成
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            //タッチの位置からRayを発射
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                // RayがヒットしたGameObjectをstaticなクラス変数に格納
                SelectedGameObject = hit.collider.gameObject;
            }
        }
        else
        {
            SelectedGameObject = null;
        }

    }
}
