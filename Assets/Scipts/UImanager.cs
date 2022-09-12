using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI管理相关
/// </summary>
public class UImanager : MonoBehaviour
{
    public Image healthBar; //角色的血条
    //单例模式
    public Text bulletCountText; //子弹数量内容显示


    public static UImanager instance { get; private set; }


    void Awake()
    {
        instance = this;

    }
    /// <summary>
    /// 更新血条
    /// </summary>
    /// <param name="curAmount"></param>
    /// <param name="maxAmount"></param>

    public void UpdateHealthBar(int curAmount, int maxAmount)
    {
        healthBar.fillAmount = (float)curAmount / (float)maxAmount;
    }
    //更新子弹数量文本的显示
    public void UpdateBulletCount( int curAmount, int maxAmount)
    {
        bulletCountText.text = curAmount.ToString() + "/" + maxAmount.ToString();
    }
}
