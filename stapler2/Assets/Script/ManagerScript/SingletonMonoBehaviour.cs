//シングルトンクラス

using UnityEngine;
using System.Collections;


public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

	//ジェネリックにして複数の方に対応したインスタンスを定義
	private static T instance;
	public static T Instance
	{

		get
		{
			if(instance==null)
			{
				instance=(T)FindObjectOfType(typeof(T));

				if(Instance==null)
				{
					Debug.LogError (typeof(T) + "is nothing");
				}


			}
			return instance;
		}
	}

	/*protected void Awake()
	{
		CheckInstance ();
	}

	protected bool CheckInstance()
	{
		if (this == Instance) {
			return true;
		}
		Destroy (this);

		return false;
	}
	*/
}
