using UnityEngine;
using System.Collections;

public class TapStaplerSE : MonoBehaviour {

    //===============================================
    //ホッチキスの音を鳴らすスクリプト
    //===============================================

    public AudioClip audioClip;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = audioClip;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //AudioManager.Instance.PlaySE("nc52466");
            audioSource.PlayOneShot(audioClip);
        }
    }
}
