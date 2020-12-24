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

    //private Vector3 penginAngles;

    public int totalscore;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.eulerAngles = new Vector3(0, 180, 180);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //キー入力の受付
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        //velocity（速度）に新しい値を代入して移動
        rb.velocity = new Vector3(x * moveSpeed, -fallSpeed, z * moveSpeed); 

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
            totalscore += col.transform.parent.GetComponent<FlowerCircle>().flowerScore;

        }
    }


    /// <summary>
    /// 水面に顔を出す
    /// </summary>
    /// <returns></returns>
    IEnumerator penginFloat()
    {
        yield return new WaitForSeconds(1.0f);

        rb.isKinematic = true;//重力を切る

        transform.eulerAngles = new Vector3(0, 180, 0);//正面を向かせる

        transform.DOMoveY(4.7f, 1.0f);//yの4.7の位置に1秒かけて動く

    }
}

