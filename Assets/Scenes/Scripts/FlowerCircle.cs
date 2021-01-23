using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowerCircle : MonoBehaviour
{
    public int flowerScore;

    //public PlayerController p;//ペンギンの位置情報を入れている

    [SerializeField]
    private GameObject Prefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 5.0f, RotateMode.FastBeyond360)//花を５秒かけて360度回転させる
         .SetEase(Ease.Linear)
         .SetLoops(-1, LoopType.Restart);//無限ループ


        //p = GameObject.Find("penguin").GetComponent<PlayerController>();//ペンギンの位置情報取得のため
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public void DOLocalMove(Vector3 penguin,float second)
    //{
    //    new Vector3(p.penguin.transform.position.x,p.penguin.transform.position.y,p.transform.position.z);

    //    second = 0.1f;
    //    Debug.Log(penguin);
    //}



    /// <summary>
    /// 花輪の演出
    /// 見たところは自分が考えてでは見当が付かなかった
    /// </summary>
    public void Move(PlayerController penguin)
    {
        Sequence sequence = DOTween.Sequence();//ここは見た

        sequence.Append(transform.DOScale(Vector3.zero, 1.0f));//ここも見た

        sequence.Join(transform.DOLocalMove(Vector3.zero,1.0f));//Joinまでは見た new Vector3(p.penguin.transform.position.x, p.penguin.transform.position.y, p.transform.position.z),

        Destroy(gameObject, 1.0f);

        //一回これで試して、無理そうならこれらをメソッドにして試す
        GameObject effect = Instantiate(Prefab, transform.position, Quaternion.identity);

        effect.transform.position = new Vector3(effect.transform.position.x, effect.transform.position.y-3.0f, effect.transform.position.z);

        Destroy(effect, 1.0f);
    }
}
