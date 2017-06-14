using UnityEngine;
using System.Collections;

public class NumberDisplay : MonoBehaviour {

    public int count;//スコアの数値入れる変数

    public SpriteRenderer image0;//一の位
    public SpriteRenderer image10;//十の位
    public SpriteRenderer image100;//百の位
    public SpriteRenderer image1000;//千の位
    public SpriteRenderer image10000;//万の位


    public Sprite[] spriteArray = new Sprite[10];//配列で10個作る

    void Awake()
    {
        //ここでAwakeを使ってシーンが呼び出された際にメインのほうのスコアをcountに代入
        count = StaplerSE.GetTapNum();
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        int a = count / 10000;          //万の位
        int b = count / 1000 % 10;      //千の位
        int c = count / 100 % 10;       //百の位
        int d = count / 10 % 10;        //十の位
        int e = count % 10;             //一の位


        image0.sprite = spriteArray[e];//一の位のイメージののスプライトに数字のスプライトをぶちこむ
        image10.sprite = spriteArray[d];//十の位のイメージのスプライトに数字のスプライトをぶち込む
        image100.sprite = spriteArray[c];//百の位のイメージのスプライトに数字のスプライトをぶち込む
        image1000.sprite = spriteArray[b];//千の位のイメージのスプライトに数字のスプライトをぶち込む
        image10000.sprite = spriteArray[a];//万の位のイメージのスプライトに数字のスプライトをぶち込む


    }
}
