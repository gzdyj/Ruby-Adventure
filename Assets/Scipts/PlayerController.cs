using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController:MonoBehaviour
{
    public float speed = 5f;//移动速度

    private int maxHealth = 5; // 最大生命值
    private int currentHealth; //当前生命值

    [SerializeField]
    private int curBulletCount; //当前子弹
    private int maxBulletCount; //最大子弹 

    public int MyCurBullerCount { get { return curBulletCount; } }
    public int MyMaxBulletCount { get { return maxBulletCount; } }
    
    
    //获取生命的get方法
    public int MyMaxHealth { get { return maxHealth; } }
    public int MyCurrentHealth { get { return currentHealth; } }
    
    private float invincibleTime = 2f; //无敌时间2秒

    private float invincibleTimer; //无敌计时器

    private bool isInvincible; //是否处于无敌状态

    public GameObject bulletPrefab; //子弹

    //=====玩家的音效=====

    public AudioClip hitClip;//受伤音效
    public AudioClip launchClip;//发射齿轮的音效


    //===============玩家的朝向===========
    private Vector2 lookDirection = new Vector2(1, 0); //默认朝向右方

    Animator anim;

    Rigidbody2D rbody;//刚体组件

    // Start is called before the first frame update
    void Start()
    {
        invincibleTimer = 0;
        currentHealth = 2;
        curBulletCount = 2;
        maxBulletCount = 99;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth); //更新血条
        UImanager.instance.UpdateBulletCount(curBulletCount, maxBulletCount); //更新子弹



    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); //控制水平移动方向A： -1， D： 1
        float moveY = Input.GetAxisRaw("Vertical"); //控制垂直移动方向W：1 S：-1

        Vector2 moveVector = new Vector2(moveX, moveY);
        if (moveVector.x != 0 || moveVector.y != 0) {
            lookDirection = moveVector;
        }
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", moveVector.magnitude);

        // =============== 移动 ====================
        Vector2 position = rbody.position;
        //position.x += moveX * speed * Time.deltaTime;
        //position.y += moveY * speed * Time.deltaTime;
        position += moveVector * speed * Time.deltaTime;
        rbody.MovePosition(position);
        
        // ===========无敌计时===========
        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0) {
                Debug.Log("无敌时间");
                isInvincible = false; //倒计时结束以后（2秒）取消无敌状态
            }
        }
        //======按下J键 并且 子弹数量大于0  进行攻击===
        if (Input.GetKeyDown(KeyCode.J) && curBulletCount > 0) {
            ChangeBulletCount(-1); //每次攻击减1子弹
            anim.SetTrigger("Launch");//播放攻击动画
            AudioManager.instance.AudioPlay(launchClip);
            GameObject bullet = Instantiate(bulletPrefab, rbody.position+Vector2.up*0.5f, Quaternion.identity);
            BulletController bc = bullet.GetComponent<BulletController>();
            if (bc != null) {
                bc.Move(lookDirection, 300);
            }
        }
        //==============按下 E 键 进行NPC交互
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rbody.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
                if (npc != null )
                {
                    npc.ShowDialog(); //显示对话框
                }
            }
        }
    }
    //改变玩家的生命值
    public void ChangeHealth(int amount) {
        //如果玩家受到伤害
        if (amount < 0) {
            if (isInvincible == true) {
                return;
            }
            isInvincible = true;
            anim.SetTrigger("Hit");
            AudioManager.instance.AudioPlay(hitClip);
            invincibleTimer = invincibleTime;
        }

        Debug.Log(currentHealth + "/" + maxHealth);
        //把玩家的生命值约束在0 和 最大值之间
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UImanager.instance.UpdateHealthBar(currentHealth, maxHealth); //更新血条
        Debug.Log(currentHealth + "/" + maxHealth); 
    }

    public void ChangeBulletCount(int amount)
    {
        curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount); //限制子弹数量在0-最大值之间
        UImanager.instance.UpdateBulletCount(curBulletCount, maxBulletCount); //更新子弹
    }


}
