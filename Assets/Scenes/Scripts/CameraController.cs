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

    public Camera maincamera;
    //public bool mainCamera = true;
    public Camera fpscamera;
    //public bool fpsCamera = false;
    public Camera selfcamera;
    //public bool selfCamera = false;
    //public PlayerController water;
    //public  Button cameraButton; UIがないから作れない、GameMasterで作る必要がある


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerController.transform.position;//カメラと追随対象のゲームオブジェクト（ペンギン等）との距離（初期位置）を補正値として取得

        //maincamera = GameObject.Find("MainCamera");
        //fpscamera = GameObject.Find("FPSCamera");
        //selfcamera = GameObject.Find("SelfishCamera");

        fpscamera.enabled = false;
        selfcamera.enabled = false;

        //water = GameObject.Find("penguin").GetComponent<PlayerController>();
        //cameraButton = GameObject.Find("CameraButton").GetComponent<Button>;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.inWater == true)//もし着水状態だったら　　
        {
            return;

            //    selfcamera.enabled = false;ここに書き足したが、上手くいかず　おそらくreturnがあるから

            //    maincamera.enabled = true;

            //    fpscamera.enabled = false;
        }



        if (playerController != null)
        {
            transform.position = playerController.transform.position + offset;//カメラの位置を追随対象の位置＋補正値にする
        }

        //if(playerController.inWater == true )強制的に三人称カメラにしようとしたが、上手くいかず
        //{
        //    selfcamera.enabled = false;

        //    maincamera.enabled = true;

        //    fpscamera.enabled = false;
        //}
        

    }
    /// <summary>
    /// カメラ切り替えメソッド
    /// </summary>
    public void cameraChangeButton()
    {
        //if (mainCamera == true) 失敗
        //{
        //    mainCamera = false;
        //    maincamera.SetActive(false);

        //    fpsCamera = true;
        //    fpscamera.SetActive(true);
        //    Debug.Log(mainCamera);
        //    Debug.Log(fpsCamera);
        //    Debug.Log(selfCamera);
        //}
        // else if(fpsCamera == true)
        //{
        //    fpsCamera = false;
        //    fpscamera.SetActive(false);

        //    selfCamera = true;
        //    selfcamera.SetActive(true);
        //    Debug.Log(mainCamera);
        //    Debug.Log(fpsCamera);
        //    Debug.Log(selfCamera);
        //}
        // else if (selfCamera == true)
        //{
        //    selfCamera = false;
        //    selfcamera.SetActive(false);

        //    mainCamera = true;
        //    maincamera.SetActive(true);
        //    Debug.Log(mainCamera);
        //    Debug.Log(fpsCamera);
        //    Debug.Log(selfCamera);
        //}


        if(maincamera.enabled &&playerController.inWater ==  false)//FPSCameraに切り替え
        {
            maincamera.enabled = false;

            fpscamera.enabled = true;
        }

        else if(fpscamera.enabled && playerController.inWater == false)//自撮りカメラに切り替え
        {
            fpscamera.enabled = false;

            selfcamera.enabled = true;

            //if(playerController.inWater == true)//着水後強制的にMainCameraに戻す　強制的に出来ていないからまた考える
            //{
            //    selfcamera.enabled = false;

            //    maincamera.enabled = true;
            //}
        }
        //else if (fpscamera.enabled && playerController.inWater == true)//一人称視点の時だけ条件式を追加しておけば最後はメインの三人称に出来るはず
        //{
        //    fpscamera.enabled = false;

        //    maincamera.enabled = true;
        //}

        else if (selfcamera.enabled &&playerController.inWater == false)//MainCameraに切り替え  上手くできていたが、強制的に切り替えではない
        {
            selfcamera.enabled = false;

            maincamera.enabled = true;

            //if (playerController.inWater == true)//着水後強制的にMainCameraに戻す　おそらくここも上と同じ
            //{
            //    selfcamera.enabled = false;

            //    maincamera.enabled = true;
            //}
        }

    }
}
