//===============================
//
//
//オーディオを管理するクラス
/// <summary>
/// Audio manager.
/// </summary>
/// 使い方
/// すきなスクリプトから１行指定すればならせます
/// AudioManager.Instance.Play ("BGMファイル名");
/// 
//===============================

using UnityEngine;
//オーディオで使うものの読み込み
using UnityEngine.Audio;
using System;
//
using System.Linq;
using System.Collections;
//ジェネリックコレクションを使用する為に宣言します
using System.Collections.Generic;

//シングルトンクラスを継承
public class AudioManager : SingletonMonoBehaviour<AudioManager> {

	//======
	//グローバル変数
	//======
	//デバックモード
	public bool DebugMode = true;

	//========================================================================
	//BGM用
	//BGM用オーディオソース
	//クロスフェードに対応させるため二つ用意
	private List<AudioSource> bgmSource = null;
	/// 再生可能なBGM(AudioClip)のリストです。
	/// 実行時にBGMファイルから自動で読み込む
	private Dictionary<string,AudioClip> AudioClipDict = null;

	/// 現在再生中のAudioSource
	/// FadeOut中のものは除く
	[NonSerialized]
	public AudioSource CurrentAudioSource = null;
	//========================================================================


	//========================================================================
	//SE用
	//SEのファイルを格納する
	public List<AudioClip> 	SEList=null;
	//総読み込みファイル数
	//public int MaxSE = 10;
	//SE用オーディオソース
	private List<AudioSource> seSources = null;
	//Dictionary<Keyの型,値の型:>:キーを指定すると値を取得することができるコレクション
	private Dictionary<string,AudioClip> seDict = null;
	[NonSerialized]
	public AudioSource currentSe = null;
	//========================================================================


	//========================================================================
	//Common
	/// BGM再生音量
	/// 次回フェードインから適用されます。
	/// 再生中の音量を変更するには、CurrentAudioSource.Volumeを変更してください。
	[Range (0f, 1f)]
	public float TargetVolume = 1.0f;

	/// フェードイン、フェードアウトにかかる時間です。
	public float TimeToFade = 2.0f;

	/// フェードインとフェードアウトの実行を重ねる割合です。
	/// 0を指定すると、完全にフェードアウトしてからフェードインを開始します。
	/// 1を指定すると、フェードアウトとフェードインを同時に開始します。
	[Range (0f, 1f)]
	public float CrossFadeRatio = 1.0f;

	/// コルーチン中断に使用
	private IEnumerator fadeOutCoroutine;
	/// <summary>
	/// コルーチン中断に使用
	/// </summary>
	private IEnumerator fadeInCoroutine;
	//========================================================================

	private static bool created = false;

	/// FadeOut中、もしくは再生待機中のAudioSource
	public AudioSource SubAudioSource {
		get { 
			//bgmSourcesのうち、CurrentAudioSourceでない方を返す
			if (this.bgmSource == null) {
				return null;
			}
			//全ての要素を検索
			foreach (AudioSource s in this.bgmSource) {
				//現在の要素と同じで無ければ
				if (s != this.CurrentAudioSource) {
					return s;
				}
			}
			return null;
		}
	}

	//クロスフェードのためのsubAudioSource
	public AudioSource SubSESource{
		get{ 
			//オーディオソースが存在しなかったら
			if (this.seSources == null) {
				return null;
			}
			//全ての要素を検索
			foreach (AudioSource s in this.seSources) {
				//現在のSEと同じじゃ無ければ
				if (s != this.currentSe) {
					//オーディオソースを返す
					return s;
				}
			}
			return null;
		}

	}

	//一番最初に呼び出す
	public void Awake()
	{
		if (!created)
		{
			DontDestroyOnLoad(this);
			created = true;
		}
		else
		{
			Destroy(this.gameObject);
		}

		//オーディオリスナーの作成
		if(FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
		{
			//オーディオリスナーの取得
			this.gameObject.AddComponent<AudioListener>();
		}
		//オーディオソースの作成
		//クロスフェードをするためにオーディオソースをふたつ用意
		this.bgmSource = new List<AudioSource> ();
		this.bgmSource.Add (this.gameObject.AddComponent<AudioSource> ());
		this.bgmSource.Add (this.gameObject.AddComponent<AudioSource> ());
		//全ての要素のオプションを変更
		foreach (AudioSource s in this.bgmSource) {
			//開始時しかならない設定を切る
			s.playOnAwake = false;
			//初期のvolumeは０
			s.volume = 0f;
			//ループ再生はオン
			s.loop = true;
		}

		//[Resources/Audio/BGM]フォルダからBGMを探す
		this.AudioClipDict = new Dictionary<string, AudioClip> ();
		//LoadAllで引数のパスの中のものを全て探して加える
		foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Audio/BGM")) {
			this.AudioClipDict.Add (bgm.name, bgm);
		}


		//SEオーディオソースのリスト作成
		this.seSources = new List<AudioSource>();
		//SEも同様に二つ用意
		this.seSources.Add (this.gameObject.AddComponent<AudioSource> ());
		this.seSources.Add (this.gameObject.AddComponent<AudioSource> ());
		//全ての要素のオプションを変更
		foreach (AudioSource s_se in this.seSources) {
			//開始時しかならない設定を切る
			s_se.playOnAwake = false;
			s_se.volume = 1f;
			//ループ再生はオフ
			s_se.loop = false;
		}

		//[Resources/Audio/SE]フォルダからSEを探す
		this.seDict = new Dictionary<string, AudioClip>();
		//オーディオクリップをSEフォルダから読み込み
		foreach (AudioClip se in Resources.LoadAll<AudioClip>("Audio/SE")) {
			this.seDict.Add (se.name, se);
		}
	}


