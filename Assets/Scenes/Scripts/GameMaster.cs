using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private int score = 0;
    [SerializeField]
    private Text ScoreLabel;

    private GameObject p;//penguinオブジェクトのデータを入れる箱
    private float startPosition;//ペンギンの最初の位置---これいらない
    private float changePosition;
    private GameObject w;//水面オブジェクトを入れる箱
    private float waterPosition;//水面の位置---これいらない--57行目のchange～がうまくいかなかったから復活
    private float distance;//ペンギンと水面の間の距離を図る
    private float distanceCut;//小数点以下を切り捨てるため
    [SerializeField]
    private Text distanceLabel;

    // Start is called before the first frame update
    void Start()
    {
        ScoreLabel.text = "Score:" + score;


        p = GameObject.Find("penguin");//ペンギンのｙ座標取得のため
        startPosition = p.transform.position.y;//これ作ったけどいらない

        w = GameObject.Find("WaterProDaytime");
        waterPosition = w.transform.position.y;//これ作ったけどいらない---下にある57行目がなぜかうまくいかないため復活

        //distance = (p.transform.position.y - w.transform.position.y)*100f;//変数作って入れなくてもこれでできる

       　distance = (startPosition - waterPosition + 0.5f)* 100f;//上の式に変更

        distanceCut = Mathf.Floor(distance)/100f;//100掛けて、小数点以下切り捨てて、100掛ければ小数点以下第二位まで残せる

        Debug.Log(distanceCut);

        distanceLabel.text = distanceCut + "m";
    }

    // Update is called once per frame
    void Update()
    {

        if(distance>0.00f)
        {
            //score = GameObject.Find("penguin").GetComponent<PlayerController>().totalscore;



            //GameObject p = GameObject.Find("penguin");

            changePosition = p.transform.position.y;//なぜかここで止まる、うまくいかない

            //distance = (changePosition - w.transform.position.y) * 100f;//57がうまくいかないためとりあえずコメントアウト

            distance = (changePosition - waterPosition)*100f;//上の式に変更---なぜか５７行目がうまくいかないから復活

            distanceCut = Mathf.Floor(distance)/100f;


            distanceLabel.text = distanceCut + "m";

        }

        else
        {
            distance = 0.00f;

            distanceLabel.text = distance + ".00m";
        }
    }

    public void Addscore( int totalscore)
    {
        ScoreLabel.text = "Score:" + totalscore;
    }
}
