/*
 *スコア管理クラス
 *
*/

using UnityEngine;
using System.Collections;

//シングルトンを継承
public class ScoreManager : SingletonMonoBehaviour<ScoreManager> {

	//=====
	//グローバル変数
	//=====
	//public


	//private
	//スコア
	private static int m_score=0;
	//ハイスコア
	private static int m_High_Score;
	//複製禁止
	private static bool created=false;

	public int Score 
	{
		set
		{
			m_score = value;
		}
		get
		{
			return m_score;
		}
	}

	public int GetHighScore {

		get { 
			return m_High_Score;
		}
	}

	public void Awake(){

		if (!created)
		{
			DontDestroyOnLoad(this);
			created = true;
		}
		else
		{
			Destroy(this.gameObject);
		}

	}

	/*
	 *関数名	:ScoreManager
	 *内容	:パラメータを持たないコンストラクタ
	 *引数	:
	 *戻り値	:
	*/
	public ScoreManager(){}

	/*
	 *関数名	:AddScore
	 *内容	:スコア加算用関数
	 *引数	:加算される値
	 *戻り値	:
	*/
	public void AddScore(int value)
	{
		//スコア加算
		m_score+=value;

		//もしHighScoreを超えていたら
		if (m_score > m_High_Score) {
			//入れ替える
			m_High_Score = m_score;
		}
	}

	/*
	 *関数名	:Reset
	 *内容	:スコア初期化関数
	 *引数	:
	 *戻り値	:
	*/

	public void Reset()
	{
		m_score=0;
	}
}