	/*
	 *関数名 :playSE
	 *内容	:SEを流す
	 *引数	:setName:SEファイル名
	 *戻り値 :
	*/
	public void PlaySE(string seName)
	{
		//Dictionary内のファイル名を探す
		if (!this.seDict.ContainsKey (seName)) {
			Debug.LogError (string.Format ("SE名[{0}]が見つかりません", seName));
			return;
		}
		//もしオーディオソースが存在し、クリップ名が検索したいSE名と同じだったら
//		if((this.currentSe != null) && (this.currentSe.clip == this.seDict[seName]))
//		{
//			//早期リターン
//			return;
//		}
		//現在のSEを止める
		//this.StopSE ();
		//現在のオーディオソースに使っていないソースを入れる
		this.currentSe = this.SubSESource;
		//クリップ名を読み込んで来たSE名に変更
		this.currentSe.clip = this.seDict [seName];
		//SEを鳴らす
		this.currentSe.Play();
	}

	/*
	 *関数名 :StopSE
	 *内容	:全リストを検索してSEを止める
	 *引数	:
	 *戻り値 :
	*/
	public void StopSE()
	{
		//全ての要素を検索しストップする
		foreach (AudioSource s in this.seSources) {
			s.Stop ();
		}
	}

////////////////////////////////////////////////////////////////////////////////////////////////////
///以下BGM関連の関数
	/*
	 *関数名 :PlayBGM
	 *内容	:BGMの再生
	 *引数	:bgm名
	 *戻り値 :
	*/
	public void PlayBGM(string bgmName)
	{
		if (!this.AudioClipDict.ContainsKey (bgmName)) {
			Debug.LogError (string.Format ("BGM名[{0}]が見つかりません。", bgmName));  
			return;
		}

		if ((this.CurrentAudioSource != null)
			&& (this.CurrentAudioSource.clip == this.AudioClipDict [bgmName])) {
			//すでに指定されたBGMを再生中
			return;
		}

		//クロスフェード中なら中止
		stopFadeOut ();
		stopFadeIn ();

		//再生中のBGMをフェードアウト開始
		this.StopBGM ();

		float fadeInStartDelay = this.TimeToFade * (1.0f - this.CrossFadeRatio);

		//BGM再生開始
		this.CurrentAudioSource = this.SubAudioSource;
		this.CurrentAudioSource.clip = this.AudioClipDict [bgmName];
		this.fadeInCoroutine = fadeIn (this.CurrentAudioSource, this.TimeToFade, this.CurrentAudioSource.volume, this.TargetVolume, fadeInStartDelay);
		StartCoroutine (this.fadeInCoroutine);

	}
		
	/*
	 *関数名 :StopBGM
	 *内容	:BGMを止める
	 *引数	:
	 *戻り値 :
	*/
	public void StopBGM()
	{
		if (this.CurrentAudioSource != null) {
			this.fadeOutCoroutine = fadeOut (this.CurrentAudioSource, this.TimeToFade, this.CurrentAudioSource.volume, 0f);
			StartCoroutine (this.fadeOutCoroutine);
		}
	}

	/// <summary>
	/// BGMをただちに停止します。
	/// </summary>
	public void StopImmediately ()
	{
		this.fadeInCoroutine = null;
		this.fadeOutCoroutine = null;
		foreach (AudioSource s in this.bgmSource) {
			s.Stop ();
		}
		this.CurrentAudioSource = null;
	}

