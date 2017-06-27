using UnityEngine;
using System.Collections;

public class WatermillRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //くるくると回転する
        gameObject.transform.Rotate(new Vector3(0, 0, 5));

    }
}
