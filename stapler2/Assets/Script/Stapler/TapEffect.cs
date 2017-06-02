using UnityEngine;
using System.Collections;

//====================================================
//エフェクトの表示非表示を切り替えるスクリプト
//====================================================

public class TapEffect : MonoBehaviour {


    // 変数宣言----------------------------------------------------------------------

    [SerializeField]
    ParticleSystem tapEffect;

    [SerializeField]
    Camera _camera;

    public GameObject effect;

    //表示する時間
    private float DisplayTime = 1f;
    //時間を加算する変数
    float time = 0f;

    //時間を加算するかのフラグ
    bool CountFlag;

    Vector3 pos;

    //Ray関係
    private RaycastHit hit;
    private Ray ray;

    public Renderer rd;


    // Use this for initialization
    void Start() {
        CountFlag = false;
        effect.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                tapEffect.transform.position = hit.point;
                pos = tapEffect.transform.position;
                pos.x += 1f;
                pos.z -= 2f;
                tapEffect.transform.position = pos;
            }


            effect.SetActive(true);

            //時間を加算するフラグをtrueに
            CountFlag = true;
            //変数を初期化
            time = 0f;
        }

        //フラグがtrueの間に時間を加算
        if (CountFlag == true)
        {
            time += Time.deltaTime;
        }

        //加算した時間が設定した制限を超えたら処理
        if (time > DisplayTime)
        {
            rd.GetComponent<Renderer>().material.SetColor("_Color", new Color(255, 255, 255));

            //エフェクトの表示を消す
            effect.SetActive(false);
            //フラグをfalse
            CountFlag = false;
            //変数を初期化
            time = 0f;
        }
    }

}
