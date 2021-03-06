﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostureChange : MonoBehaviour
{
    public bool bp = false;//ボタンを押したら姿勢を切り替えるため

    public bool wt;//着水情報を入手するため
    public Vector3 angle;//画像の角度を入れる箱,上手くいかない

    public GameMaster gameMaster;


    private Vector3 startpos;

    private PlayerController playercontroller;

    private FlowerCircle flowercircle;

    // Start is called before the first frame update
    void Start()
    {

        angle = transform.eulerAngles;//これでここに角度を入れて変更する予定

        startpos = transform.eulerAngles;

        angle.z = 180.0f;

        transform.eulerAngles = new Vector3(0, 0, angle.z);

        //transform.Rotate(new Vector3(0f, 0f, 180f));//逆さま向いた

        //Debug.Log(angle.z);

        playercontroller = GameObject.Find("penguin").GetComponent<PlayerController>();

        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();//ゲージのデータをこれで取得


    }

    // Update is called once per frame
    void Update()
    {
        //wt = GameObject.Find("penguin").GetComponent < PlayerController > ().inWater;//着水情報を入手して画像の角度を元に戻すため 

        //Debug.Log(wt);

        if(playercontroller.inWater == true)//考え中、最初の角度を変数に入れてそれをここに持ってくるのがいいと思う
        {
            transform.eulerAngles = startpos;
        }



    }


    public void OnClick()//クリックしたらtrueとfalseを切り替える
    {
        if (bp != true)//trueじゃなかったら＋ゲージがMaxだったら
        {
            bp = true;
            //Debug.Log("trueになった");

            angle.z = 90.0f;//動かない
            transform.eulerAngles = new Vector3(0, 0, angle.z);

            //transform.Rotate(new Vector3(0f, 0f, -90f));//傾く

            gameMaster.isGaugeMax = false;
        }

        else
        {
            bp = false;
            //Debug.Log("falseになった");

            angle.z = 180.0f;//動かない
            transform.eulerAngles = new Vector3(0, 0, angle.z);

            //transform.Rotate(new Vector3(0, 0, 90f));//傾きを戻す
        }

        gameMaster.ChangeButtonInteractable(angle.z);
    }
}
