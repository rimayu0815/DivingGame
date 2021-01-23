using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;//ShinyEffectForUGUIを利用するために必要な宣言


public class GameMaster : MonoBehaviour
{
    private int score = 0;
    [SerializeField]
    private Text ScoreLabel;


    /// <summary>
    /// プレイ画面右下の距離を表示
    /// </summary>
    private GameObject p;//penguinオブジェクトのデータを入れる箱
    private float startPosition;//ペンギンの最初の位置---これいらない
    private float changePosition;
    private GameObject w;//水面オブジェクトを入れる箱
    private float waterPosition;//水面の位置---これいらない--57行目のchange～がうまくいかなかったから復活
    private float distance;//ペンギンと水面の間の距離を図る
    private float distanceCut;//小数点以下を切り捨てるため
    [SerializeField]
    private Text distanceLabel;


    /// <summary>
    ///プレイ画面左下の姿勢変更ゲージを作成 
    /// </summary>
    public Image postureGauge;//fillAmountを使うためにデータを入れる
    public GameObject pg;//上のpostureGaugeにデータを入れるため
    //public float gauge = 1.0f;//姿勢切り替えがゲージの間だけ使えるようにする  要らない
    public bool gtf;//PostureChangeからbpのデータを手に入れるため
    public int gaugeMax;//要らない
    public float countTime = 2.0f;//これでゲージの減りを調整
    public PostureChange postureChange;//ゲージが満タンのとき以外に切り替えれないようにするため
    //完成したがinteractibleを使って再度作り直し
    private Button buttonAttitude;


    [SerializeField]
    private ShinyEffectForUGUI shinyEffect;

    public bool isGaugeMax;





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



        pg = GameObject.Find("ImageGauge");//ImageGaugeにあるfillAmountを使うためデータを取得
        postureGauge = pg.GetComponent<Image>();//Image型のpostureGaugeにデータを入れる

        postureGauge.fillAmount = 1.0f;//最初のゲージ量を設定

        buttonAttitude = GameObject.Find("Button").GetComponent<Button>();

        postureChange = GameObject.Find("Button").GetComponent<PostureChange>();


    }

    // Update is called once per frame
    void Update()
    {
        ///<summary>
        ///プレイ画面右下の距離を表示
        /// </summary>
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


        
        ///<summary>
        ///プレイ画面左下の姿勢変更ゲージを作成
        /// </summary>
        gtf = postureChange.bp;//ボタンを押しているときにゲージを変動させるために取得

        if (gtf == true)
        {
            postureGauge.fillAmount -= 1.0f / countTime * Time.deltaTime;

            if (postureGauge.fillAmount <= 0.0f)
            {
                postureGauge.fillAmount = 0.0f;

                buttonAttitude.interactable = false;

                //pcAngle.angle.z = 0;
                p.transform.eulerAngles = new Vector3(0, 0, 180);
                buttonAttitude.transform.eulerAngles = new Vector3(0, 0, 180);
                postureChange.bp = false;
                gtf = postureChange.bp;
            }
        }
        else if (gtf == false)
        {
            postureGauge.fillAmount +=1.0f/countTime * Time.deltaTime;
            if (postureGauge.fillAmount >= 1.0f && isGaugeMax == false)
            {
                isGaugeMax = true;

                postureGauge.fillAmount = 1.0f;

                shinyEffect.Play(0.5f);//なぜかクリックした時に光るようになっている
                Debug.Log(postureGauge.fillAmount);
                
                buttonAttitude.interactable = true;

            }
        }
        //ptf = postureGauge.fillAmount;//このデータをPostureChangeに送ってる

        //if (pcAngle.angle.z == 90.0f)
        //{
        //    buttonAttitude.interactable = true;
        //}
        //else
        //{
        //    buttonAttitude.interactable = false;
        //}

        //Debug.Log(ptf);

        //if(postureGauge.fillAmount = 1.0f)
        //{
        //ptf = true;
        //}
        //else 
        //{
        //ptf = false;
        //}
        //ゲージが0になったら、強制的に切り替えるよう他Script同様修正






    }

    public void Addscore( int totalscore)
    {
        ScoreLabel.text = "Score:" + totalscore;
    }

    public void ChangeButtonInteractable(float angle)
    {
        //ボタンが90度だったら
        if(angle == 90.0f)
        {
            buttonAttitude.interactable = true;
        }
        else
        {
            buttonAttitude.interactable = false;
            //gtf = true;
        }
    }
}