	/// <summary>
	/// BGMをフェードインさせながら再生を開始します。
	/// </summary>
	/// <param name="bgm">AudioSource</param>
	/// <param name="timeToFade">フェードインにかかる時間</param>
	/// <param name="fromVolume">初期音量</param>
	/// <param name="toVolume">フェードイン完了時の音量</param>
	/// <param name="delay">フェードイン開始までの待ち時間</param>
	private IEnumerator fadeIn (AudioSource bgm, float timeToFade, float fromVolume, float toVolume, float delay)
	{
		if (delay > 0) {
			yield return new WaitForSeconds (delay);
		}


		float startTime = Time.time;
		bgm.Play ();
		while (true) {
			float spentTime = Time.time - startTime;
			if (spentTime > timeToFade) {
				bgm.volume = toVolume;
				this.fadeInCoroutine = null;
				break;
			}

			float rate = spentTime / timeToFade;
			float vol = Mathf.Lerp (fromVolume, toVolume, rate);
			bgm.volume = vol;
			yield return null;
		}
	}

	/// <summary>
	/// BGMをフェードアウトし、その後停止します。
	/// </summary>
	/// <param name="bgm">フェードアウトさせるAudioSource</param>
	/// <param name="timeToFade">フェードアウトにかかる時間</param>
	/// <param name="fromVolume">フェードアウト開始前の音量</param>
	/// <param name="toVolume">フェードアウト完了時の音量</param>
	private IEnumerator fadeOut (AudioSource bgm, float timeToFade, float fromVolume, float toVolume)
	{ 
		float startTime = Time.time;
		while (true) {
			float spentTime = Time.time - startTime;
			if (spentTime > timeToFade) {
				bgm.volume = toVolume;
				bgm.Stop ();
				this.fadeOutCoroutine = null;
				break;
			}

			float rate = spentTime / timeToFade;
			float vol = Mathf.Lerp (fromVolume, toVolume, rate);
			bgm.volume = vol;
			yield return null;
		}
	}

	/// <summary>
	/// フェードイン処理を中断します。
	/// </summary>
	private void stopFadeIn ()
	{
		if (this.fadeInCoroutine != null)
			StopCoroutine (this.fadeInCoroutine);
		this.fadeInCoroutine = null;

	}

	/// <summary>
	/// フェードアウト処理を中断します。
	/// </summary>
	private void stopFadeOut ()
	{
		if (this.fadeOutCoroutine != null) {
			StopCoroutine (this.fadeOutCoroutine);
		}

		this.fadeOutCoroutine = null;
	}




	/// デバッグ用操作パネルを表示
	public void OnGUI ()
	{
		if (this.DebugMode)
		{
			//AudioClipが見つからなかった場合
			if (this.AudioClipDict.Count == 0) {
				GUI.Box (new Rect (10, 10, 200, 50), "Audio Manager(Debug Mode)");
				GUI.Label (new Rect (10, 35, 80, 20), "Audio clips not found.");
				return;
			}

			//枠
			GUI.Box (new Rect (10, 10, 200, 150 + this.AudioClipDict.Count * 25), "BGM Manager(Debug Mode)");
			int i = 0;
			GUI.Label (new Rect (20, 30 + i++ * 20, 180, 20), "Target Volume : " + this.TargetVolume.ToString ("0.00"));
			GUI.Label (new Rect (20, 30 + i++ * 20, 180, 20), "Time to Fade : " + this.TimeToFade.ToString ("0.00"));
			GUI.Label (new Rect (20, 30 + i++ * 20, 180, 20), "Crossfade Ratio : " + this.CrossFadeRatio.ToString ("0.00"));

			i = 0;
			//再生ボタン
			foreach (AudioClip bgm in this.AudioClipDict.Values) {
				bool currentBgm = (this.CurrentAudioSource != null && this.CurrentAudioSource.clip == this.AudioClipDict [bgm.name]);

				if (GUI.Button (new Rect (20, 100 + i * 25, 40, 20), "Play")) {
					this.PlayBGM (bgm.name);
				}
				string txt = string.Format ("[{0}] {1}", currentBgm ? "X" : "_", bgm.name);
				GUI.Label (new Rect (70, 100 + i * 25, 1000, 20), txt);

				i++;
			}

			//停止ボタン
			if (GUI.Button (new Rect (20, 100 + i++ * 25, 180, 20), "Stop")) {
				this.StopBGM ();
			}
			if (GUI.Button (new Rect (20, 100 + i++ * 25, 180, 20), "Stop Immediately")) {
				this.StopImmediately ();
			}
		}

		if (this.DebugMode) {
			int j = 0;
			foreach (AudioClip se in this.seDict.Values)
			{
				bool iscurrentSE = (this.currentSe != null && this.currentSe.clip == this.seDict [se.name]);

				if (GUI.Button (new Rect (320, 100 + j * 25, 40, 20), "Play")) {
					this.PlaySE (se.name);
				}
				string txt = string.Format ("[{0}] {1}", iscurrentSE ? "X" : "_", se.name);
				GUI.Label (new Rect (370, 100 + j * 25, 1000, 20), txt);
				j++;
			}
			if (GUI.Button (new Rect (320, 200+j * 25, 180, 20), "Stop")) {
				this.StopSE ();
			}

		}
	}
}




