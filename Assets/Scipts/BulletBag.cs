using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public int bulletCount = 10; //包里含有的的子弹数量
    public AudioClip collectClip;
    public ParticleSystem collectEffect; //拾取特效

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (pc.MyCurBullerCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount); //增加玩家的子弹数
                Instantiate(collectEffect, transform.position, Quaternion.identity); //添加拾取特效
                AudioManager.instance.AudioPlay(collectClip); //播放音效
                Destroy(this.gameObject);
            }
        }
    }

}
