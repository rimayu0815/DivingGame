using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerObj.transform.position;//カメラと追随対象のゲームオブジェクト（ペンギン等）との距離（初期位置）を補正値として取得
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj !=null)
        {
            transform.position = playerObj.transform.position + offset;//カメラの位置を追随対象の位置＋補正値にする
        }
    }
}
