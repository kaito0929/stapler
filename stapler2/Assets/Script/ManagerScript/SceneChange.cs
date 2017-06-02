using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    //世界観の説明画面とエンディングからの遷移条件
    public int ChangeNum;
    //ステージ1からの遷移条件
    public Stage_Clear_NextPage pageNum;

    //アリスに攻撃が当たったフラグ
    //ゲームオーバー画面への遷移条件
    public AliceGameOver aliceGameOver;

    //タイトル画面への遷移を行うための条件
    public Title title;
    //ステージの最初からやり直すための条件
    public Retry retry;


    //public Timer time;
    //次のシーンの名前
    public  string nextScene;
    // Use this for initialization
    private static float margin = 0.7f;
	//遷移したかどうかを保存する変数
	bool Moved = false;

    void Start ()
    {
        //  AudioManager.Instance.PlayBGM("");
        // AudioManager.Instance.PlaySE("");
        //nextScene = null;
        ChangeNum = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log (nextScene);
        if (!Moved)
        {
            switch (Application.loadedLevelName)
            {
                case "Title"://タイトル画面
                    /*次のシーンに遷移する方法*/

                    //画面をタップしたら遷移開始
                    if (Input.GetMouseButton(0))
                    {
                        ChangeScene();
                    }
                    break;
                case "Story"://世界観の説明画面

                    ChangeNum = PageTurn.GetNum();

                    //最後までページが進んだ状態でタップしたならば遷移開始
                    if (ChangeNum == 5)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            ChangeScene();
                        }
                    }
                    break;
                case "stage1"://ステージ1

                    //ステージ1でクリア条件を達成したら遷移開始
                    if (pageNum.GetTurnNum() == -1f)
                    {
                        ChangeScene();
                    }
                    //もしもゲームオーバー条件を満たしてしまった場合には
                    //ゲームオーバー画面への遷移を開始させる
                    else if (aliceGameOver.GetGameOverFlag() == true)
                    {
                        ChangeScene();
                    }

                    break;
                case "stage2"://ステージ2

                    //ステージ2でのクリア条件を達成したら遷移開始
                    if (pageNum.GetTurnNum() == -1f)
                    {
                        ChangeScene();
                    }
                    //もしもゲームオーバー条件を満たしてしまった場合には
                    //ゲームオーバー画面への遷移を開始させる
                    else if (aliceGameOver.GetGameOverFlag() == true)
                    {
                        ChangeScene();
                    }
                    break;
                case "stage3"://ステージ3

                    //ステージ3でのクリア条件を達成したら遷移開始
                    if (pageNum.GetTurnNum() == -1f)
                    {
                        ChangeScene();
                    }
                    //もしもゲームオーバー条件を満たしてしまった場合には
                    //ゲームオーバー画面への遷移を開始させる
                    else if (aliceGameOver.GetGameOverFlag() == true)
                    {
                        ChangeScene();
                    }
                    break;
                case "Ending"://エンディング画面

                    ChangeNum = PageTurn.GetNum();

                    //エンディングの画像が最後まで進んだ状態でタップして遷移開始
                    if (ChangeNum == 4)
                    {
                        if (Input.GetMouseButton(0))
                        {
                            ChangeScene();
                        }
                    }
                    break;
                case "GameOver"://ゲームオーバー画面
                    
                    //ステージの最初からやり直すボタンを押された場合に処理
                    //進行中だったステージの最初へと遷移する
                    if(retry.GetRetryFlag()==true)
                    {
                        ChangeScene();
                    }
                    //タイトルへ戻るボタンを押されたら処理
                    //タイトル画面へと遷移を行う
                    else if(title.GetTitleFlag()==true)
                    {
                        ChangeScene();
                    }

                    break;
            }
        }
        if (nextScene == SceneManager.GetActiveScene().name)
        {
            //次のシーンをnull
            //nextScene = null;
            //Debug.Log(" null or NotNull:::" + nextScene);
            Moved = false;
        }
        /*	if ((nextScene != null) && (Moved == false))
            {
                Debug.Log ("nextScene name:::" + nextScene);
                Moved = true;
                //シーンの遷移
                FadeManager.Instance.LoadLevel (nextScene, 2.0f);

            }

            if(nextScene == SceneManager.GetActiveScene().name)
            {
                //次のシーンをnull
                nextScene = null;
                Debug.Log(" null or NotNull:::" + nextScene);
                Moved = false;
            }*/

    }
    public void ChangeScene()
    {
		FadeManager.Instance.LoadLevel(nextScene, 1.0f);
        Moved = true;
    }
}
