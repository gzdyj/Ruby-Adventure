using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC交互相关
/// </summary>

public class NPCmanager : MonoBehaviour
{
    public GameObject tipImage; //按键提示

    public GameObject dialogImage;

    public float showTime = 4; //对话框显示器计时器

    public float showTimer; //对话框显示计时器

    void Start()
    {
        tipImage.SetActive(true); //初始默认提示键
        dialogImage.SetActive(false); //初始默认隐藏对话框
        showTimer = -1;
    }
    void Update()
    {
        showTimer -= Time.deltaTime;
        if (showTimer < 0)
        {
            dialogImage.SetActive(false);
        }
    }
    //显示对话框
    public void ShowDialog()
    {
        showTimer = showTime;
        tipImage.SetActive(false);
        dialogImage.SetActive(true);
    }
}
