using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("移動速度")]//Header属性を宣言するとinspector上に（）に記述した内容が表示される
    public float moveSpeed;

    [Header("落下速度")]
    public float fallSpeed;

    [Header("着水判定用。trueなら着水済")]
    public bool inWater;

    private Rigidbody rb;

    private float x;
    private float z;

    [SerializeField, Header("水しぶきのエフェクト")]
    private GameObject splashEffectPrefab = null;

    [SerializeField]
    private AudioClip splashSE = null;

    public int totalscore;//ポイント計算用

    private Vector3 penguinAngles;//姿勢制御用
    private Vector3 startAngles;//最初の姿勢を記憶用

    [SerializeField]
    private GameMaster gamemaster;//GameMaterScriptを参照するため 


    public float pcGauge;

    private bool pc;//PostureChangeScriptから受け取ったbool型のデータをこの箱に入れるため


    public GameObject penguin;

    private FlowerCircle flowercircle;

    private float second = 1.0f;


    private Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(0, 180, 180);

        startAngles = transform.eulerAngles;



        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // if (inWater == false)//キー入力の受付
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
        }

        //velocity（速度）に新しい値を代入して移動
        rb.velocity = new Vector3(x * moveSpeed, -fallSpeed, z * moveSpeed);

        //transform.eulerAngles = startAngles; //ボタンを押したら姿勢情報が更新されるからこれで戻すが、これをアップデートに入れると姿勢情報が更新され続けて浮かび上がってこない

    }

    private void Update()
    {
        pc = GameObject.Find("Button").GetComponent<PostureChange>().bp;//ButtonオブジェクトについているPostureChangeScriptの中のbpのデータをpcに代入する

        pcGauge = GameObject.Find("GameMaster").GetComponent<GameMaster>().postureGauge.fillAmount;


        if (pc == true && inWater == false && pcGauge > 0.0f)//ボタンを押したら姿勢変更と落下速度半減
        {
            transform.eulerAngles = new Vector3(penguinAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);

            penguinAngles.x = 90f;

            //rb.velocity = new Vector3(x * moveSpeed, -fallSpeed / 2.0f, z * moveSpeed);処理が重いから

            rb.drag = 25.0f;//空気抵抗あり

            anim.SetBool("Prone", true);

        }

        if (pc == false && inWater == false)
        {
            transform.eulerAngles = startAngles;

            rb.drag = 0f;//空気抵抗なし

            anim.SetBool("Prone", false);

        }
    }

    private void OnTriggerEnter(Collider col)//IｓTriggerがオンのコライダーを持つゲームオブジェクトを通過した場合に呼び出されるメソッド
    {

        if (col.gameObject.tag == "Water" && inWater == false)//通過したゲームオブジェクトのtagがWaterかつisWaterがfalse（未着水）の状態であるなら
        {
            inWater = true;//着水状態に変更する

            //Debug.Log("着水" + inWater);

            StartCoroutine(penginFloat());//水面に顔を出そうとするコルーチン

            GameObject effect = Instantiate(splashEffectPrefab, transform.position, Quaternion.identity);//着水位置より少し低い位置でエフェクトを使いたいから着水位置を把握して、その情報をeffect変数に入れる

            effect.transform.position = new Vector3(effect.transform.position.x, effect.transform.position.y, effect.transform.position.z - 0.5f);

            Destroy(effect, 2.0f);

        }

        //AudioSource.PlayClipAtPoint(splashSE, transform.position);

        if (col.gameObject.tag == "FlowerCircle")
        {
            col.GetComponent<BoxCollider>().enabled = false;

            flowercircle = col.transform.parent.GetComponent<FlowerCircle>();

            totalscore += flowercircle.flowerScore;

            gamemaster.Addscore(totalscore);

            flowercircle.transform.SetParent(gameObject.transform);

            ///<summary>花輪の演出</summary>
            //penguin.transform.position = new Vector3(penguin.transform.position.x, penguin.transform.position.y, penguin.transform.position.z);

            //Debug.Log(penguin.transform.position);

            flowercircle.Move(this);

            //FlowerCircle.DOLocalMove(new Vector3(penguin.x,penguin.y,penguin.z),1);

        }


    }


    /// <summary>
    /// 水面に顔を出す
    /// </summary>
    /// <returns></returns>
    IEnumerator penginFloat()//方向キーを押していたら斜めで浮いてきてペンギンが表示されない
    {
        yield return new WaitForSeconds(1.0f);

        rb.isKinematic = true;//重力を切る

        transform.eulerAngles = new Vector3(-30, 180, 0);//正面を向かせる

        transform.DOMoveY(-0.5f, 1.0f);//yの4.7の位置に1秒かけて動く

    }

}

