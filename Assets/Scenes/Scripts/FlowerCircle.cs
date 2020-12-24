using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowerCircle : MonoBehaviour
{
    public int flowerScore;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.DORotate(new Vector3(0, 360, 0), 5.0f, RotateMode.FastBeyond360)//花を５秒かけて360度回転させる
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType. Restart);//無限ループ

    }


}
