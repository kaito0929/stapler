using UnityEngine;
using System.Collections;

//==========================================
//杵をコントロールするスクリプト
//==========================================

public class PestleControl : MonoBehaviour {

    // 変数宣言----------------------------------------------------------------------

    private Vector3 pos;

    //杵の移動方向を決定する
    private enum MOVEDIR
    {
        UP,
        DOWN,
    }
    private MOVEDIR moveDir;

    // Use this for initialization
    void Start () {
        moveDir = MOVEDIR.UP;
    }

    // Update is called once per frame
    void Update()
    {

        pos = gameObject.transform.position;

        switch (moveDir)
        {
            case MOVEDIR.UP:

                if (pos.y <= 7.5f)
                {
                    pos.y += 0.1f;
                }
                else
                {
                    moveDir = MOVEDIR.DOWN;
                }

                break;
            case MOVEDIR.DOWN:

                if (pos.y >= 2.75f)
                {
                    pos.y -= 0.1f;
                }
                else
                {
                    moveDir = MOVEDIR.UP;
                }

                break;
        }

        gameObject.transform.position = pos;

    }
}
