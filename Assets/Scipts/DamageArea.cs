using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//伤害陷阱
public class DamageArea : MonoBehaviour
{


    private float invincibleTimer; //无敌计时器

    private bool isInvincible; //是否处于无敌状态

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController pc = collision.GetComponent<PlayerController>();


        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }
        






    }
}
