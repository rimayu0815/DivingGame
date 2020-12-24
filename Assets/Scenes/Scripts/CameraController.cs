using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField]
    //private GameObject playerObj;

    [SerializeField]
    private PlayerController playerController;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerController.transform.position;//カメラと追随対象のゲームオブジェクト（ペンギン等）との距離（初期位置）を補正値として取得
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.inWater == true)//もし着水状態だったら
        {
            return;
        }



        if (playerController != null)
        {
            transform.position = playerController.transform.position + offset;//カメラの位置を追随対象の位置＋補正値にする
        }
        
    }

}
