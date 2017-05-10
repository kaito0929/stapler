using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public enum TimerState
{
	play,
	stop,
	pause
}


public class Timer : MonoBehaviour 
{


	//開始時間
	private float StartTime = 0;

	//終わり時間
	private float EndTime = 0;

	//残り時間
	private float RemainingTime = 0;

    public float checktime = 0;

	//一度でも0になったらtrueに
	//playでリセット
	private bool Zero = true;

    //タイマー実行回数
    private uint Couter = 0;

	//設定する時間
	private float SetterTime = 0;

	//タイマーの状況
	TimerState state = TimerState.stop;



	// Use this for initialization
	void Start ()
    {
		GetComponent<Text>().text = RemainingTime.ToString("0");
	}
	
	//// Update is called once per frame
	//void Update () 
    //{
		
	//}

	void FixedUpdate()
	{

			if (state == TimerState.play) {
				//現在の時間が終わり時間を超えていなかった時の処理
				if (EndTime > Time.time) {
					//残り時間計算
					RemainingTime = EndTime - Time.time;
					checktime = RemainingTime;
				} else {
					//-表示を防ぐため0に指定
					RemainingTime = 0;
				}
			

				//一度目のゼロ
				if ((RemainingTime <= 0) && (Zero == false)) {
					Debug.Log ("stop");
					AudioManager.Instance.PlaySE ("gong-played2");
					state = TimerState.stop;
					Zero = true;
				}
			}

			//テキストに残り時間を設定
			GetComponent<Text> ().text = RemainingTime.ToString ("0");
		
    }

	//制限時間を設定
	public void SetTime(float sec,bool SetRemaingTime = true)
	{
		SetterTime = sec;
		if (SetRemaingTime) {
			RemainingTime = sec;
		}
	}

	//タイマーをスタート
	public void StartTimer()
	{
		switch (state)
		{
			case TimerState.stop:
				StartTime = Time.time;
				EndTime = StartTime + SetterTime;
				Zero = false;
				Couter++;
				break;

			case TimerState.play:

				break;

			case TimerState.pause:
				StartTime = Time.time;
				EndTime = StartTime + RemainingTime;
				break;
			default:
				break;
		}

		state = TimerState.play;
	}

	//タイマーを時間を指定してスタート
	public void StartTimer(float sec)
	{
		SetterTime = sec;
		StartTimer();

	}

	//タイマーを停止
	public void StopTimer()
	{
		state = TimerState.stop;
	}

	//タイマーを一時停止
	public void PauseTimer()
	{
		state = TimerState.pause;
	}

	//残り時間を取得
	public float GetRemainningTime()
	{
		return RemainingTime;
	}

	//タイマーが0かどうか
	public bool GetZero()
	{
		return Zero;
	}

	//タイマーの実行回数を返します
	public uint GetCounter()
	{
		return Couter;
	}



}
